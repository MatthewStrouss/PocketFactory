using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchDatabase
{

    public Dictionary<string, Dictionary<string, ResearchScriptableObject>> research = new Dictionary<string, Dictionary<string, ResearchScriptableObject>>();

    private static ResearchDatabase instance;
    public static ResearchDatabase Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ResearchDatabase();
            }

            return instance;
        }
    }

    public ResearchDatabase()
    {
        this.RegisterMachineResearch();
    }

    public void RegisterMachineResearch()
    {

    }

    public void RegisterResearch(string researchType, string researchName, ResearchScriptableObject researchScriptableObject)
    {
        if (this.research.TryGetValue(researchType, out Dictionary<string, ResearchScriptableObject> existingType))
        {
            if (existingType.TryGetValue(researchName, out ResearchScriptableObject existingResearchScriptableObject))
            {
                Debug.Log(string.Format("Trying to insert {0} into {1}/{2}, but it already exists in ResearchDatabase", existingResearchScriptableObject.Name, researchType, researchName));
            }
            else
            {
                existingType.Add(researchName, researchScriptableObject);
            }
        }
        else
        {
            this.research.Add(researchType, new Dictionary<string, ResearchScriptableObject>());
            this.research[researchType].Add(researchName, researchScriptableObject);
        }
    }
}
