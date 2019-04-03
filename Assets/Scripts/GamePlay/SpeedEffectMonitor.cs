using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEffectMonitor : MonoBehaviour {

    float speedUpDuration;
    float speedUpFactor;
    Timer speedUpTimer;


	// Use this for initialization
	void Start () {
        speedUpTimer = gameObject.AddComponent<Timer>();
        EventManager.AddListener(EventName.SpeedUpEffectActivated, GetSpeedEffectInfo);
    }

    private void Update()
    {
        EffectUtils.speedEffectDuration = speedUpTimer.GetTimeLeft();
        EffectUtils.speedEffectFactor = speedUpFactor;
    }

    void GetSpeedEffectInfo(float duration,float factor) {
        speedUpTimer.Duration = duration;
        speedUpFactor = factor;
        speedUpTimer.Run();
        EffectUtils.speedUp = true;
    }
}
