using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Research")]
public class ResearchScriptableObject : ScriptableObject
{
    [SerializeField] private ResearchEvent onResearchUpdate;

    [SerializeField] private bool isUnlocked;
    public bool IsUnlocked
    {
        get => this.isUnlocked;
        set
        {
            this.isUnlocked = value;
            onResearchUpdate?.Raise(this);
        }
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

    [SerializeField] private List<ResearchResultBase> researchResults;
    public List<ResearchResultBase> ResearchResults
    {
        get => this.researchResults;
        set => this.researchResults = value;
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

public enum ResearchTypeEnum
{
    MACHINE,
    UPGRADE,
    RECIPE
}