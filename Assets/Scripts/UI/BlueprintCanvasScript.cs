using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintCanvasScript : MonoBehaviour
{
    [SerializeField] private Text headerText;
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private Text messageText;
    [SerializeField] private InputField nameInputField;
    [SerializeField] private InputField dataInputField;
    [SerializeField] private PasteBlueprintCanvasScript PasteBlueprintCanvasScript;
    [SerializeField] private BlueprintBrowserCanvas BlueprintBrowserCanvas;

    private Blueprint Blueprint;

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

    public void Activate(Blueprint blueprint)
    {
        this.Blueprint = blueprint;
        this.Activate();
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
        this.gameObject.SetActive(true);
        this.nameInputField.text = this.Blueprint.Name;
        this.dataInputField.text = this.Blueprint.Paste;
    }

    public void PlaceButton_Clicked()
    {
        this.gameObject.SetActive(false);
        this.BlueprintBrowserCanvas.gameObject.SetActive(false);

        this.PasteBlueprintCanvasScript.Activate(
            this.Blueprint.Paste,
            this.BlueprintBrowserCanvas.UpdateUI
            );
    }

    public void ShareButton_Clicked()
    {
        GUIUtility.systemCopyBuffer = Newtonsoft.Json.JsonConvert.SerializeObject(this.Blueprint);
    }
}
