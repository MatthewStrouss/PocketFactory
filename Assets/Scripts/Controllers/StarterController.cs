using Assets;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StarterController : MonoBehaviour, IMachineController
{
    public MachineController MachineController;

    //[RangeAttribute(1, 3)]
    public int SpawnCount = 1;

    public string recipeType = "Basic";

    public Recipe ChosenRecipe;

    public Transform resourceSpawnPosition;
    public Transform moveToPosition;

    public GameObject starterGUI;

    void Awake()
    {
        this.ChosenRecipe = RecipeDatabase.GetRecipe(this.recipeType, "(None)");
        this.starterGUI = PrefabDatabase.Instance.GetPrefab("UI", "Starter");
    }

    // Start is called before the first frame update
    void Start()
    {
        //resourceType = PrimitiveResourceType.GOLD;
        //this.ChosenRecipe = RecipeDatabase.Instance.GetRecipe(recipeType, "(None)");
        //this.starterGUI = PrefabDatabase.Instance.GetPrefab("UI", "Starter");
        //InvokeRepeating("ActionToPerformOnTimer", 0.0f, 2.0f);
        //this.starterGUI = GameObject.Find("StarterCanvas");
    }

    public void CollisionEnter(Collider2D col)
    {

    }

    public void AddToInventory(Resource resourceToAdd)
    {
        throw new System.NotImplementedException();
    }

    public void ActionToPerformOnTimer()
    { 
        if ((this.ChosenRecipe != null) && (this.ChosenRecipe.Name != "(None)"))
        {
            this.MachineController.SubtractElectricityCost();
            GameObject go = Instantiate(PrefabDatabase.Instance.GetPrefab("Resource", "ResourcePrefab"), resourceSpawnPosition.position, Quaternion.Euler(transform.eulerAngles));
            //go.GetComponent<SpriteRenderer>().sprite = SpriteDatabase.Instance.GetSprite("Resource", ChosenRecipe.Result.name);
            go.GetComponent<SpriteRenderer>().sprite = ResourceDatabase.GetResource(ChosenRecipe.Result.name).Sprite;
            ResourceController rc = go.GetComponent<ResourceController>();
            rc.SetResource(ChosenRecipe.Result, SpawnCount);
            rc.Move(moveToPosition.position);
            rc.nextMoveToPosition = new Vector3(2f, 2f, 0f);
        }
    }

    public void SetRecipe(Recipe newRecipe)
    {
        this.ChosenRecipe = newRecipe;
        //GameManagerController.Instance.gUIManagerController.starterCanvas.GetComponent<StarterPanelScript>().UpdateUI(this);
        this.starterGUI.GetComponent<StarterPanelScript>().UpdateUI(this);
    }

    public void OnClick()
    {
        if (this.starterGUI == null)
        {
            this.starterGUI = PrefabDatabase.Instance.GetPrefab("UI", "Starter");
        }

        this.starterGUI.GetComponent<StarterPanelScript>().Activate();
        this.starterGUI.GetComponent<StarterPanelScript>().UpdateUI(this);
        //this.starterGUI.GetComponent<StarterCanvasScript>().Activate(this);// This is the new one
    }

    public void SetControllerValues(IMachineController other)
    {
        StarterController otherController = other as StarterController;
        this.SpawnCount = otherController.SpawnCount;
        this.ChosenRecipe = otherController.ChosenRecipe;
    }
}
