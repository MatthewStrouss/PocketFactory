using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public void ShowArrowsButton_Click()
    {
        UnityEngine.Object.FindObjectsOfType<GameObject>().ToList().Where(x => x.layer.Equals(8)).ToList().ForEach(x =>
        {
            x.GetComponent<MachineController>()?.ToggleArrow();
        });
    }
}
