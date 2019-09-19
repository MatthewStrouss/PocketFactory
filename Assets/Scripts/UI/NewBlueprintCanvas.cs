using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBlueprintCanvas : MonoBehaviour
{
    [SerializeField] private Text headerText;
    [SerializeField] private OkCancelCanvasScript OkCancelCanvas;
    [SerializeField] private Text nameText;
    [SerializeField] private InputField dataText;
    [SerializeField] private GameObject dataPanel;
    [SerializeField] private Text errorText;
    [SerializeField] private GameObject errorPanel;
    [SerializeField] private BlueprintBrowserCanvas BlueprintCanvas;

    //private Dictionary<string, object> dict;
    private BlueprintTypeEnum blueprintTypeEnum;
    private Action AcceptCallback;
    private Action CancelCallback;
    private string Error;
    private string JsonData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Activate()
    {
        this.gameObject.SetActive(true);

        this.UpdateUI();
    }

    public void Activate(BlueprintTypeEnum blueprintTypeEnum, Action acceptCallback, Action cancelCallback, string jsonData = null)
    {
        this.blueprintTypeEnum = blueprintTypeEnum;
        this.AcceptCallback = acceptCallback;
        this.CancelCallback = cancelCallback;
        this.JsonData = jsonData;
        this.Activate();
    }

    public void Deactivate(Action callback)
    {
        this.gameObject.SetActive(false);
        this.nameText.text = string.Empty;
        this.dataText.text = string.Empty;
        callback?.DynamicInvoke();
    }

    public void UpdateUI()
    {
        this.headerText.text = $"New {(this.blueprintTypeEnum == BlueprintTypeEnum.BLUEPRINT ? "Blueprint" : "Folder")}";
        this.dataPanel.SetActive(this.blueprintTypeEnum == BlueprintTypeEnum.BLUEPRINT);
        this.dataText.text = this.JsonData;
        this.errorText.text = this.Error;
        this.errorPanel.SetActive(!string.IsNullOrWhiteSpace(this.Error));

        this.OkCancelCanvas.Activate(
            "Please fill out the information about this blueprint. You may also select the folder you wish to save it in.",
            this.AcceptBlueprintSave,
            this.CancelBlueprintSave
        );
    }

    public void AcceptBlueprintSave()
    {
        bool isValidForSave = this.IsBlueprintValidForSave();

        if (isValidForSave)
        {
            this.Save();
            this.Deactivate(this.AcceptCallback);
        }
    }

    public void CancelBlueprintSave()
    {
        this.Deactivate(this.CancelCallback);
    }

    private bool IsBlueprintValidForSave()
    {
        bool isValidForSave = true;

        if (this.BlueprintCanvas.currentLevel.ContainsKey(this.nameText.text))
        {
            isValidForSave = false;
            this.Error = "Something with that name already exists here";
            this.UpdateUI();
        }

        return isValidForSave;
    }

    private void Save()
    {
        object objectToAdd;

        if (this.blueprintTypeEnum == BlueprintTypeEnum.BLUEPRINT)
        {
            objectToAdd = new Blueprint(this.nameText.text, this.dataText.text);
        }
        else
        {
            objectToAdd = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
        }

        this.BlueprintCanvas.currentLevel.Add(this.nameText.text, objectToAdd);
    }
}
