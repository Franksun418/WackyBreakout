using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    static Dictionary<EventName, List<UnityAction<int>>> intListeners = new Dictionary<EventName, List<UnityAction<int>>>();
    static Dictionary<EventName, List<IIntEventInvoker>> intInvokers = new Dictionary<EventName, List<IIntEventInvoker>>();

    static Dictionary<EventName, List<UnityAction<float>>> floatListeners = new Dictionary<EventName, List<UnityAction<float>>>();
    static Dictionary<EventName, List<IFloatEventInvoker>> floatInvokers = new Dictionary<EventName, List<IFloatEventInvoker>>();

    static Dictionary<EventName, List<UnityAction>> Listeners = new Dictionary<EventName, List<UnityAction>>();
    static Dictionary<EventName, List<IEventInvoker>> Invokers = new Dictionary<EventName, List<IEventInvoker>>();

    static Dictionary<EventName, List<UnityAction<float, float>>> doubleFloatListeners = new Dictionary<EventName, List<UnityAction<float, float>>>();
    static Dictionary<EventName, List<IDoubleFloatEventInvoker>> doubleFloatInvokers = new Dictionary<EventName, List<IDoubleFloatEventInvoker>>();

    public static void Initialize()
    {

        foreach (EventName eventName in Enum.GetValues(typeof(EventName)))
        {
            switch (eventName)
            {
                case EventName.BallDiedEvent:
                case EventName.BallReducedEvent:
                case EventName.AllBlockDestroyedEvent:
                case EventName.BlockDestroyedEvent:
                    {
                        if (!Invokers.ContainsKey(eventName))
                        {
                            Invokers.Add(eventName, new List<IEventInvoker>());
                            Listeners.Add(eventName, new List<UnityAction>());
                        }
                        else
                        {
                            Invokers[eventName].Clear();
                            Listeners[eventName].Clear();
                        }
                    }
                    break;

                case EventName.LastBallLostIntEvent:
                case EventName.PointsAddedIntEvent:
                    {
                        if (!intInvokers.ContainsKey(eventName))
                        {
                            intInvokers.Add(eventName, new List<IIntEventInvoker>());
                            intListeners.Add(eventName, new List<UnityAction<int>>());
                        }
                        else
                        {
                            intInvokers[eventName].Clear();
                            intListeners[eventName].Clear();
                        }
                    }
                    break;
                case EventName.FreezerEffectActivated:
                    {
                        if (!floatInvokers.ContainsKey(eventName))
                        {
                            floatInvokers.Add(eventName, new List<IFloatEventInvoker>());
                            floatListeners.Add(eventName, new List<UnityAction<float>>());
                        }
                        else
                        {
                            floatInvokers[eventName].Clear();
                            floatListeners[eventName].Clear();
                        }
                    }
                    break;
                case EventName.SpeedUpEffectActivated:
                    {
                        if (!doubleFloatInvokers.ContainsKey(eventName))
                        {
                            doubleFloatInvokers.Add(eventName, new List<IDoubleFloatEventInvoker>());
                            doubleFloatListeners.Add(eventName, new List<UnityAction<float, float>>());
                        }
                        else
                        {
                            doubleFloatInvokers[eventName].Clear();
                            doubleFloatListeners[eventName].Clear();
                        }
                    }
                    break;
            }
        }

    }


    public static void AddInvoker(EventName eventName, IIntEventInvoker invoker)
    {
        intInvokers[eventName].Add(invoker);
        foreach (UnityAction<int> listener in intListeners[eventName])
        {
            invoker.AddListener(eventName, listener);
        }

    }

    public static void AddListener(EventName eventName, UnityAction<int> listener)
    {
        intListeners[eventName].Add(listener);
        foreach (IIntEventInvoker invoker in intInvokers[eventName])
        {
            invoker.AddListener(eventName, listener);
        }


    }

    public static void AddInvoker(EventName eventName, IFloatEventInvoker invoker)
    {
        foreach (UnityAction<float> listener in floatListeners[eventName])
        {
            invoker.AddListener(eventName, listener);
        }
        floatInvokers[eventName].Add(invoker);
    }

    public static void AddListener(EventName eventName, UnityAction<float> listener)
    {
        foreach (IFloatEventInvoker invoker in floatInvokers[eventName])
        {
            invoker.AddListener(eventName, listener);
        }
        floatListeners[eventName].Add(listener);

    }

    public static void AddInvoker(EventName eventName, IEventInvoker invoker)
    {
        foreach (UnityAction listener in Listeners[eventName])
        {
            invoker.AddListener(eventName, listener);
        }
        Invokers[eventName].Add(invoker);
    }

    public static void AddListener(EventName eventName, UnityAction listener)
    {
        foreach (IEventInvoker invoker in Invokers[eventName])
        {
            invoker.AddListener(eventName, listener);
        }
        Listeners[eventName].Add(listener);

    }

    public static void AddInvoker(EventName eventName, IDoubleFloatEventInvoker invoker)
    {
        foreach (UnityAction<float, float> listener in doubleFloatListeners[eventName])
        {
            invoker.AddListener(eventName, listener);
        }
        doubleFloatInvokers[eventName].Add(invoker);
    }

    public static void AddListener(EventName eventName, UnityAction<float, float> listener)
    {
        foreach (IDoubleFloatEventInvoker invoker in doubleFloatInvokers[eventName])
        {
            invoker.AddListener(eventName, listener);
        }
        doubleFloatListeners[eventName].Add(listener);
    }


    public static void RemoveInvoker(EventName eventName, IIntEventInvoker intEventInvoker)
    {
        intInvokers[eventName].Remove(intEventInvoker);
    }

    public static void RemoveInvoker(EventName eventName, IFloatEventInvoker floatEventInvoker)
    {
        floatInvokers[eventName].Remove(floatEventInvoker);
    }

    public static void RemoveInvoker(EventName eventName, IEventInvoker EventInvoker)
    {
        Invokers[eventName].Remove(EventInvoker);
    }

    public static void RemoveInvoker(EventName eventName, IDoubleFloatEventInvoker EventInvoker)
    {
        doubleFloatInvokers[eventName].Remove(EventInvoker);
    }



}
