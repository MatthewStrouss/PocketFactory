using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RecipesPanelScript : MonoBehaviour
{
    public GameObject content;
    public Button buttonPrefab;
    public Action<Recipe> callback;

    public GameObject recipePrefab;
    public GameObject resourcePrefab;

    public GameObject recipeCanvasPrefab;

    public Button unlockButtonPrefab;

    public string recipeType;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Activate(string recipeType, Action<Recipe> callback)
    {
        this.recipeType = recipeType;
        this.callback = callback;
        this.UpdateUI();
        this.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void Deactivate(Recipe chosenRecipe)
    {
        this.callback(chosenRecipe);
        this.gameObject.SetActive(false);
    }

    public void XButton_Click()
    {
        this.Deactivate();
    }

    public void UpdateUI()
    {
        foreach (Transform child in this.content.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        Dictionary<string, Recipe> basicRecipes = RecipeDatabase.Instance.GetRecipesForType(this.recipeType);

        foreach (KeyValuePair<string, Recipe> recipe in basicRecipes)
        {
            Button newButton;

            if (recipe.Value.IsUnlocked)
            {
                //Debug.Log(string.Format("{0} = {1}",
                //    string.Join(" + ", recipe.Value.Requirements.Select(x => x.name).ToArray()),
                //    recipe.Value.Result.name
                //    ));
                newButton = Instantiate<Button>(this.recipeCanvasPrefab.transform.Find("Button").GetComponent<Button>(), this.content.transform);
                newButton.GetComponent<RecipeCanvasScript>().SetRecipe(recipe.Value, () => this.Deactivate(recipe.Value));
            }
            else
            {
                newButton = Instantiate<Button>(this.unlockButtonPrefab, this.content.transform);
                newButton.transform.Find("CostText").GetComponent<Text>().text = string.Format("${0}", recipe.Value.UnlockCost);
                newButton.transform.Find("NameText").GetComponent<Text>().text = recipe.Value.Name;

                newButton.onClick.AddListener(() =>
                {
                    RecipeDatabase.Instance.UnlockRecipe(recipe.Value);
                    this.UpdateUI();
                });
            }
        }
    }
}
