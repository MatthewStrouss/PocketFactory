using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrafterCanvasScript : MonoBehaviour
{
    public GameObject chosenImage;
    public CrafterController crafter;
    public GameObject recipePanel;
    public GameObject craftingRecipePanel;
    public GameObject inventoryContent;

    public GameObject craftingRecipeResourcePrefab;

    public GameObject recipeCanvas;

    [SerializeField] private CancelCanvasScript XButton;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void XButton_Click()
    {
        this.Deactivate();
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
        this.XButton.Activate(this.XButton_Click);
    }

    public void Deactivate()
    {
        this.crafter = null;
        this.gameObject.SetActive(false);
        this.XButton.Deactivate();
    }

    public void RecipeButton_Click()
    {
        this.gameObject.SetActive(false);

        foreach (Transform child in craftingRecipePanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        this.recipePanel.GetComponent<RecipesPanelScript>().Activate("Recipe", (r) =>
        {
        this.crafter.SetRecipe(r);

            //r.Requirements.ForEach(x =>
            //{
            //    GameObject craftingResourcePrefab = Instantiate(this.craftingRecipeResourcePrefab, this.craftingRecipePanel.transform);
            //    craftingRecipeResourcePrefab.GetComponent<ResourceCanvasScript>().SetResource(x);
            //    craftingRecipeResourcePrefab.SetActive(true);
            //});

            //this.recipeCanvas.GetComponent<RecipeCanvasScript>().SetRecipe(r, null);

            this.Activate();
        });
    }

    public void UpdateUI(CrafterController starter)
    {
        this.crafter = starter;
        //StarterController sc = this.starter.GetComponent<StarterController>();
        //this.chosenImage.GetComponent<Image>().sprite = SpriteDatabase.Instance.GetSprite("Resource", this.crafter.ChosenRecipe.Result.name);
        //this.chosenImage.GetComponent<ResourceCanvasScript>().SetResource(this.crafter.ChosenRecipe.Result);
        this.recipeCanvas.GetComponent<RecipeCanvasScript>().SetRecipe(RecipeDatabase.GetRecipe("Recipe", "(None)"), null);
        this.recipeCanvas.GetComponent<RecipeCanvasScript>().SetRecipe(this.crafter.ChosenRecipe, null);

        //foreach (Transform child in this.inventoryContent.transform)
        //{
        //    GameObject.Destroy(child.gameObject);
        //}

        //this.crafter.Inventory.ForEach(x =>
        //{
        //    GameObject craftingResourcePrefab = Instantiate(this.craftingRecipeResourcePrefab, this.inventoryContent.transform);
        //    craftingRecipeResourcePrefab.GetComponent<ResourceCanvasScript>().SetResource(x);
        //    craftingRecipeResourcePrefab.SetActive(true);
        //});
    }
}
