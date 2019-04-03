using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IDoubleFloatEventInvoker {
     void AddListener(EventName eventName, UnityAction<float, float> unityAction);
}
