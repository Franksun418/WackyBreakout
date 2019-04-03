using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IIntEventInvoker {
    void AddListener(EventName eventName, UnityAction<int> unityAction);
}
