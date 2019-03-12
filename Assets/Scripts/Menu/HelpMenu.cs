using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpMenu : MonoBehaviour {
    public void HandleBackButtonOnClickEvent()
    {
        MenuManager.GoToMenu(MenuName.Main);
        Destroy(gameObject);
    }
}

