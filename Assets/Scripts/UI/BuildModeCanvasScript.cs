using DigitalRubyShared;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BuildModeCanvasScript : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private OkCancelCanvasScript OkCancelCanvas;
    [SerializeField] public MovementScript movementScript;
    [SerializeField] private GameObject Content;
    [SerializeField] private Button BuildMachineButtonPrefab;
    [SerializeField] private Toggle BuildMachineTogglePrefab;
    [SerializeField] private GameObject GridTilemap;

    private List<Button> buttonsList = new List<Button>();

    private GameObject machineToPlace;
    private List<GameObject> placedMachines = new List<GameObject>();

    private static readonly float[] BoundsX = new float[] { -8f, 7f };
    private static readonly float[] BoundsY = new float[] { -8f, 7f };

    private void Awake()
    {

    }

    private void PlayerModel_MoneyUpdated(object sender, System.EventArgs e)
    {
        this.UpdateUI();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleClick(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Ended)
        {
            if (machineToPlace != null)
            {
                Vector3 mousePos = this.cam.ScreenToWorldPoint(new Vector3(gesture.FocusX, gesture.FocusY));
                Vector2 rayPos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
                RaycastHit2D test = Physics2D.Raycast(rayPos, Vector2.zero, 0f, 1 << 8);

                if (rayPos.x >= BoundsX[0] && rayPos.x <= BoundsX[1] &&
                    rayPos.y >= BoundsY[0] && rayPos.y <= BoundsY[1])
                if (test)
                {
                    MachineController offender = test.transform.gameObject.GetComponent<MachineController>();

                    if (offender.Machine.MachineID == this.machineToPlace.GetComponent<MachineController>().Machine.MachineID)
                    {
                        if (this.placedMachines.Contains(test.transform.gameObject))
                        {
                            offender.Sell(true);
                            this.placedMachines.Remove(test.transform.gameObject);
                        }
                    }
                }
                else
                {
                    GameObject goToAdd = this.machineToPlace.GetComponent<MachineController>().Place(rayPos, Quaternion.identity);

                    if (goToAdd != null)
                    {
                        this.placedMachines.Add(goToAdd);
                    }
                }

                this.OkCancelCanvas.UpdateInstructionText($"{this.placedMachines.Count}<align=right>${this.placedMachines.Sum(x => x.GetComponent<MachineController>().Machine.BuildCost)}");
                this.OkCancelCanvas.SetOkButtonActive(this.placedMachines.Count > 0);
            }
        }
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);

        this.OkCancelCanvas.Activate(
            "Build machines",
            "Tap to build and remove, hold to rotate",
            this.AcceptBuild,
            this.CancelBuild,
            false
            );

        this.CreateButtons();

        UnityEngine.Object.FindObjectsOfType<GameObject>().Where(x => x.layer == 8).Select(x => x.GetComponent<SpriteRenderer>()).ToList().ForEach(x => x.color = Color.gray);
        this.GridTilemap.SetActive(true);

        this.UpdateUI();

        this.movementScript.tapGesture.StateUpdated += this.HandleClick;
        this.movementScript.EnableBuildMode();

        Player.playerModel.MoneyUpdated += PlayerModel_MoneyUpdated;
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
        this.OkCancelCanvas.Deactivate();

        this.movementScript.tapGesture.StateUpdated -= this.HandleClick;
        this.movementScript.DisableBuildMode();

        UnityEngine.Object.FindObjectsOfType<GameObject>().Where(x => x.layer == 8).Select(x => x.GetComponent<SpriteRenderer>()).ToList().ForEach(x => x.color = Color.white);
        this.GridTilemap.SetActive(false);

        Player.playerModel.MoneyUpdated -= PlayerModel_MoneyUpdated;
    }

    public void AcceptBuild()
    {
        this.placedMachines.Clear();
        GameObject.Destroy(this.machineToPlace);
        this.machineToPlace = null;

        this.Deactivate();
    }

    public void CancelBuild()
    {
        this.placedMachines.ForEach(x => x?.GetComponent<MachineController>()?.Sell(true));
        this.placedMachines.Clear();
        GameObject.Destroy(this.machineToPlace);
        this.machineToPlace = null;

        this.Deactivate();
    }

    public void CreateButtons()
    {
        foreach (Transform child in this.Content.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        this.buttonsList.Clear();

        List<GameObject> machines = PrefabDatabase.Instance.GetPrefabsForType("Machine").Values.Where(x => x.GetComponent<MachineController>().Machine.IsUnlocked).OrderBy(x => x.GetComponent<MachineController>().Machine.BuildCost).ToList();

        foreach (GameObject machine in machines)
        {
            MachineController machineController = machine.GetComponent<MachineController>();
            Toggle newToggle = Instantiate(this.BuildMachineTogglePrefab, this.Content.transform);

            Transform background = newToggle.transform.Find("Background");
            background.GetComponent<Image>().sprite = machine.GetComponent<MachineController>().Machine.Sprite;
            background.Find("NameText").GetComponent<Text>().text = machine.GetComponent<MachineController>().Machine.MachineName;
            //background.Find("CostText").GetComponent<Text>().text = machine.GetComponent<MachineController>().Machine.BuildCost.ToString();
            newToggle.group = this.GetComponent<ToggleGroup>();
            newToggle.onValueChanged.AddListener((value) =>
            {
                if (value)
                {
                    this.SetMachine(machine);
                    background.Find("NameText").GetComponent<Text>().color = new Color(237f / 255f, 151f / 255f, 26f / 255f);
                }
                else
                {
                    background.Find("NameText").GetComponent<Text>().color = new Color(201f / 255f, 211f / 255f, 206f / 255f);
                }
            });

            //Button newButton = Instantiate<Button>(this.BuildMachineButtonPrefab, this.Content.transform);
            //newButton.GetComponent<SelectMachineButtonScript>().Activate(machine.GetComponent<MachineController>().Machine);

            //newButton.onClick.AddListener(() =>
            //{
            //    this.SetMachine(machine);
            //});

            //this.buttonsList.Add(newButton);
        }

        this.Content.transform.GetChild(0).GetComponent<Toggle>().isOn = true;
    }

    public void UpdateUI()
    {
        this.buttonsList.ForEach(x => x.interactable = Player.playerModel.CanAfford(x.GetComponent<SelectMachineButtonScript>().machine.BuildCost));
    }

    public void SetMachine(GameObject machineToPlace)
    {
        if (this.machineToPlace != null)
        {
            Destroy(this.machineToPlace);
        }

        this.machineToPlace = Instantiate(machineToPlace);
        this.machineToPlace.GetComponent<BoxCollider2D>().enabled = false;
    }
}
