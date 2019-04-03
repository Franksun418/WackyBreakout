using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBlock : Block {

    [SerializeField]
    Sprite standardBlockSprite_1;
    [SerializeField]
    Sprite standardBlockSprite_2;
    [SerializeField]
    Sprite standardBlockSprite_3;

    List<Sprite> objects;
	// Use this for initialization
	void Start () {
        base.Start();
        worthPoints = ConfigurationUtils.StandardBlockPoints;
        objects = new List<Sprite>();
        objects.Add(standardBlockSprite_1);
        objects.Add(standardBlockSprite_2);
        objects.Add(standardBlockSprite_3);
        GetComponent<SpriteRenderer>().sprite = objects[Random.Range(0, objects.Count)];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    override protected void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball"))
        {
            AudioManager.Play(AudioClipName.BallHitStandardBlock);
        }
        base.OnCollisionEnter2D(coll);
    }
}
