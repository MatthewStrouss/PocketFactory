using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGUIScript : MonoBehaviour
{
    public Button machinesButton;
    public GameObject machinesPanel;
    public GameObject rotationsPanel;

    public Button playPauseButton;
    public Image playImage;
    public Image pauseImage;

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
        PrefabDatabase.Instance.GetPrefab("UI", "Selection").GetComponent<SelectionCanvasScript>().ToggleActive();
    }

    public void PlayPauseButton_Click()
    {
        bool isPaused = GameManagerController.Instance.PlayPause();

        this.playImage.gameObject.SetActive(!isPaused);
        this.pauseImage.gameObject.SetActive(isPaused);
    }

    public void BuildButton_Click()
    {
        Camera.main.GetComponent<PlayerScript>().BuildMode();
    }
}
