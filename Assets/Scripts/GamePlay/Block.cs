using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Block : MonoBehaviour,IIntEventInvoker,IEventInvoker {

    protected int worthPoints;

    Dictionary<EventName, UnityEvent> unityEvents = new Dictionary<EventName, UnityEvent>();
    Dictionary<EventName, UnityEvent<int>> unityIntEvents = new Dictionary<EventName, UnityEvent<int>>();

    // Use this for initialization
    virtual protected void Start () {
        unityIntEvents.Add(EventName.PointsAddedIntEvent, new PointsAddedEvent());
        EventManager.AddInvoker(EventName.PointsAddedIntEvent, (IIntEventInvoker)this);

        unityEvents.Add(EventName.BlockDestroyedEvent, new BlockDestroyedEvent());
        EventManager.AddInvoker(EventName.BlockDestroyedEvent, (IEventInvoker)this);
    }


    virtual protected void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.CompareTag("Ball"))
        {
            Destroy(this.gameObject);
            unityIntEvents[EventName.PointsAddedIntEvent].Invoke(worthPoints);
            unityEvents[EventName.BlockDestroyedEvent].Invoke();
        }
    }

    public void AddListener(EventName eventName, UnityAction<int> unityAction)
    {
        if (unityIntEvents.ContainsKey(eventName))
        {
            unityIntEvents[eventName].AddListener(unityAction);
        }
    }

    public void AddListener(EventName eventName, UnityAction unityAction)
    {
        if (unityEvents.ContainsKey(eventName))
        {
            unityEvents[eventName].AddListener(unityAction);
        }
    }
}
