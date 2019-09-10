using Assets.Scripts.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ResearchDatabase
{

    public static Dictionary<string, Dictionary<string, Research>> database = new Dictionary<string, Dictionary<string, Research>>(StringComparer.InvariantCultureIgnoreCase)
    {
        { "Machine", new Dictionary<string, Research>(StringComparer.InvariantCultureIgnoreCase)},
        { "Upgrade", new Dictionary<string, Research>(StringComparer.InvariantCultureIgnoreCase)},
        { "Recipe", new Dictionary<string, Research>(StringComparer.InvariantCultureIgnoreCase)}
    };


    static ResearchDatabase()
    {
        if (database.TryGetValue("Machine", out Dictionary<string, Research> machineDict))
        {
            foreach (KeyValuePair<string, ScriptableObject> machine in (Resources.Load(@"ScriptableObjects/ResearchDatabaseMachine", typeof(ScriptableObjectDatabase)) as ScriptableObjectDatabase).database)
            {
                machineDict.Add(machine.Key, new Research(machine.Value as IResearch));
            }
        }

        if (database.TryGetValue("Upgrade", out Dictionary<string, Research> upgradeDict))
        {
            foreach (KeyValuePair<string, ScriptableObject> upgrade in (Resources.Load(@"ScriptableObjects/ResearchDatabaseUpgrade", typeof(ScriptableObjectDatabase)) as ScriptableObjectDatabase).database)
            {
                upgradeDict.Add(upgrade.Key, new Research(upgrade.Value as IResearch));
            }
        }

        if (database.TryGetValue("Recipe", out Dictionary<string, Research> recipeDict))
        {
            foreach (KeyValuePair<string, ScriptableObject> recipe in (Resources.Load(@"ScriptableObjects/ResearchDatabaseRecipe", typeof(ScriptableObjectDatabase)) as ScriptableObjectDatabase).database)
            {
                recipeDict.Add(recipe.Key, new Research(recipe.Value as IResearch));
            }
        }
    }

    internal static void OverwriteFromSave(Dictionary<string, Dictionary<string, Research>> researchDatabase)
    {
        foreach(KeyValuePair<string, Dictionary<string, Research>> researchType in researchDatabase)
        {
            if (database.TryGetValue(researchType.Key, out Dictionary<string, Research> researchTypeDict))
            {
                foreach (KeyValuePair<string, Research> researchItem in researchType.Value)
                {
                    if (researchTypeDict.TryGetValue(researchItem.Key, out Research existingResearch))
                    {
                        if (researchItem.Value.IsUnlocked)
                        {
                            existingResearch.Unlock();
                            //existingResearch.IsUnlocked = true;
                        }
                        //existingResearch
                    }
                }
            }
            else
            {
                //database.Add("Recipe", new Dictionary<string, Research>(StringComparer.InvariantCultureIgnoreCase));
            }
        }
    }

    public static long GetRemainingResearchCost()
    {
        long total = 0;

        foreach (KeyValuePair<string, Dictionary<string, Research>> researchType in database)
        {
            total += researchType.Value.Values.Where(x => !x.IsUnlocked).Sum(x => x.Cost);
        }

        return total;
    }
}

[Serializable]
public class Research
{
    [SerializeField] private bool isUnlocked;
    public bool IsUnlocked
    {
        get => this.isUnlocked;
        set
        {
            this.isUnlocked = value;
            //onResearchUpdate?.Raise(this);
        }
    }

    [SerializeField] private string name;
    [JsonIgnore]
    public string Name
    {
        get => this.name;
        set => this.name = value;
    }

    [SerializeField] private long cost;
    [JsonIgnore]
    public long Cost
    {
        get => this.cost;
        set => this.cost = value;
    }

    [SerializeField] private Sprite sprite;
    [JsonIgnore]
    public Sprite Sprite
    {
        get => this.sprite;
        set => this.sprite = value;
    }

    [SerializeField] private string description;
    [JsonIgnore]
    public string Description
    {
        get => this.description;
        set => this.description = value;
    }

    [SerializeField] private List<Action> unlockEvent;
    [JsonIgnore]
    public List<Action> UnlockEvent
    {
        get;
        set;
    }

    public Research()
    { }

    public Research(Research otherResearch)
    {
        this.IsUnlocked = otherResearch.IsUnlocked;
        this.Name = otherResearch.Name;
        this.Cost = otherResearch.Cost;
        this.Sprite = otherResearch.Sprite;
        this.Description = otherResearch.Description;
        this.UnlockEvent = new List<Action>();
    }

    public Research(IResearch otherResearch)
    {
        this.IsUnlocked = otherResearch.IsUnlocked;
        this.Name = otherResearch.Name;
        this.Cost = otherResearch.UnlockCost;
        this.Sprite = otherResearch.Sprite;
        this.Description = otherResearch.Description;
        this.UnlockEvent = new List<Action>();
        this.UnlockEvent.Add(otherResearch.Unlock);
    }

    public void Unlock()
    {
        this.IsUnlocked = true;
        this.UnlockEvent.ForEach(x => x.DynamicInvoke());
    }
}