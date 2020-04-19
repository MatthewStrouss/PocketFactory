using Assets;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Resource
{
    private long cost;
    [JsonIgnore]
    public long Cost
    {
        get => this.cost;
        set => this.cost = value;
    }

    private long value;
    [JsonIgnore]
    public long Value
    {
        get => this.value;
        set => this.value = value;
    }

    [SerializeField] private long quantity;
    public long Quantity
    {
        get => this.quantity;
        set => this.quantity = value;
    }

    public long id;

    [JsonIgnore]
    public string name;

    private RecipeRequirement result;

    private Sprite sprite;
    [JsonIgnore]
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

    public Resource(long id, long quantity)
    {
        Resource other = ResourceDatabase.database.Values.FirstOrDefault(x => x.id == id);
        this.id = other.id;
        this.cost = other.cost;
        this.name = other.name;
        this.value = other.value;
        this.quantity = quantity;
        this.sprite = other.Sprite;
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
