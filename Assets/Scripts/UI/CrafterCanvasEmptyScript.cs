using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CrafterCanvasEmptyScript : MonoBehaviour
{
    [SerializeField] private SearchBoxScript SearchBox;
    [SerializeField] private GameObject Content;
    [SerializeField] private Button RecipeButtonPrefab;
    [SerializeField] private InputField InputField;

    private CrafterController CrafterController;
    private Action Callback;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Activate()
    {
        this.SearchBox.Activate(this.OnSearchBox_Updated);
        this.UpdateUI();
        this.gameObject.SetActive(true);
    }

    public void Activate(CrafterController crafterController, Action callback)
    {
        this.CrafterController = crafterController;
        this.Callback = callback;
        this.Activate();
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
        this.Callback?.DynamicInvoke();
    }

    public void UpdateUI()
    {
        foreach (Transform child in this.Content.transform)
        {
            Destroy(child.gameObject);
        }

        RecipeDatabase.GetRecipesForType("Recipe").Values.ToList().ForEach(x => 
        {
            Button newButton = Instantiate(this.RecipeButtonPrefab, this.Content.transform);
            newButton.GetComponent<RecipeButtonScript>().Activate(x, () => this.RecipeButtonClicked_Callback(x));
        });
    }

    public void OnSearchBox_Updated(string searchString)
    {
        foreach (Transform child in this.Content.transform)
        {
            child.gameObject.SetActive(child.GetComponent<RecipeButtonScript>().RecipeContains(searchString));
        }
    }

    public void RecipeButtonClicked_Callback(Recipe recipeClicked)
    {
        this.CrafterController.ChosenRecipe = recipeClicked;
        this.Deactivate();
    }
}
