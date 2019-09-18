using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BlueprintDatabase
{
    public static Dictionary<string, object> database = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);

    static BlueprintDatabase()
    {
        database.Add("Circuit 3/s", new Blueprint("Circuit 3/s", "[{\"MachineName\":\"Starter\",\"Pos\":[-1,0,-8],\"SpawnCount\":1,\"ChosenRecipe\":\"(None)\"},{\"MachineName\":\"Starter\",\"Pos\":[0,-1,-8],\"SpawnCount\":1,\"ChosenRecipe\":\"(None)\"},{\"MachineName\":\"Starter\",\"Pos\":[0,1,-8],\"SpawnCount\":1,\"ChosenRecipe\":\"(None)\"},{\"MachineName\":\"Starter\",\"Pos\":[0,0,-8],\"SpawnCount\":1,\"ChosenRecipe\":\"(None)\"},{\"MachineName\":\"Starter\",\"Pos\":[1,0,-8],\"SpawnCount\":1,\"ChosenRecipe\":\"(None)\"}]"));
    }

    public static void OverwriteFromSave(string json)
    {
        database = RecreateBlueprintLibrary(json);
    }

    private static Dictionary<string, object> RecreateBlueprintLibrary(string json)
    {
        Dictionary<string, object> newDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        Dictionary<string, object> returnDict = new Dictionary<string, object>();

        foreach (KeyValuePair<string, object> kvp in newDict)
        {
            Blueprint blueprint = Newtonsoft.Json.JsonConvert.DeserializeObject<Blueprint>(kvp.Value.ToString());

            if (string.IsNullOrWhiteSpace(blueprint.Name))
            {
                returnDict.Add(kvp.Key, RecreateBlueprintLibrary(kvp.Value.ToString()));
            }
            else
            {
                returnDict.Add(kvp.Key, blueprint);
            }
        }

        return returnDict;
    }

    //public static void Add(string key, object value, List<string> keys)
    //{
    //    Dictionary<string, object> currentLevel = database;

    //    foreach (string key in keys)
    //    {
    //        database[key]
    //    }
    //}
}

public class Blueprint
{
    private string name;
    public string Name
    {
        get => this.name;
        set => this.name = value;
    }

    private string paste;
    public string Paste
    {
        get => this.paste;
        set => this.paste = value;
    }

    private List<BlueprintRequirements> blueprintRequirements;
    public List<BlueprintRequirements> BlueprintRequirements
    {
        get => this.blueprintRequirements;
        set => this.blueprintRequirements = value;
    }

    public Blueprint()
    {

    }

    public Blueprint(string name, string paste)
    {
        this.Name = name;
        this.Paste = paste;
    }

    public Blueprint(Blueprint other)
    {
        this.Name = other.Name;
        this.Paste = other.Paste;
    }
}

public class BlueprintRequirements
{
    private string machine;
    public string Machine
    {
        get => this.machine;
        set => this.machine = value;
    }

    private long quantity;
    public long Quantity
    {
        get => this.quantity;
        set => this.quantity = value;
    }

    public BlueprintRequirements()
    {

    }

    public BlueprintRequirements(string machine, long quantity)
    {
        this.Machine = machine;
        this.Quantity = quantity;
    }
}
