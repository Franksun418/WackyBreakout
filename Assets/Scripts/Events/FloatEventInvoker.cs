using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloatEventInvoker : EventInvoker
{

    Dictionary<EventName, UnityEvent<float>> unityFloatEvents = new Dictionary<EventName, UnityEvent<float>>();

    public void AddFloatEventListener(EventName eventName, UnityAction<float> unityAction)
    {
        unityFloatEvents[eventName].AddListener(unityAction);
    }

}
