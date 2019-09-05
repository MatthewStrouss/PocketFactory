using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HydraulicPressController : MonoBehaviour, IMachineController
{
    public MachineController MachineController;

    public Transform resourceSpawnPosition;
    public Transform moveToPosition;

    public string recipeType = "Plate";

    [SerializeField]
    public List<Recipe> chosenRecipes;

    public int SpawnCount = 1;

    private List<Resource> inventory;
    public List<Resource> Inventory
    {
        get => this.inventory;
        set => this.inventory = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.Inventory = new List<Resource>();
        this.chosenRecipes = RecipeDatabase.GetRecipesForType(recipeType).Values.ToList();

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
        Resource resourceFromInventory = this.ResourceFromInventory();

        // Create resource
        if (resourceFromInventory != null)
        {
            this.CreateResource(resourceFromInventory);
        }
    }

    public void CreateResource(Resource resource)
    {
        this.MachineController.SubtractElectricityCost();
        GameObject go = Instantiate(PrefabDatabase.Instance.GetPrefab("Resource", "ResourcePrefab"), resourceSpawnPosition.position, Quaternion.Euler(transform.eulerAngles));

        go.GetComponent<SpriteRenderer>().sprite = SpriteDatabase.Instance.GetSprite("Resource", resource.name);
        ResourceController rc = go.GetComponent<ResourceController>();
        rc.SetResource(resource, resource.quantity);
        rc.Move(moveToPosition.position);
        rc.nextMoveToPosition = new Vector3(2f, 2f, 0f);
    }

    public Resource ResourceFromInventory()
    {
        Resource resource = null;

        // First item in inventory that matches a recipe

        Resource firstInventoryItem = this.Inventory.FirstOrDefault(x => x.quantity > 0);

        if (firstInventoryItem != null)
        {
            Recipe recipe = this.chosenRecipes?.FirstOrDefault(x => x.Requirements.Contains(firstInventoryItem, Comparers.Instance.ResourceComparerInstance));

            if (recipe == null)
            {
                resource = firstInventoryItem;
                this.RemoveFromInventory(resource);
            }
            else
            {
                resource = recipe.Result;
                recipe.Requirements.ForEach(x => this.RemoveFromInventory(x));
            }
        }

        return resource;
    }

    public void RemoveFromInventory(Resource resource)
    {
        this.Inventory.FirstOrDefault(x => x.id.Equals(resource.id)).quantity -= resource.quantity;
    }

    public void OnClick()
    {
        throw new NotImplementedException();
    }

    public void SetControllerValues(IMachineController other)
    {
        HydraulicPressController otherController = other as HydraulicPressController;
        this.Inventory = otherController.Inventory;
    }
}
