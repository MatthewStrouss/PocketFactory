using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Databases/New Recipe SO Database")]
public class RecipeScriptableObjectDatabase : ScriptableObject
{
    [SerializeField] private List<RecipeSODatabaseKeyValuePair> keyValuePairs = new List<RecipeSODatabaseKeyValuePair>();
    public Dictionary<string, Dictionary<string, ScriptableObject>> database = new Dictionary<string, Dictionary<string, ScriptableObject>>();

    private void OnEnable()
    {
        //this.keyValuePairs.ForEach(x => this.database.Add(x.Key, x.Value));

        foreach(RecipeSODatabaseKeyValuePair recipeSODatabaseKeyValuePair in this.keyValuePairs)
        {
            Dictionary<string, ScriptableObject> itemType;

            if (!database.TryGetValue(recipeSODatabaseKeyValuePair.Key, out itemType))
            {
                database.Add(recipeSODatabaseKeyValuePair.Key, new Dictionary<string, ScriptableObject>());
                itemType = database[recipeSODatabaseKeyValuePair.Key];
            }

            foreach (SODatabaseKeyValuePair sODatabaseKeyValuePair in recipeSODatabaseKeyValuePair.Value)
            {
                if (!itemType.ContainsKey(sODatabaseKeyValuePair.Key))
                {
                    itemType.Add(sODatabaseKeyValuePair.Key, sODatabaseKeyValuePair.Value);
                }
            }
        }
    }
}

[Serializable]
public struct RecipeSODatabaseKeyValuePair
{
    [SerializeField] public string Key;
    [SerializeField] public List<SODatabaseKeyValuePair> Value;
}