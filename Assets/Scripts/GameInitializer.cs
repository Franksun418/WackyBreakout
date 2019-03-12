using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Initializes the game
/// </summary>
public class GameInitializer : MonoBehaviour 
{
    /// <summary>
    /// Awake is called before Start
    /// </summary>
	void Start()
    {
        // initialize screen utils
        EventManager.Initialize();
        ScreenUtils.Initialize();
        ConfigurationUtils.Initialize();

    }
}
