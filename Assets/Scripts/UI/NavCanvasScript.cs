using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavCanvasScript : MonoBehaviour
{
    public GameObject ResearchCanvas;
    public GameObject BuildCanvas;

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
        Camera.main.GetComponent<PlayerScript>().StartSelectMode();
    }

    public void ResearchButton_Clicked()
    {
        this.ResearchCanvas.GetComponent<ResearchCanvasScript>().Activate();
    }

    public void BuildButton_Clicked()
    {
        this.BuildCanvas.GetComponent<BuildCanvasScript>().Activate();
    }

    public void BlueprintButton_Clicked()
    {
        throw new System.Exception("Not implemented");
    }
}
