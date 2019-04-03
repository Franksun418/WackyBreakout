using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IEventInvoker
{
    void AddListener(EventName eventName, UnityAction unityAction);
}
