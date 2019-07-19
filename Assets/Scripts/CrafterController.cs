using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Comparers;

public class CrafterController : MonoBehaviour, IMachineController
{
    public Transform resourceSpawnPosition;
    public Transform moveToPosition;

    public string recipeType = "Recipe";

    [SerializeField]
    public Recipe chosenRecipe;

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
        this.chosenRecipe = RecipeDatabase.Instance.GetRecipe(recipeType, "Circuit");
        //this.chosenRecipe = RecipeDatabase.Instance.GetRecipesForType(recipeType).Values.ToList();

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
        //rc.SetResource(chosenRecipe.Result, spawnQuantity);
        rc.Move(moveToPosition.position);
        rc.nextMoveToPosition = new Vector3(2f, 2f, 0f);
    }

    public Recipe RecipeFromInventory()
    {
        Recipe recipe = null;

        if (this.chosenRecipe != null)
        {
            List<Resource> inventoryItemsAbove0 = this.Inventory.Where(x => x.quantity > 0).ToList();
            int inventoryCount = inventoryItemsAbove0.Count;

            if (inventoryCount == this.chosenRecipe.Requirements.Count)
            {
                bool hasInventoryItems = true;

                for (int i = 0; i < inventoryCount; i++)
                {
                    hasInventoryItems &= hasInventoryItems &= this.chosenRecipe.Requirements.Contains(inventoryItemsAbove0[i], Comparers.Instance.RecipeComparerInstance);
                }

                if (hasInventoryItems)
                {
                    recipe = this.chosenRecipe;
                }
            }
        }

        return recipe;
    }

    public void RemoveFromInventory(Resource resource)
    {
        this.Inventory.FirstOrDefault(x => x.id.Equals(resource.id)).quantity -= resource.quantity;
    }
}
