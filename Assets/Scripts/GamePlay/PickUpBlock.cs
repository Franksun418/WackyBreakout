using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUpBlock : Block,IFloatEventInvoker,IDoubleFloatEventInvoker {

    [SerializeField]
    Sprite freezerSprite;
    [SerializeField]
    Sprite speedUpSprite;

    public bool IsFreezer;
    public bool IsFaster;

    float speedUpFactor;
    float speedUpEffectDuration;

    public float freezerEffectDuration;


    PickUpEffect effect;

    Dictionary<EventName, UnityEvent<float, float>> unityDoubleFloatEvents = new Dictionary<EventName, UnityEvent<float, float>>();
    Dictionary<EventName, UnityEvent<float>> unityFloatEvents = new Dictionary<EventName, UnityEvent<float>>();

    // Use this for initialization
    void Start () {
        base.Start();
        speedUpFactor = ConfigurationUtils.SpeedUpFactor;
        speedUpEffectDuration = ConfigurationUtils.SpeedUpEffectDuration;
        worthPoints = ConfigurationUtils.PickUpBlockPoints;

        if (IsFreezer)
        {
            unityFloatEvents.Add(EventName.FreezerEffectActivated, new FreezerEffectActivated());
            EventManager.AddInvoker(EventName.FreezerEffectActivated, this as IFloatEventInvoker);
        }

        if (IsFaster)
        {
            unityDoubleFloatEvents.Add(EventName.SpeedUpEffectActivated, new SpeedUpEffectActivated());
            EventManager.AddInvoker(EventName.SpeedUpEffectActivated, this as IDoubleFloatEventInvoker);
        }
    }


    public void AddListener(EventName eventName, UnityAction<float> unityAction)
    {
        if (unityFloatEvents.ContainsKey(eventName))
        {
            unityFloatEvents[eventName].AddListener(unityAction);
        }
    }

    public void AddListener(EventName eventName, UnityAction<float,float> unityAction)
    {
        if (unityDoubleFloatEvents.ContainsKey(eventName))
        {
            unityDoubleFloatEvents[eventName].AddListener(unityAction);
        }
    }


    public PickUpEffect Effect {
        set {
            effect = value;
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

            if (effect == PickUpEffect.Freezer)
            {
                spriteRenderer.sprite = freezerSprite;
                IsFreezer = true;
                IsFaster = false;
                freezerEffectDuration = ConfigurationUtils.FreezerEffectDuration;

            }
            if(effect == PickUpEffect.SpeedUp) {
                spriteRenderer.sprite = speedUpSprite;
                IsFaster = true;
                IsFreezer = false;
            }
        }
    }


    protected override void OnCollisionEnter2D(Collision2D coll)
    {

        if (IsFreezer)
        {
            unityFloatEvents[EventName.FreezerEffectActivated].Invoke(freezerEffectDuration);
            AudioManager.Play(AudioClipName.BallHitFreezerBlock);
        }
        if (IsFaster)
        {
            unityDoubleFloatEvents[EventName.SpeedUpEffectActivated].Invoke(speedUpEffectDuration, speedUpFactor);
            AudioManager.Play(AudioClipName.BallHitSpeedUpBlock);
        }
        base.OnCollisionEnter2D(coll);
    }

}
