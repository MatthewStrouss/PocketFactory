using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGUIScript : MonoBehaviour
{
    public Button machinesButton;
    public GameObject machinesPanel;
    public GameObject rotationsPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MachinesButton_Click()
    {
        this.machinesPanel.GetComponent<MachinesPanelScript>().Activate();
    }

    public void RotateButton_Click()
    {
        this.rotationsPanel.GetComponent<RotationPanelScript>().Activate();
    }

    public void SelectButton_Click()
    {
        Camera.main.GetComponent<PlayerScript>().StartSelectMode();
    }
}
