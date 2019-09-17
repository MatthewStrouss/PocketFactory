using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Comparers;

public class CrafterController : MonoBehaviour, IMachineController
{
    public MachineController MachineController;

    public Transform resourceSpawnPosition;
    public Transform moveToPosition;

    public string recipeType = "Recipe";

    [SerializeField]
    public Recipe ChosenRecipe;

    private List<Resource> inventory;
    public List<Resource> Inventory
    {
        get => this.inventory;
        set => this.inventory = value;
    }

    public GameObject crafterGUI;

    // Start is called before the first frame update
    void Start()
    {
        this.Inventory = new List<Resource>();

        if (string.IsNullOrWhiteSpace(this.ChosenRecipe.Name))
        {
            this.ChosenRecipe = RecipeDatabase.GetRecipe(this.recipeType, "(None)");
        }
        //this.chosenRecipe = RecipeDatabase.Instance.GetRecipesForType(recipeType).Values.ToList();
        this.crafterGUI = PrefabDatabase.Instance.GetPrefab("UI", "Crafter");

        //InvokeRepeating("ActionToPerformOnTimer", 0.0f, 2.0f); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddToInventory(Resource resourceToAdd)
    {
        Resource resourceInInventory = this.Inventory.FirstOrDefault(y => y.name.Equals(resourceToAdd.name, StringComparison.InvariantCultureIgnoreCase));

        if (resourceInInventory?.name == null)
        {
            this.Inventory.Add(new Resource(ResourceDatabase.GetResource(resourceToAdd.name)));
            resourceInInventory = this.Inventory.FirstOrDefault(y => y.name.Equals(resourceToAdd.name, StringComparison.OrdinalIgnoreCase));
            resourceInInventory.Quantity = 0;
        }

        resourceInInventory.Quantity++;

        this.UpdateUI();
    }

    public void CollisionEnter(Collider2D col)
    {
        this.AddToInventory(col.GetComponent<ResourceController>().resource);
        Destroy(col.gameObject);
    }

    public void ActionToPerformOnTimer()
    {
        // Check if inventory contains all resources in recipe
        Recipe recipeFromInventory = this.RecipeFromInventory();

        // Subtract recipe resources from inventory
        recipeFromInventory?.Requirements.ForEach(x => this.RemoveFromInventory(x));

        // Create resource
        if (recipeFromInventory != null) // TODO change this because this if statement is gross
        {
            this.CreateResourceFromRecipe(recipeFromInventory);
        }
    }

    public void CreateResourceFromRecipe(Recipe recipe)
    {
        this.MachineController.SubtractElectricityCost();
        GameObject go = Instantiate(PrefabDatabase.Instance.GetPrefab("Resource", "ResourcePrefab"), resourceSpawnPosition.position, Quaternion.Euler(transform.eulerAngles));

        go.GetComponent<SpriteRenderer>().sprite = recipe.Result.Sprite;
        ResourceController rc = go.GetComponent<ResourceController>();
        rc.SetResource(recipe.Result, recipe.Result.Quantity);
        rc.Move(moveToPosition.position);
        rc.nextMoveToPosition = new Vector3(2f, 2f, 0f);
    }

    public Recipe RecipeFromInventory()
    {
        Recipe recipe = null;

        if (this.ChosenRecipe != null && this.ChosenRecipe.Name != "(None)")
        {
            List<Resource> inventoryItemsAbove0 = this.Inventory.Where(x => x.Quantity > 0).ToList();
            int inventoryCount = inventoryItemsAbove0.Count;

            if (inventoryCount == this.ChosenRecipe.Requirements.Count)
            {
                bool hasInventoryItems = true;

                for (int i = 0; i < inventoryCount; i++)
                {
                    hasInventoryItems &= hasInventoryItems &= this.ChosenRecipe.Requirements.Contains(inventoryItemsAbove0[i], Comparers.Instance.RecipeComparerInstance);
                }

                if (hasInventoryItems)
                {
                    recipe = this.ChosenRecipe;
                }
            }
        }

        return recipe;
    }

    public void RemoveFromInventory(Resource resource)
    {
        this.Inventory.FirstOrDefault(x => x.id.Equals(resource.id)).Quantity -= resource.Quantity;
    }

    public void OnClick()
    {
        this.crafterGUI.GetComponent<CrafterCanvasScript>().UpdateUI(this);
        this.crafterGUI.GetComponent<CrafterCanvasScript>().Activate();
    }

    public void SetRecipe(Recipe newRecipe)
    {
        this.ChosenRecipe = newRecipe;
        this.crafterGUI.GetComponent<CrafterCanvasScript>().UpdateUI(this);
        //GameManagerController.Instance.gUIManagerController.starterCanvas.GetComponent<StarterPanelScript>().UpdateUI(this);
        //this.starterGUI.GetComponent<StarterPanelScript>().UpdateUI(this);
    }

    public void SetControllerValues(IMachineController other)
    {
        CrafterController otherController = other as CrafterController;
        this.ChosenRecipe = otherController.ChosenRecipe;
        this.Inventory = otherController.Inventory;
    }

    public void UpdateUI()
    {
        if (this.crafterGUI.activeSelf && this.crafterGUI.GetComponent<CrafterCanvasScript>().crafter == this)
        {
            this.crafterGUI.GetComponent<CrafterCanvasScript>().UpdateUI(this);
        }
    }
}