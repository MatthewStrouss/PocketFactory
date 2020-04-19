using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RecipeButtonScript : MonoBehaviour
{
    [SerializeField] private Button RecipeButton;
    [SerializeField] private GameObject ResourceCanvasNewPrefab;
    [SerializeField] private GameObject IngredientsScrollView;
    [SerializeField] private ResourceCanvasNew ResultResourceCanvas;

    private Recipe Recipe;
    private Action RecipeCallback;


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
        this.UpdateUI();
        this.gameObject.SetActive(true);
    }

    public void Activate(Recipe recipe, Action recipeCallback)
    {
        this.Recipe = recipe;
        this.RecipeCallback = recipeCallback;
        this.RecipeButton.onClick.AddListener(() => recipeCallback());
        this.Activate();
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
        foreach(Transform child in this.IngredientsScrollView.transform)
        {
            Destroy(child);
        }

        this.Recipe.Requirements.ForEach(x =>
        {
            GameObject newResource = Instantiate(this.ResourceCanvasNewPrefab, this.IngredientsScrollView.transform);
            newResource.GetComponent<ResourceCanvasNew>().Activate(x);
        });

        this.ResultResourceCanvas.Activate(this.Recipe.Result);
    }

    public bool RecipeContains(string data)
    {
        return this.Recipe.Result.name.IndexOf(data, StringComparison.InvariantCultureIgnoreCase) >= 0 ||
            this.Recipe.Requirements.Select(x => x.name).Any(x => x.IndexOf(data, StringComparison.InvariantCultureIgnoreCase) >= 0);
    }
}
