using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SelectResourceCanvasScript : MonoBehaviour
{
    [SerializeField] private GameObject Content;
    [SerializeField] private GameObject XButton;
    [SerializeField] private Button buttonPrefab;
    public Action<Resource> callback;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Activate(Action<Resource> callback)
    {
        this.gameObject.SetActive(true);
        this.callback = callback;
        this.UpdateUI();
        this.XButton.GetComponent<CancelCanvasScript>().Activate(this.gameObject);
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
        this.XButton.GetComponent<CancelCanvasScript>().Deactivate();
    }

    public void Deactivate(Resource chosenResource)
    {
        this.Deactivate();
        this.callback(chosenResource);
    }

    public void UpdateUI()
    {
        foreach (Transform child in this.Content.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        List<Resource> resources = ResourceDatabase.database.Values.ToList();

        foreach (Resource resource in resources)
        {
            Button newButton = Instantiate<Button>(this.buttonPrefab, this.Content.transform);
            newButton.GetComponentInChildren<ResourceCanvasScript>().SetResource(resource);

            newButton.onClick.AddListener(() => this.Deactivate(resource));
        }
    }
}
