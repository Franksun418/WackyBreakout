using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour,IEventInvoker{

    Rigidbody2D rgbd2d;
    float angle;
    Vector2 force;
    Timer timerRebirth;
    Timer timerSpeedUp;
    Timer timerMove;
    BallSpawner ballSpawner;
    float halfHeight;
    float speedFactor;
    public bool speedUp;
    public float myspeed;
    public bool newball;

    Dictionary<EventName, UnityEvent> unityEvents = new Dictionary<EventName, UnityEvent>();

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
        rgbd2d = gameObject.GetComponent<Rigidbody2D>();
        angle = -90.0f * Mathf.Deg2Rad; 
        force = new Vector2(ConfigurationUtils.BallImpulseForce * Mathf.Cos(angle), ConfigurationUtils.BallImpulseForce * Mathf.Sin(angle));
        ballSpawner = Camera.main.GetComponent<BallSpawner>();
        halfHeight = gameObject.GetComponent<BoxCollider2D>().size.y / 2;
        EventManager.AddListener(EventName.SpeedUpEffectActivated,SpeepThisUp);



        unityEvents.Add(EventName.BallReducedEvent, new BallReducedEvent());
        EventManager.AddInvoker(EventName.BallReducedEvent, this as IEventInvoker);

        unityEvents.Add(EventName.BallDiedEvent, new BallDiedEvent());
        EventManager.AddInvoker(EventName.BallDiedEvent, this as IEventInvoker);

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

    public void AddListener(EventName eventName,UnityAction unityAction) {
        if (unityEvents.ContainsKey(eventName))
        {
            unityEvents[eventName].AddListener(unityAction);
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
            if (transform.position.y - halfHeight < ScreenUtils.ScreenBottom)
            {
                unityEvents[EventName.BallReducedEvent].Invoke();
                Destroy(gameObject);
            }
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
        unityEvents[EventName.BallDiedEvent].Invoke();
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
