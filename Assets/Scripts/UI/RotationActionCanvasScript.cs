using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationActionCanvasScript : MonoBehaviour
{
    [SerializeField] private OkCancelCanvasScript OkCancelButton;
    [SerializeField] private PlayerScript playerScript;

    private List<GameObject> SelectedGameObjects;
    private Dictionary<int, Quaternion> BackupData = new Dictionary<int, Quaternion>();

    private Action OkCallback;
    private Action CancelCallback;

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

        this.OkCancelButton.Activate(
            "Press the direction of the rotation",
            this.Accept,
            this.Cancel
            );
    }

    public void Activate(List<GameObject> selectedObjects, Action okCallback, Action cancelCallback)
    {
        this.SelectedGameObjects = selectedObjects;

        this.SelectedGameObjects.ForEach(x => this.BackupData.Add(x.GetInstanceID(), x.transform.rotation));

        this.OkCallback = okCallback;
        this.CancelCallback = cancelCallback;

        this.Activate();
    }

    public void Deactivate(Action callback)
    {
        this.BackupData.Clear();
        this.gameObject.SetActive(false);
        this.OkCancelButton.Deactivate();

        callback?.DynamicInvoke();
    }

    public void Accept()
    {
        this.Deactivate(this.OkCallback);
    }

    public void Cancel()
    {
        //this.playerScript.CancelRotation();
        this.SelectedGameObjects.ForEach(x =>
        {
            x.transform.rotation = this.BackupData[x.GetInstanceID()];
        });

        this.Deactivate(this.CancelCallback);
    }

    public void RotateLeftButton_Clicked()
    {
        this.SelectedGameObjects.ForEach(x => x.GetComponent<MachineController>().Rotate(Quaternion.Euler(0f, 0f, 270f)));
    }

    public void RotateUpButton_Clicked()
    {
        this.SelectedGameObjects.ForEach(x => x.GetComponent<MachineController>().Rotate(Quaternion.Euler(0f, 0f, 180f)));
    }

    public void RotateRightButton_Clicked()
    {
        this.SelectedGameObjects.ForEach(x => x.GetComponent<MachineController>().Rotate(Quaternion.Euler(0f, 0f, 90f)));
    }

    public void RotateDownButton_Clicked()
    {
        this.SelectedGameObjects.ForEach(x => x.GetComponent<MachineController>().Rotate(Quaternion.Euler(0f, 0f, 0f)));
    }
}
