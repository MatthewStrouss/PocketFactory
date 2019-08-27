using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "New Research2")]
public class ResearchScriptableObject2 : ScriptableObject
{
    [SerializeField] private bool isUnlocked;
    public bool IsUnlocked
    {
        get => this.isUnlocked;
        set => this.isUnlocked = value;
    }

    [SerializeField] private ResearchTypeEnum researchType;
    public ResearchTypeEnum ResearchType
    {
        get => this.researchType;
        set => this.researchType = value;
    }

    [SerializeField] private string name;
    public string Name
    {
        get => this.name;
        set => this.name = value;
    }

    [SerializeField] private long cost;
    public long Cost
    {
        get => this.cost;
        set => this.cost = value;
    }

    [SerializeField] private Sprite sprite;
    public Sprite Sprite
    {
        get => this.sprite;
        set => this.sprite = value;
    }

    [SerializeField] private string description;
    public string Description
    {
        get => this.description;
        set => this.description = value;
    }

    public void Unlock()
    {
        this.isUnlocked = true;
    }

    public void Lock()
    {
        this.isUnlocked = false;
    }

    [SerializeField] public UnityEvent action;
}