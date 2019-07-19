using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StarterController : MonoBehaviour, IMachineController, IPointerClickHandler
{
    [RangeAttribute(1, 3)]
    public int SpawnCount = 1;

    public string recipeType = "Basic";

    [SerializeField]
    public Recipe chosenRecipe;

    public Transform resourceSpawnPosition;
    public Transform moveToPosition;

    private bool isUnlocked;
    public bool IsUnlocked
    {
        get => isUnlocked;
        set => isUnlocked = value;
    }

    public GameObject starterGUI;

    // Start is called before the first frame update
    void Start()
    {
        //resourceType = PrimitiveResourceType.GOLD;
        this.chosenRecipe = RecipeDatabase.Instance.GetRecipe(recipeType, "(None)");
        this.starterGUI = PrefabDatabase.Instance.GetPrefab("UI", "Starter");
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
        //if (isActive())
        //{
        //    for (int i = 0; i < SpawnCount; ++i)
        //    {
        //        GameObject go = Instantiate(resource, transform.position + Vector3.forward, Quaternion.Euler(transform.eulerAngles));
        //        go.GetComponent<Resource>().setResourceType(resourceType);
        //    }
        //}

        if ((this.chosenRecipe != null) && (this.chosenRecipe.Name != "(None)"))
        {
            GameObject go = Instantiate(PrefabDatabase.Instance.GetPrefab("Resource", "ResourcePrefab"), resourceSpawnPosition.position, Quaternion.Euler(transform.eulerAngles));
            go.GetComponent<SpriteRenderer>().sprite = SpriteDatabase.Instance.GetSprite("Resource", chosenRecipe.Result.name);
            ResourceController rc = go.GetComponent<ResourceController>();
            rc.SetResource(chosenRecipe.Result, SpawnCount);
            rc.Move(moveToPosition.position);
            rc.nextMoveToPosition = new Vector3(2f, 2f, 0f);
        }
    }

    public void SetRecipe(Recipe newRecipe)
    {
        this.chosenRecipe = newRecipe;
        //GameManagerController.Instance.gUIManagerController.starterCanvas.GetComponent<StarterPanelScript>().UpdateUI(this);
        this.starterGUI.GetComponent<StarterPanelScript>().UpdateUI(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //GameManagerController.Instance.gUIManagerController.starterCanvas.GetComponent<StarterPanelScript>().Activate();
        //GameManagerController.Instance.gUIManagerController.starterCanvas.GetComponent<StarterPanelScript>().UpdateUI(this);
        this.starterGUI.GetComponent<StarterPanelScript>().Activate();
        this.starterGUI.GetComponent<StarterPanelScript>().UpdateUI(this);
    }
}
