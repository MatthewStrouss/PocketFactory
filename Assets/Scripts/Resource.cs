using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Resource
{
    public long cost;

    public long value;

    public long quantity;

    public long id;

    public string name;
    private RecipeRequirement result;

    [JsonConstructor]
    public Resource(long value, long cost, long quantity, long id, string name)
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

    public Resource(RecipeRequirement resource)
    {
        //this.id = resource.requirement.id;
        this.cost = resource.requirement.Cost;
        this.name = resource.requirement.name;
        this.value = resource.requirement.Value;
        this.quantity = resource.quantity;
    }
}
