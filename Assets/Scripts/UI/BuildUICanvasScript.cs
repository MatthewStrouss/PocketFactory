using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildUICanvasScript : MonoBehaviour
{
    public GameObject machinesPanel;
    public GameObject selectionCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Deactivate()
    {
        this.selectionCanvas.GetComponent<SelectionCanvasScript>().Deactivate();
        this.gameObject.SetActive(false);
    }

    public void MachinesButton_Clicked()
    {
        this.machinesPanel.GetComponent<MachinesPanelScript>().Activate();
    }

    public void RotateButton_Clicked()
    {

    }

    public void SelectButton_Click()
    {
        this.selectionCanvas.GetComponent<SelectionCanvasScript>().ToggleActive();
    }
}
