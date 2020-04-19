using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameEvent<T> : ScriptableObject
{
    private readonly List<IGameEventListener<T>> eventListeners = new List<IGameEventListener<T>>();

    public void Raise(T item)
    {
        this.eventListeners.ForEach(x => x.OnEventRaised(item));
    }

    public void RegisterListener(IGameEventListener<T> listener)
    {
        if(!this.eventListeners.Contains(listener))
        {
            this.eventListeners.Add(listener);
        }
    }

    public void UnregisterListener(IGameEventListener<T> listener)
    {
        if (this.eventListeners.Contains(listener))
        {
            this.eventListeners.Remove(listener);
        }
    }
}
