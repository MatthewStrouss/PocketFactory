using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Resource
{
    public int cost;

    public int value;

    public int quantity;

    public int id;

    public string name;

    [JsonConstructor]
    public Resource(int value, int cost, int quantity, int id, string name)
    {
        this.id = id;
        this.cost = cost;
        this.name = name;
        this.value = value;
        this.quantity = quantity;
    }

    public Resource(Resource resource)
    {
        this.id = resource.id;
        this.cost = resource.cost;
        this.name = resource.name;
        this.value = resource.value;
        this.quantity = resource.quantity;
    }
}
