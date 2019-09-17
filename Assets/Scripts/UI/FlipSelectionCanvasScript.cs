using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlipSelectionCanvasScript : MonoBehaviour
{
    [SerializeField] private OkCancelCanvasScript OkCancelButton;

    private List<GameObject> SelectedGameObjects;
    private Dictionary<int, GameObject> BackupData = new Dictionary<int, GameObject>();

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
            "Press the direction you wish to flip the machines",
            this.Accept,
            this.Cancel
            );
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

        this.OkCallback = okCallback;
        this.CancelCallback = cancelCallback;

        this.Activate();
    }

    public void Deactivate(Action callback)
    {
        this.BackupData.Values.ToList().ForEach(x => Destroy(x));
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
        this.SelectedGameObjects.ForEach(x =>
        {
            x.transform.SetPositionAndRotation(this.BackupData[x.GetInstanceID()].transform.position, this.BackupData[x.GetInstanceID()].transform.rotation);
        });

        this.Deactivate(this.CancelCallback);
    }
    
    public void FlipSelectionXButton_Clicked()
    {
        if (this.SelectedGameObjects.Count > 0)
        {

            Vector3 point = new Vector3(
                Mathf.Round(this.SelectedGameObjects.Sum(x => x.transform.position.x) / this.SelectedGameObjects.Count),
                Mathf.Round(this.SelectedGameObjects.Sum(x => x.transform.position.y) / this.SelectedGameObjects.Count),
                -8
                );

            this.SelectedGameObjects.ForEach(x =>
            {
                x.transform.RotateAround(point, new Vector3(0, 1, 0), 180);
            });
        }
    }

    public void FlipSelectionYButton_Clicked()
    {
        if (this.SelectedGameObjects.Count > 0)
        {

            Vector3 point = new Vector3(
                Mathf.Round(this.SelectedGameObjects.Sum(x => x.transform.position.x) / this.SelectedGameObjects.Count),
                Mathf.Round(this.SelectedGameObjects.Sum(x => x.transform.position.y) / this.SelectedGameObjects.Count),
                -8
                );

            this.SelectedGameObjects.ForEach(x =>
            {
                x.transform.RotateAround(point, new Vector3(0, 0, 1), 180);
            });
        }
    }
}
