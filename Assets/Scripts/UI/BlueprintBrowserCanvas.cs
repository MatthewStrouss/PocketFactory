using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintBrowserCanvas : MonoBehaviour
{
    [SerializeField] private CancelCanvasScript XButton;
    [SerializeField] private Text headerText;
    [SerializeField] private GameObject Content;
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private NewBlueprintCanvas NewBlueprintCanvas;
    [SerializeField] private BlueprintCanvasScript BlueprintCanvasScript;
    [SerializeField] private Text messageText;
    [SerializeField] private Image messagePanel;

    private Dictionary<string, object> homeLevel = new Dictionary<string, object>();
    private Dictionary<string, object> previousLevel = new Dictionary<string, object>();
    public Dictionary<string, object> currentLevel;
    private List<string> history;

    private void Awake()
    {

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
        this.homeLevel = BlueprintDatabase.database;
        this.currentLevel = homeLevel;
        this.history = new List<string>();

        this.XButton.Activate(this.Deactivate);
        this.UpdateUI();
        this.gameObject.SetActive(true);
    }

    public void Activate(string jsonData)
    {
        this.Activate();
        this.NewBlueprint(BlueprintTypeEnum.BLUEPRINT, jsonData);
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
        this.homeLevel = null;
        this.previousLevel = null;
        this.currentLevel = null;
        this.history = null;
        this.messageText.text = null;
        this.messagePanel.gameObject.SetActive(false);
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
                    this.BlueprintCanvasScript.Activate(kvp.Value as Blueprint);
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

    public void ShareFolderButton_Clicked()
    {
        GUIUtility.systemCopyBuffer = Newtonsoft.Json.JsonConvert.SerializeObject(this.currentLevel);
    }

    public void ImportClipboardButton_Clicked()
    {
        try
        {
            Dictionary<string, object> newEntry = this.RecreateBlueprintLibrary(GUIUtility.systemCopyBuffer);


            bool isValid = true;
            string key = string.Empty;

            foreach(KeyValuePair<string, object> kvp in newEntry)
            {
                bool keyExists = this.currentLevel.ContainsKey(kvp.Key);

                if (keyExists)
                {
                    key = kvp.Key;
                    isValid = false;
                    break;
                }

                isValid &= true;
            }

            if (isValid)
            {
                foreach (KeyValuePair<string, object> kvp in newEntry)
                {
                    object objectToAdd;

                    try
                    {
                        objectToAdd = Newtonsoft.Json.JsonConvert.DeserializeObject<Blueprint>(kvp.Value.ToString());
                    }
                    catch
                    {
                        objectToAdd = kvp.Value;
                    }

                    if (objectToAdd == default(Blueprint))
                    {
                        objectToAdd = kvp.Value;
                    }

                    this.currentLevel.Add(kvp.Key, objectToAdd);
                    //BlueprintDatabase.Add(kvp.Key, objectToAdd, this.history);
                }

                this.messageText.text = "Successfully imported blueprint library";
                this.messagePanel.color = Color.green;

                this.UpdateUI();
            }
            else
            {
                this.messageText.text = $"Failed to import blueprint library. Key {key} already exists.";
                this.messagePanel.color = Color.red;
            }
        }
        catch (Exception ex)
        {
            this.messageText.text = "Failed to import blueprint library";
            this.messagePanel.color = Color.red;
        }

        this.messagePanel.gameObject.SetActive(!string.IsNullOrWhiteSpace(this.messageText.text));
    }

    private Dictionary<string, object> RecreateBlueprintLibrary(string json)
    {
        Dictionary<string, object> newDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        Dictionary<string, object> returnDict = new Dictionary<string, object>();

        foreach(KeyValuePair<string, object> kvp in newDict)
        {
            Blueprint blueprint = Newtonsoft.Json.JsonConvert.DeserializeObject<Blueprint>(kvp.Value.ToString());

            if (string.IsNullOrWhiteSpace(blueprint.Name))
            {
                returnDict.Add(kvp.Key, this.RecreateBlueprintLibrary(kvp.Value.ToString()));
            }
            else
            {
                returnDict.Add(kvp.Key, blueprint);
            }
        }

        return returnDict;
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
