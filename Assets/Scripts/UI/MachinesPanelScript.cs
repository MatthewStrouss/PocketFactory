using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachinesPanelScript : MonoBehaviour
{
    public GameObject machinesPanel;
    public Button buttonPrefab;
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
        foreach (Transform child in this.machinesPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        Dictionary<string, GameObject> machines = PrefabDatabase.Instance.GetPrefabsForType("Machine");

        foreach(KeyValuePair<string, GameObject> machine in machines)
        {
            Button newButton = Instantiate<Button>(this.buttonPrefab, this.machinesPanel.transform);
            newButton.GetComponentInChildren<Text>().text = machine.Key;
            newButton.image.sprite = machine.Value.GetComponent<SpriteRenderer>().sprite;
            newButton.GetComponent<RectTransform>().sizeDelta = new Vector2(64, 64);
            newButton.onClick.AddListener(() =>
            {
                Camera.main.GetComponent<PlayerScript>().SetMachine(machine.Value);
                this.gameObject.SetActive(false);
            //    GlobalManager.instance._playerManager.playerStateEnum = PlayerStateEnum.PLACE_TILE;
            //    GlobalManager.instance._playerManager.placeID = x.ID;
            });
        }

        this.gameObject.SetActive(true);
    }

    public void XButton_Click()
    {
        this.gameObject.SetActive(false);
    }
}
