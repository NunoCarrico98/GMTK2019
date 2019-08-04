// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[SerializeField]
public class GenericEvent<T> : UnityEvent<T> { }

public abstract class RuntimeSet<T> : ScriptableObject
{
    public List<T> Items = new List<T>();

    public void Add(T thing)
    {
        if (!Items.Contains(thing))
        {
            Items.Add(thing);
            OnAdd(thing);
            OnUpdate(thing);
        }
    }

    public void Remove(T thing)
    {
        if (Items.Contains(thing))
        {
            Items.Remove(thing);
            OnRemove(thing);
            OnUpdate(thing);
        }
    }

    public abstract void OnAdd(T thing);
    public abstract void OnRemove(T thing);
    public abstract void OnUpdate(T thing);
}