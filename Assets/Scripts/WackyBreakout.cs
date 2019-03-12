using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WackyBreakout : MonoBehaviour {

    [SerializeField]
    Transform gameOverMessage;

    [SerializeField]
    HUD hUD;

	// Use this for initialization
	void Start () {
        EventManager.AddListener(EventName.LastBallLostEvent, HandleGameLostEvent);
        EventManager.AddListener(EventName.AllBlockDestroyedEvent, HandleGameWonEvent);
	}
	

	// Update is called once per frame
	void Update () {

    }

    public void HandleGameLostEvent(int num) {
        var overMessage =  Instantiate(gameOverMessage).GetComponent<GameOverMessage>();
         overMessage.title.text = "You Lost!";
         overMessage.finalScore.text = "Your Score: " + num.ToString();
    }

    public void HandleGameWonEvent(int num) {
        var overMessage = Instantiate(gameOverMessage).GetComponent<GameOverMessage>();
        overMessage.title.text = "You Won!";
        overMessage.finalScore.text = "Your Score: " + hUD.Score.ToString();
    }


}
