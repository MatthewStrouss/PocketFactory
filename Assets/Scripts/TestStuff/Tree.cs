using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private RecipeScriptableObject recipeScriptableObject;

    private void Awake()
    {
        //string strToPrint = this.PrintTree(this.recipeScriptableObject, "", true, 1);

        //Debug.Log(strToPrint);
    }

    private string PrintTree(RecipeScriptableObject recipeScriptableObject, string indent, bool last, long quantity)
    {
        string strToReturn = string.Format("{0}+- {1} {2}", indent, quantity, recipeScriptableObject.Name);
        indent += last ? "   " : "|  ";

        //for (int i = 0; i < recipeScriptableObject.Requirements.Count; i++)
        //{
        //    strToReturn = string.Join(Environment.NewLine, strToReturn, PrintTree(recipeScriptableObject.Requirements[i].requirement, indent, i == recipeScriptableObject.Requirements.Count - 1, recipeScriptableObject.Requirements[i].quantity));
        //}

        return strToReturn;
    }
}
