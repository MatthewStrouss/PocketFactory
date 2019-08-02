using Assets;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StarterController : MonoBehaviour, IMachineController
{
    public MachineController MachineController;

    [RangeAttribute(1, 3)]
    public int SpawnCount = 1;

    public string recipeType = "Basic";

    public Recipe ChosenRecipe;

    public Transform resourceSpawnPosition;
    public Transform moveToPosition;

    public GameObject starterGUI;

    void Awake()
    {
        this.ChosenRecipe = RecipeDatabase.Instance.GetRecipe(recipeType, "(None)");
        this.starterGUI = PrefabDatabase.Instance.GetPrefab("UI", "Starter");
        Debug.Log(string.Format("StarterController.Awake was called. Setting starterGUI to {0} and its final value is {1}", 
            PrefabDatabase.Instance.GetPrefab("UI", "Starter")?.name ?? "null",
            this.starterGUI?.name ?? "null"
            ));
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

    public void OnCollision(Collider2D col)
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
            go.GetComponent<SpriteRenderer>().sprite = SpriteDatabase.Instance.GetSprite("Resource", ChosenRecipe.Result.name);
            ResourceController rc = go.GetComponent<ResourceController>();
            rc.SetResource(ChosenRecipe.Result, SpawnCount);
            rc.Move(moveToPosition.position);
            rc.nextMoveToPosition = new Vector3(2f, 2f, 0f);
        }
    }

    public void SetRecipe(Recipe newRecipe)
    {
        Debug.Log(string.Format("Setting starter recipe to: {0}", newRecipe.Name));
        this.ChosenRecipe = newRecipe;
        //GameManagerController.Instance.gUIManagerController.starterCanvas.GetComponent<StarterPanelScript>().UpdateUI(this);
        this.starterGUI.GetComponent<StarterPanelScript>().UpdateUI(this);
    }

    public void OnClick()
    {
        Debug.Log(string.Format("You clicked on the starter. The UI has a value of {0}", this.starterGUI?.name ?? "null"));

        if (this.starterGUI == null)
        {
            this.starterGUI = PrefabDatabase.Instance.GetPrefab("UI", "Starter");
            Debug.Log(string.Format("The UI *NOW* has a value of {0}", this.starterGUI?.name ?? "null"));
        }

        this.starterGUI.GetComponent<StarterPanelScript>().Activate();
        this.starterGUI.GetComponent<StarterPanelScript>().UpdateUI(this);
    }

    public void SetControllerValues(IMachineController other)
    {
        StarterController otherController = other as StarterController;
        this.SpawnCount = otherController.SpawnCount;
        this.ChosenRecipe = otherController.ChosenRecipe;
    }
}
