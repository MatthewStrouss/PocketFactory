using DigitalRubyShared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveModeCanvasScript : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private OkCancelCanvasScript OkCancelCanvas;
    [SerializeField] public MovementScript movementScript;

    private List<GameObject> SelectedGameObjects;
    private Dictionary<int, GameObject> BackupData = new Dictionary<int, GameObject>();

    private Action OkCallback;
    private Action CancelCallback;

    private GameObject machineToPlace;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Activate()
    {
        this.gameObject.SetActive(true);

        this.OkCancelCanvas.Activate(
            "Press a tile where you would like to place the machines.",
            this.Accept,
            this.Cancel
            );

        this.movementScript.tapGesture.StateUpdated += this.HandleClick;
        this.movementScript.EnableBuildMode();
    }

    public void Activate(List<GameObject> selectedObjects, Action okCallback, Action cancelCallback)
    {
        this.SelectedGameObjects = selectedObjects;

        this.SelectedGameObjects.ForEach(x =>
        {
            GameObject newGO = new GameObject();
            newGO.transform.position = x.transform.position;
            newGO.transform.rotation = x.transform.rotation;

            this.BackupData.Add(x.GetInstanceID(), newGO);
        });

        this.SetupBuildMode();

        this.OkCallback = okCallback;
        this.CancelCallback = cancelCallback;

        this.Activate();
    }

    public void Deactivate(Action callback)
    {
        this.BackupData.Values.ToList().ForEach(x => Destroy(x));
        this.BackupData.Clear();
        this.gameObject.SetActive(false);
        this.OkCancelCanvas.Deactivate();

        callback?.DynamicInvoke();
        this.movementScript.tapGesture.StateUpdated -= this.HandleClick;
        this.movementScript.DisableBuildMode();
    }

    public void Accept()
    {
        foreach (Transform child in this.machineToPlace.transform)
        {
            child.GetComponent<BoxCollider2D>().enabled = true;
            child.GetComponent<MachineController>().enabled = true;
        }

        this.machineToPlace.transform.DetachChildren();
        Destroy(this.machineToPlace);

        this.Deactivate(this.OkCallback);
    }

    public void Cancel()
    {
        this.machineToPlace.transform.DetachChildren();
        Destroy(this.machineToPlace);

        this.SelectedGameObjects.ForEach(x =>
        {
            x.transform.SetPositionAndRotation(this.BackupData[x.GetInstanceID()].transform.position, this.BackupData[x.GetInstanceID()].transform.rotation);
        });

        this.Deactivate(this.CancelCallback);
    }

    public void SetupBuildMode()
    {
        GameObject parent = new GameObject();

        //List<GameObject> newGameObjectsList = new List<GameObject>();

        //this.SelectedGameObjects.ForEach(x =>
        //{
        //    GameObject clone = Instantiate(x);
        //    clone.GetComponent<MachineController>().SetControllerValues(x.GetComponent<MachineController>().controller);
        //    clone.GetComponent<BoxCollider2D>().enabled = false;
        //    clone.GetComponent<MachineController>().enabled = false;
        //    //clone.GetComponent<MachineController>().controller.enabled = false;
        //    newGameObjectsList.Add(clone);
        //    x.SetActive(false);
        //});

        parent.transform.position = new Vector3(
            Mathf.Round(this.SelectedGameObjects.Sum(x => x.transform.position.x) / this.SelectedGameObjects.Count),
            Mathf.Round(this.SelectedGameObjects.Sum(x => x.transform.position.y) / this.SelectedGameObjects.Count),
            -8
            );

        this.SelectedGameObjects.ForEach(x => x.transform.parent = parent.transform);
        this.SelectedGameObjects.ForEach(x => x.transform.position = new Vector3(x.transform.position.x, x.transform.position.y, x.transform.position.z));
        this.machineToPlace = parent;
    }

    public void HandleClick(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Ended)
        {
            if (machineToPlace != null)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(gesture.FocusX, gesture.FocusY));
                Vector2 rayPos = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
                this.machineToPlace.transform.position = new Vector3(rayPos.x, rayPos.y, -8);
                bool canPlace = true;
                foreach (Transform child in this.machineToPlace.transform)
                {
                    Vector2 machineRayPos = new Vector2(Mathf.Round(child.transform.position.x), Mathf.Round(child.transform.position.y));
                    RaycastHit2D test = Physics2D.Raycast(machineRayPos, Vector2.zero, 0f, 1 << 8);
                    canPlace &= !test;

                    if (!canPlace) break;
                }

                this.OkCancelCanvas.SetOkButtonActive(canPlace);
            }
        }
    }
}
