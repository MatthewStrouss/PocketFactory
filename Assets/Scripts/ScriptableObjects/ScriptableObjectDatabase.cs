using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Databases/New SO Database")]
public class ScriptableObjectDatabase : ScriptableObject
{
    [SerializeField] private List<SODatabaseKeyValuePair> keyValuePairs = new List<SODatabaseKeyValuePair>();
    public Dictionary<string, ScriptableObject> database = new Dictionary<string, ScriptableObject>();

    private void OnEnable()
    {
        this.keyValuePairs.ForEach(x => this.database.Add(x.Key, x.Value));
    }
}

[Serializable]
public struct SODatabaseKeyValuePair
{
    [SerializeField] public string Key;
    [SerializeField] public ScriptableObject Value;
}