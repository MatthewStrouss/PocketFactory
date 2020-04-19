using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FurnaceController : MonoBehaviour, IMachineController
{
    public MachineController MachineController;

    public Transform resourceSpawnPosition;
    public Transform moveToPosition;

    public string recipeType = "Liquid";

    [SerializeField]
    public List<Recipe> chosenRecipes;

    private Queue<Resource> inventory;
    public Queue<Resource> Inventory
    {
        get => this.inventory;
        set
        {
            this.inventory = value;
        }
    }

    [SerializeField] private int capacity;
    public int Capacity
    {
        get => this.capacity;
        set => this.capacity = value;
    }

    private GameObject furnaceGUI;

    private Resource nextItemToCraft;
    public Resource NextItemToCraft
    {
        get => this.nextItemToCraft;
        set => this.nextItemToCraft = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.Inventory = new Queue<Resource>();
        this.chosenRecipes = RecipeDatabase.GetRecipesForType(recipeType).Values.ToList();
        this.nextItemToCraft = this.ResourceFromInventory();

        //InvokeRepeating("ActionToPerformOnTimer", 0.0f, 2.0f); 
        this.furnaceGUI = PrefabDatabase.Instance.GetPrefab("UI", "MachineBaseCanvas").GetComponent<MachineMasterPanelScript>().FurnaceCanvas.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddToInventory(Resource resourceToAdd)
    {
        for (int i = 0; i < resourceToAdd.Quantity; i++)
        {
            if (this.Inventory.Count < this.Capacity)
            {
                this.Inventory.Enqueue(new Resource(resourceToAdd.id, 1));
            }
        }

        if (this.nextItemToCraft == null)
        {
            this.nextItemToCraft = this.ResourceFromInventory();
        }

        if (this.furnaceGUI.activeSelf && this.furnaceGUI.GetComponent<FurnaceCanvasScriptNewNew>().FurnaceController == this)
        {
            this.furnaceGUI.GetComponent<FurnaceCanvasScriptNewNew>().UpdateUI();
        }
    }

    public void CollisionEnter(Collider2D col)
    {
        this.AddToInventory(col.GetComponent<ResourceController>().resource);
        Destroy(col.gameObject);
    }

    public void ActionToPerformOnTimer()
    {
        // Create resource
        if (this.nextItemToCraft != null)
        {
            this.CreateResource(this.nextItemToCraft);
            this.Inventory.Dequeue();
            this.nextItemToCraft = this.ResourceFromInventory();
        }

        if (this.furnaceGUI.activeSelf && this.furnaceGUI.GetComponent<FurnaceCanvasScriptNewNew>().FurnaceController == this)
        {
            this.furnaceGUI.GetComponent<FurnaceCanvasScriptNewNew>().UpdateUI();
        }
    }

    public void CreateResource(Resource resource)
    {
        this.MachineController.SubtractElectricityCost();
        GameObject go = Instantiate(PrefabDatabase.Instance.GetPrefab("Resource", "ResourcePrefab"), resourceSpawnPosition.position, Quaternion.Euler(transform.eulerAngles));

        go.GetComponent<SpriteRenderer>().sprite = resource.Sprite;
        ResourceController rc = go.GetComponent<ResourceController>();
        rc.SetResource(resource, resource.Quantity);
        rc.Move(moveToPosition.position);
        rc.nextMoveToPosition = new Vector3(2f, 2f, 0f);
    }

    public Resource ResourceFromInventory()
    {
        Resource resource = null;

        if (this.Inventory.Any())
        {
            Resource itemInInventory = this.Inventory.FirstOrDefault();

            if (itemInInventory != null)
            {
                Recipe recipe = this.chosenRecipes?.FirstOrDefault(x => x.Requirements.Contains(itemInInventory, Comparers.Instance.ResourceComparerInstance));

                if (recipe == null)
                {
                    resource = itemInInventory;
                }
                else
                {
                    resource = recipe.Result;
                }
            }
        }

        return resource;
    }

    public void SetControllerValues(IMachineController other)
    {
        FurnaceController otherController = other as FurnaceController;
        this.Inventory = otherController.Inventory;
    }
}
