using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WireDrawerCanvasScriptNewNew : MonoBehaviour
{
    [SerializeField] private GameObject Content;
    [SerializeField] private Image RadialFill;
    [SerializeField] private Image MasterRadialFill;
    [SerializeField] private ResourceCanvasNew Result;
    [SerializeField] private GameObject ResourceRequirementPrefab;

    private WireDrawerController wireDrawerController;
    public WireDrawerController WireDrawerController
    {
        get => this.wireDrawerController;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.RadialFill.fillAmount = this.MasterRadialFill.fillAmount;
    }

    private void Activate()
    {
        this.UpdateUI();
        this.gameObject.SetActive(true);
    }

    public void Activate(WireDrawerController wireDrawerController)
    {
        this.wireDrawerController = wireDrawerController;
        this.Activate();
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
        for (int i = 0; i < 10; i++)
        {
            Transform child = this.Content.transform.GetChild(i);

            if (i < this.WireDrawerController.Inventory.Count)
            {
                child.gameObject.SetActive(true);
                child.GetComponent<ResourceRequirementScript>().Activate(this.WireDrawerController.Inventory.ElementAt(i));
                child.GetComponent<ResourceRequirementScript>().UpdateResourceInventoryQuantity(this.WireDrawerController.Inventory.ElementAt(i).Quantity);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }

        if (this.WireDrawerController.NextItemToCraft == null)
        {
            this.Result.gameObject.SetActive(false);
        }
        else
        {
            this.Result.Activate(this.WireDrawerController.NextItemToCraft);
            this.Result.gameObject.SetActive(true);
        }
    }
}
