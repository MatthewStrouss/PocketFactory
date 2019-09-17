using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BlueprintDatabase
{
    public static Dictionary<string, object> blueprints = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);

    static BlueprintDatabase()
    {
        //blueprints = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(Resources.Load(@"").ToString());

        blueprints.Add("Circuit 3/s", new Blueprint("Circuit 3/s", "[{\"MachineName\":\"Starter\",\"Pos\":[-1,0,-8],\"SpawnCount\":1,\"ChosenRecipe\":\"(None)\"},{\"MachineName\":\"Starter\",\"Pos\":[0,-1,-8],\"SpawnCount\":1,\"ChosenRecipe\":\"(None)\"},{\"MachineName\":\"Starter\",\"Pos\":[0,1,-8],\"SpawnCount\":1,\"ChosenRecipe\":\"(None)\"},{\"MachineName\":\"Starter\",\"Pos\":[0,0,-8],\"SpawnCount\":1,\"ChosenRecipe\":\"(None)\"},{\"MachineName\":\"Starter\",\"Pos\":[1,0,-8],\"SpawnCount\":1,\"ChosenRecipe\":\"(None)\"}]"));
    }
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
