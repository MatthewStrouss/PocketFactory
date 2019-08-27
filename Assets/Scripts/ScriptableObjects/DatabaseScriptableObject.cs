using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Database")]
public class DatabaseScriptableObject : ScriptableObject
{
    [SerializeField] private List<KeyValuePairResource> keyValuePairs = new List<KeyValuePairResource>();
    public Dictionary<string, ScriptableObject> database = new Dictionary<string, ScriptableObject>();

    private void OnEnable()
    {
        this.keyValuePairs.ForEach(x => this.database.Add(x.key, x.value));
    }
}

[Serializable]
public struct KeyValuePairResource
{
    [SerializeField] public string key;
    [SerializeField] public ScriptableObject value;
}