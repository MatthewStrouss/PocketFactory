using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HydraulicPressController : MonoBehaviour, IMachineController
{
    public Transform resourceSpawnPosition;
    public Transform moveToPosition;

    public string recipeType = "Plate";

    [SerializeField]
    public List<Recipe> chosenRecipes;

    public int SpawnCount = 1;

    List<Resource> inventory;
    List<Resource> Inventory
    {
        get => this.inventory;
        set => this.inventory = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.Inventory = new List<Resource>();
        this.chosenRecipes = RecipeDatabase.Instance.GetRecipesForType(recipeType).Values.ToList();

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
            this.Inventory.Add(ResourceDatabase.Instance.resources[resourceToAdd.name]);
            resourceInInventory = this.Inventory.FirstOrDefault(y => y.name.Equals(resourceToAdd.name, StringComparison.OrdinalIgnoreCase));
            resourceInInventory.quantity = 0;
        }

        resourceInInventory.quantity++;
    }

    public void OnCollision(Collider2D col)
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
        GameObject go = Instantiate(PrefabDatabase.Instance.GetPrefab("Resource", "ResourcePrefab"), resourceSpawnPosition.position, Quaternion.Euler(transform.eulerAngles));

        go.GetComponent<SpriteRenderer>().sprite = SpriteDatabase.Instance.GetSprite("Resource", recipe.Result.name);
        ResourceController rc = go.GetComponent<ResourceController>();
        rc.SetResource(recipe.Result, SpawnCount);
        rc.Move(moveToPosition.position);
        rc.nextMoveToPosition = new Vector3(2f, 2f, 0f);
    }

    public Recipe RecipeFromInventory()
    {
        Recipe recipe = null;

        // First item in inventory that matches a recipe

        Resource firstInventoryItem = this.Inventory.FirstOrDefault(x => x.quantity > 0);

        if (firstInventoryItem != null)
        {
            recipe = this.chosenRecipes?.FirstOrDefault(x => x.Requirements.Contains(firstInventoryItem, Comparers.Instance.ResourceComparerInstance));
        }

        return recipe;
    }

    public void RemoveFromInventory(Resource resource)
    {
        this.Inventory.FirstOrDefault(x => x.id.Equals(resource.id)).quantity -= resource.quantity;
    }
}
