using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RecipeCanvasScript : MonoBehaviour
{
    [SerializeField] private Recipe recipe;
    [SerializeField] private Button recipeButton;
    [SerializeField] private GameObject recipeContent;
    [SerializeField] private GameObject resultObject;
    [SerializeField] private GameObject recipeDisplayPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRecipe(Recipe recipe, Action onClick)
    {
        this.recipe = recipe;

        this.SetRecipeContent();
        this.SetResultContent();

        if (onClick != null)
        {
            this.recipeButton.onClick.AddListener(() => onClick());
            this.recipeButton.gameObject.SetActive(true);
        }

        //Debug.Log(string.Format(
        //    "{0} = {1}",
        //    string.Join(" + ", this.recipe.Requirements.Select(x => x.name)),
        //    this.recipe.Result.name
        //    ));
    }

    public void SetRecipeContent()
    {
        foreach (Transform child in this.recipeContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        this.recipe.Requirements.ForEach(x =>
        {
            GameObject craftingResourcePrefab = Instantiate(this.recipeDisplayPrefab, this.recipeContent.transform);
            craftingResourcePrefab.GetComponent<ResourceCanvasScript>().SetResource(x);
        });
    }

    public void SetResultContent()
    {
        try
        { 
            this.resultObject.GetComponent<ResourceCanvasScript>().SetResource(this.recipe.Result);
        }
        catch (Exception ex)
        {

        }
    }
}
