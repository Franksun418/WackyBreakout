using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : IntEventInvoker {

    protected int worthPoints;
    
	// Use this for initialization
	virtual protected void Start () {
        unityEvents.Add(EventName.PointsAddedEvent, new PointsAddedEvent());
        EventManager.AddInvoker(EventName.PointsAddedEvent, this);

        unityEvents.Add(EventName.BlockDestroyedEvent, new BlockDestroyedEvent());
        EventManager.AddInvoker(EventName.BlockDestroyedEvent, this);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    virtual protected void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.CompareTag("Ball"))
        {
            Destroy(this.gameObject);
            unityEvents[EventName.PointsAddedEvent].Invoke(worthPoints);
            unityEvents[EventName.BlockDestroyedEvent].Invoke(0);
        }
    }
}
