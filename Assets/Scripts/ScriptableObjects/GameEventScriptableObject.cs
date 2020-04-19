using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class GameEventScriptableObject : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise()
    {
        this.listeners?.ForEach(x => x.OnEventRaised());
    }

    public void RegisterListener(GameEventListener listener)
    {
        this.listeners?.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (this.listeners?.Contains(listener) ?? false)
        {
            this.listeners?.Remove(listener);
        }
    }
}

public class GameEventListener : MonoBehaviour
{
    public GameEventScriptableObject Event;
    public UnityEvent Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        Response.Invoke();
    }
}