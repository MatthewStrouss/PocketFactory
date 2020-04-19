using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SellerCanvasScriptNewNew : MonoBehaviour
{
    [SerializeField] private InputField FilterInputField;
    [SerializeField] private GameObject Content;
    [SerializeField] private GameObject ResourceCanvasPrefab;

    private SellerController SellerController;

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
        this.UpdateUI();
        this.gameObject.SetActive(true);
    }

    public void Activate(SellerController sellerController)
    {
        this.SellerController = sellerController;
        this.Activate();
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
        foreach(Transform child in this.Content.transform)
        {
            Destroy(child.gameObject);
        }

        this.SellerController.Inventory.Values.ToList().ForEach(x =>
        {
            GameObject newGO = Instantiate(this.ResourceCanvasPrefab, this.Content.transform);
            newGO.GetComponent<ResourceCanvasNew>().Activate(x);
        });
    }

    public void FilterInputField_Updated()
    {
        foreach (Transform child in this.Content.transform)
        {
            child.gameObject.SetActive(child.GetComponent<ResourceCanvasNew>().ResourceName().IndexOf(this.FilterInputField.text, System.StringComparison.InvariantCultureIgnoreCase) >= 0);
        }
    }
}
