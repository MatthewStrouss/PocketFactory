using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameEventListener<T>
{
    void OnEventRaised(T item);
}
