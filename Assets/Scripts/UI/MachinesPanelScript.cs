using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachinesPanelScript : MonoBehaviour
{
    public GameObject machinesPanel;
    public Button buttonPrefab;
    public Button unlockButtonPrefab;

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

    public void UpdateUI()
    {
        foreach (Transform child in this.machinesPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        Dictionary<string, GameObject> machines = PrefabDatabase.Instance.GetPrefabsForType("Machine");

        foreach (KeyValuePair<string, GameObject> machine in machines)
        {
            MachineController machineController = machine.Value.GetComponent<MachineController>();

            Button newButton;
            

            if (machineController.Machine.IsUnlocked)
            {
                newButton = Instantiate<Button>(this.buttonPrefab, this.machinesPanel.transform);
                newButton.transform.Find("Panel").transform.Find("Image").GetComponent<Image>().sprite = machine.Value.GetComponent<SpriteRenderer>().sprite;
                newButton.transform.Find("CostText").GetComponent<Text>().text = string.Format("${0}", machineController.Machine.BuildCost);

                newButton.onClick.AddListener(() =>
                {
                    Camera.main.GetComponent<PlayerScript>().SetMachine(machine.Value);
                    this.gameObject.SetActive(false);
                });
            }
            else
            {
                newButton = Instantiate<Button>(this.unlockButtonPrefab, this.machinesPanel.transform);
                newButton.transform.Find("CostText").GetComponent<Text>().text = string.Format("${0}", machineController.Machine.UnlockCost);

                newButton.onClick.AddListener(() =>
                {
                    machineController.Unlock();
                    this.UpdateUI();
                });
            }

            newButton.transform.Find("NameText").GetComponent<Text>().text = machine.Key;
        }
    }

    public void XButton_Click()
    {
        this.gameObject.SetActive(false);
    }
}
