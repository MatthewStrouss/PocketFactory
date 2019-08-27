using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchActions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnlockRecipe(ResearchScriptableObject researchScriptableObject)
    {
        switch(researchScriptableObject.ResearchType)
        {
            case ResearchTypeEnum.MACHINE:
                break;
            case ResearchTypeEnum.UPGRADE:
                break;
            case ResearchTypeEnum.RECIPE:
                this.UnlockRecipeResearch(researchScriptableObject);
                break;
        }
    }

    public void UnlockRecipeResearch(ResearchScriptableObject researchScriptableObject)
    {
        switch (researchScriptableObject.Name.ToLower())
        {
            case "engine":
                // unlock engine code
                break;
        }
    }
}
