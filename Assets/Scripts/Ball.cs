﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : IntEventInvoker {

    Rigidbody2D rgbd2d;
    float angle;
    Vector2 force;
    Timer timerRebirth;
    Timer timerSpeedUp;
    Timer timerMove;
    BallSpawner ballSpawner;
    float speedFactor;
    public bool speedUp;
    public float myspeed;
    public bool newball;

    // Use this for initialization
    void Start () {
       // StartCoroutine(StopTheBallForASec());
        timerRebirth = gameObject.AddComponent<Timer>();
        timerRebirth.Duration = ConfigurationUtils.BallLifeTime;
        timerRebirth.Run();
        timerSpeedUp = gameObject.AddComponent<Timer>();
        timerSpeedUp.fastertimer = true;
        timerMove = gameObject.AddComponent<Timer>();
        timerMove.Duration = 1.5f;
        timerMove.Run();
        rgbd2d = this.gameObject.GetComponent<Rigidbody2D>();
        angle = -90.0f * Mathf.Deg2Rad; 
        force = new Vector2(ConfigurationUtils.BallImpulseForce * Mathf.Cos(angle), ConfigurationUtils.BallImpulseForce * Mathf.Sin(angle));
        ballSpawner = Camera.main.GetComponent<BallSpawner>();
        EventManager.AddSpeedUpEffectListener(SpeepThisUp);

        unityEvents.Add(EventName.BallReducedEvent, new BallReducedEvent());
        EventManager.AddInvoker(EventName.BallReducedEvent, this);

        unityEvents.Add(EventName.BallDiedEvent, new BallDiedEvent());
        EventManager.AddInvoker(EventName.BallDiedEvent, this);

        timerRebirth.AddTimerFinishedEventListener(HandleRebrithTimerFinishedEvent);
        timerMove.AddTimerFinishedEventListener(HandleMoveTimerFinishedEvent);
        timerSpeedUp.AddTimerFinishedEventListener(HandleSpeedUpTimerFinishedEvent);

    }
	
	// Update is called once per frame
	void Update () {

        myspeed = rgbd2d.velocity.magnitude;
	}

    void StartMoving() {

        if (EffectUtils.speedUp)
        {
            rgbd2d.AddForce(force* EffectUtils.speedEffectFactor);
            speedUp = true;
            timerSpeedUp.Duration = EffectUtils.speedEffectDuration;
            timerSpeedUp.Run();
            newball = true;
            //   SpeepThisUp(EffectUtils.speedEffectDuration, EffectUtils.speedEffectFactor);
        }
        else {
            rgbd2d.AddForce(force);
        }
    }

    public void SetDirection(Vector2 direction) {
        float speed = rgbd2d.velocity.magnitude;
        rgbd2d.velocity = speed * direction;
    }

    private void OnBecameInvisible()
    {
        if (timerRebirth.Running)
        {
            unityEvents[EventName.BallReducedEvent].Invoke(0);
            Destroy(gameObject);
        }
    }

    void SpeepThisUp(float duration,float amount) {
        if (rgbd2d.velocity.magnitude ==0)
        {
            return;
        }
        if (timerSpeedUp.running)
        {
            timerSpeedUp.Duration += duration;
        }
        else
        {
            speedUp = true;
            timerSpeedUp.Duration = duration;
            timerSpeedUp.Run();
            speedFactor = amount;
            rgbd2d.AddForce(rgbd2d.velocity / rgbd2d.velocity.magnitude * ConfigurationUtils.BallImpulseForce*speedFactor/2);
        }
    }

    public void HandleRebrithTimerFinishedEvent() {
        unityEvents[EventName.BallDiedEvent].Invoke(0);
        Destroy(gameObject);
    }

    public void HandleMoveTimerFinishedEvent()
    {
        timerMove.Stop();
        StartMoving();
    }

    public void HandleSpeedUpTimerFinishedEvent() {
        if (timerSpeedUp.started && speedUp) {
            rgbd2d.AddForce(rgbd2d.velocity / rgbd2d.velocity.magnitude * ConfigurationUtils.BallImpulseForce * speedFactor / -2);
            speedUp = false;
            EffectUtils.speedUp = false;
        }
    }

    }
