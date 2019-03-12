using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMessage : MonoBehaviour {

    [SerializeField]
    public Text title;
    [SerializeField]
    public Text finalScore;

	// Use this for initialization
	void Start () {
        Time.timeScale = 0;
	}

    public void HandleQuitButtonClicked() {
        Time.timeScale = 1;
        MenuManager.GoToMenu(MenuName.Main);
        Destroy(gameObject);
    }
}
