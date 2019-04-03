using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public interface IFloatEventInvoker {
    void AddListener(EventName eventName, UnityAction<float> unityAction);
}
