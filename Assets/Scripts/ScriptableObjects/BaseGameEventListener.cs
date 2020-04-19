using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseGameEventListener<T, E, UER> : MonoBehaviour,
    IGameEventListener<T> where E: BaseGameEvent<T> where UER : UnityEvent<T>
{
    [SerializeField] private E gameEvent;
    public E GameEvent
    {
        get
        {
            return this.gameEvent;
        }
        set
        {
            this.gameEvent = value;
        }
    }

    [SerializeField] private UER unityEventResponse;

    private void OnEnable()
    {
        if (this.gameEvent == null)
            return;

        this.GameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        if (this.gameEvent == null)
            return;

        this.GameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(T item)
    {
        this.unityEventResponse?.Invoke(item);
    }
}

