using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : IntEventInvoker
{

    Text score;
    Text ballNum;
    int points;
    int ballsLeft;

    // Use this for initialization
    void Start()
    {
        EventManager.AddIntListener(EventName.PointsAddedIntEvent, HandlePointsAddedEvent);
        EventManager.AddListener(EventName.BallReducedEvent, HandleBallReducedEvent);

        unityIntEvents.Add(EventName.LastBallLostIntEvent, new LastBallLostIntEvent());
        EventManager.AddIntInvoker(EventName.LastBallLostIntEvent, this);

        score = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        ballNum = GameObject.FindGameObjectWithTag("BallNumText").GetComponent<Text>();
        score.text = "Score: " + points.ToString();
        ballsLeft = ConfigurationUtils.BallsPerGame;
        ballNum.text = "Balls Left: " + ballsLeft.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void HandlePointsAddedEvent(int worthPoints)
    {
        points += worthPoints;
        score.text = "Score: " + points.ToString();
    }

    void HandleBallReducedEvent()
    {
        if (ballsLeft == 0)
        {
            unityIntEvents[EventName.LastBallLostIntEvent].Invoke(points);
            AudioManager.Play(AudioClipName.GameLost);
            ballsLeft -= 1;
        }
        else {
            ballsLeft -= 1;
            if (ballNum != null)
                ballNum.text = "Balls Left: " + ballsLeft.ToString();
        }

    }

    public int Score{
        get { return points; }
}


}
