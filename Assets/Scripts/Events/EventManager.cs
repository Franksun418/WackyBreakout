using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    static Dictionary<EventName, List<UnityAction<int>>> listeners = new Dictionary<EventName, List<UnityAction<int>>>();
    static Dictionary<EventName, List<IntEventInvoker>> invokers = new Dictionary<EventName, List<IntEventInvoker>>();

    static List<PickUpBlock> freezerEffectInvokers = new List<PickUpBlock>();
    static UnityAction<float> freezerEffectListener;

    static List<PickUpBlock> speedUpEffectInvokers = new List<PickUpBlock>();
    static List<UnityAction<float, float>> speedUpEffectListener = new List<UnityAction<float, float>>();

    public static void Initialize() {
        foreach (EventName eventName in Enum.GetValues(typeof(EventName)))
        {
            if (!invokers.ContainsKey(eventName))
            {
                invokers.Add(eventName, new List<IntEventInvoker>());
                listeners.Add(eventName, new List<UnityAction<int>>());
            }
            else {
                invokers[eventName].Clear();
                listeners[eventName].Clear();
            }
            
        }
    }

    public static void AddInvoker(EventName eventName, IntEventInvoker invoker) {
		foreach (UnityAction<int> listener in listeners[eventName])
        {
			invoker.AddListener(eventName, listener);
		}
		invokers[eventName].Add(invoker);
    }

    public static void AddListener(EventName eventName, UnityAction<int> listener) {
        foreach (IntEventInvoker invoker in invokers[eventName])
        {
            invoker.AddListener(eventName, listener);
        }
        listeners[eventName].Add(listener);

    }

    public static void RemoveInvoker(EventName eventName, IntEventInvoker intEventInvoker) {
        invokers[eventName].Remove(intEventInvoker);
    }

    public static void AddFreezerEffectInvoker(PickUpBlock script)
    {
        freezerEffectInvokers.Add(script);
        script.AddFreezerEffectListener(freezerEffectListener);
    }

    public static void AddFreezerEffectListener(UnityAction<float> script)
    {
        freezerEffectListener = script;
        foreach (PickUpBlock pub in freezerEffectInvokers)
        {
            pub.AddFreezerEffectListener(freezerEffectListener);
        }
    }

    public static void AddSpeedUpEffectInvoker(PickUpBlock script)
    {
        speedUpEffectInvokers.Add(script);
        foreach (UnityAction<float, float> listener in speedUpEffectListener)
        {
            script.AddSpeedUpEffectListener(listener);
        }
    }

    public static void AddSpeedUpEffectListener(UnityAction<float, float> script)
    {
        speedUpEffectListener.Add(script);
        foreach (PickUpBlock pub in speedUpEffectInvokers)
        {
            pub.AddSpeedUpEffectListener(script);
        }

    }


}
