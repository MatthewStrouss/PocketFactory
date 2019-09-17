using DigitalRubyShared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionModeCanvasScript : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private OkCancelCanvasScript OkCancelCanvas;
    [SerializeField] private SelectionActionCanvasScript SelectionActionCanvasScript;
    [SerializeField] private MovementScript movementScript;
    [SerializeField] private GameObject selectionRectangle;

    private List<GameObject> SelectedGameObjects = new List<GameObject>();

    private Vector3 mouseStartPos;
    private Vector3 mouseEndPos;

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

        this.OkCancelCanvas.Activate(
            "Please select the machines you wish to operate on",
            () => this.AcceptSelectMode(),
            () => this.CancelSelectMode()
            );

        this.movementScript.tapGesture.StateUpdated += this.HandleClick;
        this.movementScript.actionGesture.StateUpdated += this.HandlePan;
        this.movementScript.EnableBuildMode();

        this.cam.GetComponent<PlayerScript>().playerStateEnum = PlayerStateEnum.SELECT;
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);

        this.movementScript.tapGesture.StateUpdated -= this.HandleClick;
        this.movementScript.actionGesture.StateUpdated -= this.HandlePan;
        this.movementScript.DisableBuildMode();

        this.cam.GetComponent<PlayerScript>().playerStateEnum = PlayerStateEnum.NONE;
    }

    public void AcceptSelectMode()
    {
        this.SelectionActionCanvasScript.Activate(this.SelectedGameObjects);
        this.Deactivate();
    }

    public void CancelSelectMode()
    {
        this.SelectedGameObjects?.ForEach(x => x?.GetComponent<MachineController>()?.DeactivateSelected());
        this.SelectedGameObjects?.Clear();
        this.Deactivate();
    }

    public void HandleClick(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Ended)
        {
            Vector3 mousePos = this.cam.ScreenToWorldPoint(new Vector3(gesture.FocusX, gesture.FocusY));
            Vector2 rayPos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
            RaycastHit2D test = Physics2D.Raycast(rayPos, Vector2.zero, 0f, 1 << 8);

            if (test)
            {
                //test.transform.gameObject.GetComponent<MachineController>().OnClick();
                //this.starterCanvas.GetComponent<StarterPanelScript>().Activate(test.transform.gameObject);
                GameObject machine = test.transform.gameObject;

                if (this.SelectedGameObjects.Contains(machine))
                {
                    this.RemoveFromSelection(machine);
                }
                else
                {
                    this.AddToSelection(machine);
                }
            }
        }
    }

    public void HandlePan(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Began)
        {
            this.mouseStartPos = this.cam.ScreenToWorldPoint(new Vector3(gesture.FocusX, gesture.FocusY)); ;
            this.mouseStartPos.z = -1;
            this.selectionRectangle.transform.position = this.mouseStartPos;
            //this.selectionRectangle.SetActive(true);
            this.selectionRectangle.GetComponent<SelectionObjectScript>().Activate(this.AddToSelection);
        }
        else if (gesture.State == GestureRecognizerState.Executing)
        {
            Vector3 start = this.cam.ScreenToWorldPoint(new Vector3(gesture.StartFocusX, gesture.StartFocusY));
            Vector3 end = this.cam.ScreenToWorldPoint(new Vector3(gesture.FocusX, gesture.FocusY));

            this.selectionRectangle.transform.localScale = end - start;
        }
        else if (gesture.State == GestureRecognizerState.Ended)
        {
            this.selectionRectangle.SetActive(false);
        }
    }

    public void AddToSelection(GameObject machinetoAdd)
    {
        if (!this.SelectedGameObjects.Contains(machinetoAdd))
        {
            this.SelectedGameObjects.Add(machinetoAdd);
            machinetoAdd.GetComponent<MachineController>().ActivateSelected();
        }

        this.OkCancelCanvas.UpdateInstructionText($"Selected {this.SelectedGameObjects.Count} machines");
    }

    public void RemoveFromSelection(GameObject machineToAdd)
    {
        if (this.SelectedGameObjects.Contains(machineToAdd))
        {
            this.SelectedGameObjects.Remove(machineToAdd);
            machineToAdd.GetComponent<MachineController>().DeactivateSelected();
        }

        this.OkCancelCanvas.UpdateInstructionText($"Selected {this.SelectedGameObjects.Count} machines");
    }
}
