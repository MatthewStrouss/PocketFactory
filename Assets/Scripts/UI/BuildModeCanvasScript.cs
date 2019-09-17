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
    private List<Button> buttonsList = new List<Button>();

    private GameObject machineToPlace;
    private List<GameObject> placedMachines = new List<GameObject>();

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
        if (machineToPlace != null)
        {
            if ((gesture.State != GestureRecognizerState.Failed) && (gesture.State != GestureRecognizerState.Possible))
            {
                Vector3 mousePos = this.cam.ScreenToWorldPoint(new Vector3(gesture.FocusX, gesture.FocusY));
                Vector2 rayPos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
                RaycastHit2D test = Physics2D.Raycast(rayPos, Vector2.zero, 0f, 1 << 8);

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

                this.OkCancelCanvas.UpdateInstructionText2($"${this.placedMachines.Sum(x => x.GetComponent<MachineController>().Machine.BuildCost)}");
                this.OkCancelCanvas.SetOkButtonActive(this.placedMachines.Count > 0);
            }
        }
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);

        this.OkCancelCanvas.Activate(
            "Select a machine and tap on the screen to place it",
            this.AcceptBuild,
            this.CancelBuild,
            false
            );

        this.CreateButtons();
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
            Button newButton = Instantiate<Button>(this.BuildMachineButtonPrefab, this.Content.transform);
            newButton.GetComponent<SelectMachineButtonScript>().Activate(machine.GetComponent<MachineController>().Machine);

            newButton.onClick.AddListener(() =>
            {
                this.SetMachine(machine);
            });

            this.buttonsList.Add(newButton);
        }
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
