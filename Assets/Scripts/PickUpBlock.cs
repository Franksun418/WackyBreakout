using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUpBlock : Block {

    [SerializeField]
    Sprite freezerSprite;
    [SerializeField]
    Sprite speedUpSprite;

    public bool IsFreezer;
    public bool IsFaster;

    float speedUpFactor;
    float speedUpEffectDuration;

    public float freezerEffectDuration;

    FreezerEffectActivated freezerEffectActivated = new FreezerEffectActivated();
    SpeedUpEffectActivated speedUpEffectActivated = new SpeedUpEffectActivated();

    PickUpEffect effect;

    // Use this for initialization
    void Start () {
        base.Start();
        speedUpFactor = ConfigurationUtils.SpeedUpFactor;
        speedUpEffectDuration = ConfigurationUtils.SpeedUpEffectDuration;
        worthPoints = ConfigurationUtils.PickUpBlockPoints;

        if (IsFreezer)
        {
            EventManager.AddFreezerEffectInvoker(this);
        }

        if (IsFaster)
        {
            EventManager.AddSpeedUpEffectInvoker(this);
        }

    }

    private void Update()
    {
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

    public void AddFreezerEffectListener(UnityAction<float> listener) {
        freezerEffectActivated.AddListener(listener);
    }

    public void AddSpeedUpEffectListener(UnityAction<float, float> listener)
    {
        speedUpEffectActivated.AddListener(listener);
    }

    protected override void OnCollisionEnter2D(Collision2D coll)
    {

        if (IsFreezer)
        {
            freezerEffectActivated.Invoke(freezerEffectDuration);
            AudioManager.Play(AudioClipName.BallHitFreezerBlock);
        }
        if (IsFaster)
        {
            speedUpEffectActivated.Invoke(speedUpEffectDuration, speedUpFactor);
            AudioManager.Play(AudioClipName.BallHitSpeedUpBlock);
        }
        base.OnCollisionEnter2D(coll);
    }

}
