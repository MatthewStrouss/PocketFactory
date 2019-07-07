using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource
{
    public int cost;

    public int value;

    public int quantity;

    public bool isUnlocked;

    public int id;

    public string name;

    [JsonConstructor]
    public Resource(int value, int cost, int quantity, bool isUnlocked, int id, string name)
    {
        this.id = id;
        this.cost = cost;
        this.name = name;
        this.value = value;
        this.quantity = quantity;
        this.isUnlocked = isUnlocked;
    }

    public Resource(Resource resource)
    {
        this.id = resource.id;
        this.cost = resource.cost;
        this.name = resource.name;
        this.value = resource.value;
        this.quantity = resource.quantity;
        this.isUnlocked = resource.isUnlocked;
    }
}
