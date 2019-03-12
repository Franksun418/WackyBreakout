using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounsBlock : Block
{

    // Use this for initialization
    void Start()
    {
        base.Start();
        worthPoints = ConfigurationUtils.BonusBlockPoints;
    }

    // Update is called once per frame
    void Update()
    {

    }

    override protected void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball"))
        {
            AudioManager.Play(AudioClipName.BallHitBounsBlock);
        }
        base.OnCollisionEnter2D(coll);
    }
}
