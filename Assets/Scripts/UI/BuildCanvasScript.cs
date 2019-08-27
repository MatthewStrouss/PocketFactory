using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BuildCanvasScript : MonoBehaviour
{
    public Button BuildMachineButtonPrefab;
    public GameObject Content;
    public GameObject XButton;
    public GameObject OkCancelCanvas;
    [SerializeField] private GameManagerController gameManager;

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
        this.gameObject.SetActive(true);
        this.UpdateUI();
        this.XButton.GetComponent<CancelCanvasScript>().Activate(this.gameObject);
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
        this.XButton.GetComponent<CancelCanvasScript>().Deactivate();
    }

    public void UpdateUI()
    {
        foreach (Transform child in this.Content.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        List<GameObject> machines = PrefabDatabase.Instance.GetPrefabsForType("Machine").Values.Where(x => x.GetComponent<MachineController>().Machine.IsUnlocked).ToList();

        foreach (GameObject machine in machines)
        {
            Button newButton = Instantiate<Button>(this.BuildMachineButtonPrefab, this.Content.transform);
            newButton.transform.Find("Image").GetComponent<Image>().sprite = machine.GetComponent<SpriteRenderer>().sprite;
            newButton.transform.Find("NameText").GetComponent<Text>().text = machine.GetComponent<MachineController>().Machine.MachineName;
            newButton.transform.Find("DescriptionText").GetComponent<Text>().text = "Don't forget to replace this text with machine description. Please";
            newButton.transform.Find("PriceText").GetComponent<Text>().text = string.Format("${0}", machine.GetComponent<MachineController>().Machine.BuildCost);

            newButton.onClick.AddListener(() =>
            {
                Camera.main.GetComponent<PlayerScript>().SetMachine(machine);
                this.Deactivate();
                this.OkCancelCanvas.GetComponent<OkCancelCanvasScript>().Activate(
                    $"Tap to build a {machine.GetComponent<MachineController>().Machine.MachineName}",
                    () => Camera.main.GetComponent<PlayerScript>().AcceptBuild(),
                    () => Camera.main.GetComponent<PlayerScript>().CancelBuild()
                    );
            });
        }

        //List<Machine> machines = this.gameManager.machineDatabase.machines.Where(x => x.Value.IsUnlocked).Select(x => x.Value).ToList();

        //foreach (Machine machine in machines)
        //{
        //    Button newButton = Instantiate<Button>(this.BuildMachineButtonPrefab, this.Content.transform);
        //    newButton.transform.Find("Image").GetComponent<Image>().sprite = machine.Sprite;
        //    newButton.transform.Find("NameText").GetComponent<Text>().text = machine.MachineName;
        //    newButton.transform.Find("DescriptionText").GetComponent<Text>().text = "Don't forget to replace this text with machine description. Please";
        //    newButton.transform.Find("PriceText").GetComponent<Text>().text = string.Format("${0}", machine.BuildCost);

        //    newButton.onClick.AddListener(() =>
        //    {
        //        Camera.main.GetComponent<PlayerScript>().SetMachine(machine);
        //        this.Deactivate();
        //        this.OkCancelCanvas.GetComponent<OkCancelCanvasScript>().Activate(
        //            $"Tap to build a {machine.MachineName}",
        //            () => Camera.main.GetComponent<PlayerScript>().AcceptBuild(),
        //            () => Camera.main.GetComponent<PlayerScript>().CancelBuild()
        //            );
        //    });
        //}
    }
}
