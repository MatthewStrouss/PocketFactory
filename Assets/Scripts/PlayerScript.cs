using Assets;
using Assets.Scripts;
using DigitalRubyShared;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public PlayerStateEnum playerStateEnum;

    public Resource resourceToSpawn;

    [SerializeField] private Text debugText;
    [SerializeField] private Camera cam;

    private bool settingMachine = false;

    private void Awake()
    {
        Debug.Log(string.Format("Screen resolution is: {0}x{1}", Screen.width, Screen.height));
        Debug.Log(string.Format("PersistentDataPath: {0}", Application.persistentDataPath));
        Debug.Log($"Started at {DateTime.Now.Ticks}");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        this.debugText.text = $"{GameObject.FindGameObjectsWithTag("Resource").Sum(x => x.GetComponent<ResourceController>().resource.Quantity)} resources";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.BackQuote) || Input.touchCount == 3)
        {
            PrefabDatabase.Instance.GetPrefab("UI", "Cheat").GetComponent<CheatCanvasScript>().Activate();
        }

        Vector3 mousePos = this.cam.ScreenToWorldPoint(new Vector3(Mathf.Round(Input.mousePosition.x), Mathf.Round(Input.mousePosition.x)));
        this.debugText.text = $"({mousePos.x}, {mousePos.y})";
    }

    //public void Copy()
    //{
    //    if (selectedObjects.Count > 0)
    //    {
    //        List<MachineModel> machineModels = selectedObjects.ToMachineModelList();
    //        string json = Newtonsoft.Json.JsonConvert.SerializeObject(machineModels, new Newtonsoft.Json.JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
    //        //string base64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(json));

    //        PrefabDatabase.Instance.GetPrefab("UI", "Copy").GetComponent<CopyCanvasScript>().Activate();
    //        PrefabDatabase.Instance.GetPrefab("UI", "Copy").GetComponent<CopyCanvasScript>().UpdateUI(json);
    //    }
    //}

    //public void Paste(string json)
    //{
    //    GameObject parent = new GameObject();

    //    //string json = Encoding.ASCII.GetString(Convert.FromBase64String(base64String));

    //    List<GameObject> gameObjects = json.ToGameObjectList();

    //    parent.transform.position = new Vector3(
    //        Mathf.Round(gameObjects.Sum(x => x.transform.position.x) / gameObjects.Count),
    //        Mathf.Round(gameObjects.Sum(x => x.transform.position.y) / gameObjects.Count),
    //        -8
    //        ); ;

    //    gameObjects.ForEach(x => x.transform.parent = parent.transform);

    //    IsPasteValid(parent.GetComponentsInChildren<MachineController>().ToList(), out bool isPasteValid, out string pasteInvalidString);

    //    if (isPasteValid)
    //    {
    //        this.playerStateEnum = PlayerStateEnum.PLACE_MACHINE_PASTE;
    //        this.machineToPlace = parent;
    //    }
    //    else
    //    {
    //        PrefabDatabase.Instance.GetPrefab("UI", "Error").GetComponent<ErrorCanvasScript>().Activate(pasteInvalidString);
    //    }
    //}

    //public void PasteUI()
    //{
    //    PrefabDatabase.Instance.GetPrefab("UI", "Paste").GetComponent<PasteCanvasScript>().Activate(Paste);
    //}

    //public void IsPasteValid(List<MachineController> machinesToPlace, out bool isPasteValid, out string pasteInvalidString)
    //{
    //    isPasteValid = true;
    //    pasteInvalidString = string.Empty;

    //    if (!CanAffordPaste(machinesToPlace))
    //    {
    //        isPasteValid = false;
    //        pasteInvalidString = "Not enough money for all machines in paste";
    //    }

    //    if (!HasUnlockedAllMachines(machinesToPlace))
    //    {
    //        isPasteValid = false;
    //        pasteInvalidString = "Not all machines in paste are unlocked";
    //    }


    //    if (!HasUnlockedAllRecipes(machinesToPlace))
    //    {
    //        isPasteValid = false;
    //        pasteInvalidString = "Not all recipes in paste are unlocked";
    //    }
    //}

    //public bool CanAffordPaste(List<MachineController> machinesToPlace)
    //{
    //    return Player.playerModel.Money >= machinesToPlace.Sum(x => x.Machine.BuildCost);
    //}

    //public bool HasUnlockedAllMachines(List<MachineController> machinesToPlace)
    //{
    //    return machinesToPlace.All(x => x.Machine.IsUnlocked);
    //}

    //public bool HasUnlockedAllRecipes(List<MachineController> machinesToPlace)
    //{
    //    return machinesToPlace
    //        .Where(x => x.controller is CrafterController)
    //        .Select(x => x.controller)
    //        .Cast<CrafterController>()
    //        .All(x => RecipeDatabase.GetRecipe(x.recipeType, x.ChosenRecipe.Name).IsUnlocked);
    //}

    public void SpawnResource(Resource resource)
    {
        this.resourceToSpawn = new Resource(resource);
        this.playerStateEnum = PlayerStateEnum.SPAWN_RESOURCE;
    }
}

public enum PlayerStateEnum
{
    NONE,
    PLACE_MACHINE,
    ROTATE_MACHINE,
    SELL_MACHINE,
    MOVE_MACHINE,
    SELECT,
    COPY,
    PASTE,
    PLACE_MACHINE_PASTE,
    // Cheat codes below
    SPAWN_RESOURCE,
}