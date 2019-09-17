using Assets.Scripts;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveBlueprintScript : MonoBehaviour
{
    [SerializeField] private BlueprintCanvas BlueprintCanvas;
    [SerializeField] private OkCancelCanvasScript OkCancelButton;

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
            "Are you sure you wish to save the selection as a blueprint?",
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
        this.Copy();
    }

    public void Cancel()
    {
        this.Deactivate(this.CancelCallback);
    }

    public void Copy()
    {
        if (this.SelectedGameObjects.Count > 0)
        {
            List<MachineModel> machineModels = this.SelectedGameObjects.ToMachineModelList();
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(machineModels, new Newtonsoft.Json.JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            //GUIUtility.systemCopyBuffer = json;

            this.BlueprintCanvas.Activate(json);
            //string base64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(json));

            //PrefabDatabase.Instance.GetPrefab("UI", "Copy").GetComponent<CopyCanvasScript>().Activate();
            //PrefabDatabase.Instance.GetPrefab("UI", "Copy").GetComponent<CopyCanvasScript>().UpdateUI(json);
        }
    }
}
