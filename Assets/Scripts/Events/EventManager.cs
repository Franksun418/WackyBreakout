using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    static Dictionary<EventName, List<UnityAction<int>>> intListeners = new Dictionary<EventName, List<UnityAction<int>>>();
    static Dictionary<EventName, List<IntEventInvoker>> intInvokers = new Dictionary<EventName, List<IntEventInvoker>>();

    static Dictionary<EventName, List<UnityAction<float>>> floatListeners = new Dictionary<EventName, List<UnityAction<float>>>();
    static Dictionary<EventName, List<FloatEventInvoker>> floatInvokers = new Dictionary<EventName, List<FloatEventInvoker>>();

    static Dictionary<EventName, List<UnityAction>> Listeners = new Dictionary<EventName, List<UnityAction>>();
    static Dictionary<EventName, List<EventInvoker>> Invokers = new Dictionary<EventName, List<EventInvoker>>();


    static List<PickUpBlock> freezerEffectInvokers = new List<PickUpBlock>();
    static UnityAction<float> freezerEffectListener;

    static List<PickUpBlock> speedUpEffectInvokers = new List<PickUpBlock>();
    static List<UnityAction<float, float>> speedUpEffectListener = new List<UnityAction<float, float>>();

    public static void Initialize() {
        foreach (EventName eventName in Enum.GetValues(typeof(EventName)))
        {
            string name = eventName.ToString();
            if (name.IndexOf("Int") >= 0)
            {
                if (!intInvokers.ContainsKey(eventName))
                {
                    intInvokers.Add(eventName, new List<IntEventInvoker>());
                    intListeners.Add(eventName, new List<UnityAction<int>>());
                }
                else
                {
                    intInvokers[eventName].Clear();
                    intListeners[eventName].Clear();
                }
            }
            else if (name.IndexOf("Float") >= 0)
            {
                if (!floatInvokers.ContainsKey(eventName))
                {
                    floatInvokers.Add(eventName, new List<FloatEventInvoker>());
                    floatListeners.Add(eventName, new List<UnityAction<float>>());
                }
                else
                {
                    floatInvokers[eventName].Clear();
                    floatListeners[eventName].Clear();
                }
            }
            else {
                if (!Invokers.ContainsKey(eventName))
                {
                    Invokers.Add(eventName, new List<EventInvoker>());
                    Listeners.Add(eventName, new List<UnityAction>());
                }
                else
                {
                    Invokers[eventName].Clear();
                    Listeners[eventName].Clear();
                }
            }
            
        }
        
    }


    public static void AddIntInvoker(EventName eventName, IntEventInvoker invoker) {
        intInvokers[eventName].Add(invoker);
        foreach (UnityAction<int> listener in intListeners[eventName])
        {
			invoker.AddIntListener(eventName, listener);
		}

    }

    public static void AddIntListener(EventName eventName, UnityAction<int> listener) {
        intListeners[eventName].Add(listener);
        foreach (IntEventInvoker invoker in intInvokers[eventName])
        {
            invoker.AddIntListener(eventName, listener);
        }


    }

    public static void AddFloatInvoker(EventName eventName, FloatEventInvoker invoker)
    {
        foreach (UnityAction<float> listener in floatListeners[eventName])
        {
            invoker.AddFloatEventListener(eventName, listener);
        }
        floatInvokers[eventName].Add(invoker);
    }

    public static void AddFloatListener(EventName eventName, UnityAction<float> listener)
    {
        foreach (FloatEventInvoker invoker in floatInvokers[eventName])
        {
            invoker.AddFloatEventListener(eventName, listener);
        }
        floatListeners[eventName].Add(listener);

    }

    public static void AddInvoker(EventName eventName, EventInvoker invoker)
    {
        foreach (UnityAction listener in Listeners[eventName])
        {
            invoker.AddListener(eventName, listener);
        }
        Invokers[eventName].Add(invoker);
    }

    public static void AddListener(EventName eventName, UnityAction listener)
    {
        foreach (EventInvoker invoker in Invokers[eventName])
        {
            invoker.AddListener(eventName, listener);
        }
        Listeners[eventName].Add(listener);

    }

    public static void RemoveIntInvoker(EventName eventName, IntEventInvoker intEventInvoker) {
        intInvokers[eventName].Remove(intEventInvoker);
    }

    public static void RemoveFloatInvoker(EventName eventName, FloatEventInvoker floatEventInvoker)
    {
        floatInvokers[eventName].Remove(floatEventInvoker);
    }

    public static void RemoveInvoker(EventName eventName, EventInvoker EventInvoker)
    {
        Invokers[eventName].Remove(EventInvoker);
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
