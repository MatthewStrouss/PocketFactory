using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDictionary<TKey, TValue> : ScriptableObject
{
    private Dictionary<TKey, TValue> database = new Dictionary<TKey, TValue>();

}
