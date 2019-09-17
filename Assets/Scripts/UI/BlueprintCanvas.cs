using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintCanvas : MonoBehaviour
{
    [SerializeField] private Text headerText;
    [SerializeField] private GameObject Content;
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private NewBlueprintCanvas NewBlueprintCanvas;
    [SerializeField] private PasteBlueprintCanvasScript PasteBlueprintCanvasScript;

    private Dictionary<string, object> homeLevel = new Dictionary<string, object>();
    private Dictionary<string, object> previousLevel = new Dictionary<string, object>();
    public Dictionary<string, object> currentLevel = new Dictionary<string, object>();
    private List<string> history = new List<string>();

    private void Awake()
    {
        homeLevel = BlueprintDatabase.blueprints;
        currentLevel = homeLevel;

        this.UpdateUI();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Activate()
    {
        this.UpdateUI();
        this.gameObject.SetActive(true);
    }

    public void Activate(string jsonData)
    {
        this.Activate();
        this.NewBlueprint(BlueprintTypeEnum.BLUEPRINT, jsonData);
    }

    public void Deactivate(bool eraseData)
    {
        this.gameObject.SetActive(false);

        if (eraseData)
        {
            this.homeLevel.Clear();
            this.previousLevel.Clear();
            this.currentLevel.Clear();
            this.history.Clear();
        }
    }

    public void UpdateUI()
    {
        this.gameObject.SetActive(true);
        this.CreateButtons();

        this.headerText.text = string.Join(
            "/",
            this.history
            );
    }

    public void CreateButtons()
    {
        foreach (Transform child in this.Content.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (KeyValuePair<string, object> kvp in this.currentLevel)
        {
            Button newButton = Instantiate(this.buttonPrefab, this.Content.transform);

            BlueprintTypeEnum blueprintTypeEnum;

            if (kvp.Value is Blueprint)
            {
                blueprintTypeEnum = BlueprintTypeEnum.BLUEPRINT;

                newButton.onClick.AddListener(() =>
                {
                    this.gameObject.SetActive(false);

                    this.PasteBlueprintCanvasScript.Activate(
                        (kvp.Value as Blueprint).Paste,
                        this.UpdateUI
                        );
                });
            }
            else
            {
                blueprintTypeEnum = BlueprintTypeEnum.FOLDER;

                newButton.onClick.AddListener(() =>
                {
                    this.currentLevel = kvp.Value as Dictionary<string, object>;
                    this.previousLevel = this.currentLevel;
                    this.history.Add(kvp.Key);
                    this.UpdateUI();
                });
            }

            newButton.GetComponent<BlueprintButtonPrefabScript>().Activate(kvp.Key, blueprintTypeEnum);
            //newButton.interactable = 
        }
    }

    public void BackButton_Clicked()
    {
        this.currentLevel = previousLevel;

        //foreach(string level in this.history)
        //{
        //    Dictionary<string, object> currentLevel = BlueprintDatabase
        //}

        this.UpdateUI();
    }

    public void HomeButton_Clicked()
    {
        this.currentLevel = homeLevel;
        this.history.Clear();
        this.UpdateUI();
    }

    public void NewFolderButton_Clicked()
    {
        this.NewBlueprint(BlueprintTypeEnum.FOLDER);
    }

    public void NewBlueprintButton_Clicked()
    {
        this.NewBlueprint(BlueprintTypeEnum.BLUEPRINT);
    }

    public void NewBlueprint(BlueprintTypeEnum blueprintTypeEnum, string jsonData = null)
    {
        this.NewBlueprintCanvas.Activate(
            blueprintTypeEnum,
            this.UpdateUI,
            this.UpdateUI,
            jsonData
            );
    }

    //public void IsPasteValid(List<MachineController> machinesToPlace, out bool isPasteValid, out string pasteInvalidString)
    //{
    //isPasteValid = true;
    //pasteInvalidString = string.Empty;

    //if (!CanAffordPaste(machinesToPlace))
    //{
    //isPasteValid = false;
    //pasteInvalidString = "Not enough money for all machines in paste";
    //}

    //if (!HasUnlockedAllMachines(machinesToPlace))
    //{
    //isPasteValid = false;
    //pasteInvalidString = "Not all machines in paste are unlocked";
    //}


    //if (!HasUnlockedAllRecipes(machinesToPlace))
    //{
    //isPasteValid = false;
    //pasteInvalidString = "Not all recipes in paste are unlocked";
    //}
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
}
