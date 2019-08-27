using Assets.Scripts.Interfaces;
using Malee;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "New Recipe")]
public class RecipeScriptableObject : ScriptableObject, IUnlockable
{
    [SerializeField] private bool isUnlocked;
    public bool IsUnlocked { get => this.isUnlocked; set => this.isUnlocked = value; }

    [SerializeField] private long unlockCost;
    public long UnlockCost { get => this.unlockCost; set => this.unlockCost = value; }

    [SerializeField] private RecipeRequirement result;
    public RecipeRequirement Result
    {
        get => this.result;
        set => this.result = value;
    }

    [SerializeField] private List<RecipeRequirement> requirements;
    public List<RecipeRequirement> Requirements
    {
        get => this.requirements;
        set => this.requirements = value;
    }

    [SerializeField] private Sprite sprite;
    public Sprite Sprite
    {
        get => this.sprite;
        set => this.sprite = value;
    }

    public string Name
    {
        get => this.name;
        set => this.name = value;
    }

    [SerializeField] private string type;
    public string Type
    {
        get => this.type;
        set => this.type = value;
    }

    public void Unlock()
    {
        this.IsUnlocked = true;
    }

    public void Lock()
    {
        this.IsUnlocked = false;
    }
}

[Serializable]
public struct RecipeRequirement
{
    [SerializeField] public ResourceScriptableObject requirement;
    [SerializeField] public long quantity;

    public RecipeRequirement(RecipeRequirement x) : this()
    {
        this.requirement = x.requirement;
        this.quantity = x.quantity;
    }
}