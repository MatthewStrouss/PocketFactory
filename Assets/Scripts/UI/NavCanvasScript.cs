using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavCanvasScript : MonoBehaviour
{
    public GameObject ResearchCanvas;
    public GameObject BuildCanvas;
    [SerializeField] private BlueprintBrowserCanvas BlueprintCanvas;
    [SerializeField] private SelectionModeCanvasScript SelectionModeCanvasScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectButton_Clicked()
    {
        this.SelectionModeCanvasScript.Activate();
    }

    public void ResearchButton_Clicked()
    {
        this.ResearchCanvas.GetComponent<ResearchCanvasScript>().Activate();
    }

    public void BuildButton_Clicked()
    {
        this.BuildCanvas.GetComponent<BuildModeCanvasScript>().Activate();
    }

    public void BlueprintButton_Clicked()
    {
        this.BlueprintCanvas.Activate();
    }
}
