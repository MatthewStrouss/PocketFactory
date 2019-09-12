using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Resource
{
    private long cost;
    public long Cost
    {
        get => this.cost;
        set => this.cost = value;
    }

    private long value;
    public long Value
    {
        get => this.value;
        set => this.value = value;
    }

    private long quantity;
    public long Quantity
    {
        get => this.quantity;
        set => this.quantity = value;
    }

    public long id;

    public string name;
    private RecipeRequirement result;

    private Sprite sprite;
    public Sprite Sprite
    {
        get => this.sprite;
        set => this.sprite = value;
    }

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
        this.sprite = resource.Sprite;
    }

    public Resource(RecipeRequirement resource)
    {
        this.id = resource.requirement.ID;
        this.cost = resource.requirement.Cost;
        this.name = resource.requirement.name;
        this.value = resource.requirement.Value;
        this.quantity = resource.quantity;
        this.sprite = resource.requirement.Sprite;
    }

    public Resource(ResourceScriptableObject other)
    {
        this.id = other.ID;
        this.cost = other.Cost;
        this.name = other.name;
        this.value = other.Value;
        this.quantity = other.Quantity;
        this.sprite = other.Sprite;
    }
}
