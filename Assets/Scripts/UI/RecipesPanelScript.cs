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

    public Button recipeCanvasPrefab;

    public Button unlockButtonPrefab;

    public string recipeType;

    [SerializeField] private InputField FilterInputField;

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

        List<Recipe> basicRecipes = RecipeDatabase.GetRecipesForType(this.recipeType).Values.OrderBy(x => x.UnlockCost).ToList();

        basicRecipes.ForEach(x =>
        {
            Button newButton;
            newButton = Instantiate<Button>(this.recipeCanvasPrefab, this.content.transform);
            newButton.GetComponent<RecipeButtonScript>().Activate(x, () => this.Deactivate(x));
            //newButton.GetComponent<RecipeCanvasScript>().SetRecipe(x, () => this.Deactivate(x));
            newButton.interactable = x.IsUnlocked;
        });
    }

    public void FilterInputField_Updated()
    {
        foreach (Transform child in this.content.transform)
        {
            child.gameObject.SetActive(child.GetComponent<RecipeButtonScript>().RecipeContains(this.FilterInputField.text));
        }
    }
}
