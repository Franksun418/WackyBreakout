using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HUD : MonoBehaviour,IIntEventInvoker
{

    Text score;
    Text ballNum;
    int points;
    int ballsLeft;

    Dictionary<EventName, UnityEvent<int>> unityIntEvents = new Dictionary<EventName, UnityEvent<int>>();


    // Use this for initialization
    void Start()
    {
        EventManager.AddListener(EventName.PointsAddedIntEvent, HandlePointsAddedEvent);
        EventManager.AddListener(EventName.BallReducedEvent, HandleBallReducedEvent);

        unityIntEvents.Add(EventName.LastBallLostIntEvent, new LastBallLostIntEvent());
        EventManager.AddInvoker(EventName.LastBallLostIntEvent, this);

        score = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        ballNum = GameObject.FindGameObjectWithTag("BallNumText").GetComponent<Text>();
        score.text = "Score: " + points.ToString();
        ballsLeft = ConfigurationUtils.BallsPerGame;
        ballNum.text = "Balls Left: " + ballsLeft.ToString();
    }

    void IIntEventInvoker.AddListener(EventName eventName, UnityAction<int> unityAction)
    {
        if (unityIntEvents.ContainsKey(eventName))
        {
            unityIntEvents[eventName].AddListener(unityAction);
        }
    }

    void HandlePointsAddedEvent(int worthPoints)
    {
        points += worthPoints;
        score.text = "Score: " + points.ToString();
    }

    void HandleBallReducedEvent()
    {
        if (ballsLeft < 1)
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
