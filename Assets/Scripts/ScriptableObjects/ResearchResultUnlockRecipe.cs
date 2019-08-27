using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Research Results/New Unlock Recipe")]
[System.Serializable]
public class ResearchResultUnlockRecipe : ResearchResultBase
{
    [SerializeField] public RecipeScriptableObject recipeToUnlock;
}
