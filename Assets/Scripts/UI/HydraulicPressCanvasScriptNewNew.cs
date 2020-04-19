using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HydraulicPressCanvasScriptNewNew : MonoBehaviour
{
    [SerializeField] private GameObject Content;
    [SerializeField] private Image RadialFill;
    [SerializeField] private Image MasterRadialFill;
    [SerializeField] private ResourceCanvasNew Result;
    [SerializeField] private GameObject ResourceRequirementPrefab;

    private HydraulicPressController hydraulicPressController;
    public HydraulicPressController HydraulicPressController
    {
        get => this.hydraulicPressController;
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

    public void Activate(HydraulicPressController hydraulicPressController)
    {
        this.hydraulicPressController = hydraulicPressController;
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

            if (i < this.HydraulicPressController.Inventory.Count)
            {
                child.gameObject.SetActive(true);
                child.GetComponent<ResourceRequirementScript>().Activate(this.HydraulicPressController.Inventory.ElementAt(i));
                child.GetComponent<ResourceRequirementScript>().UpdateResourceInventoryQuantity(this.HydraulicPressController.Inventory.ElementAt(i).Quantity);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }

        if (this.HydraulicPressController.NextItemToCraft == null)
        {
            this.Result.gameObject.SetActive(false);
        }
        else
        {
            this.Result.Activate(this.HydraulicPressController.NextItemToCraft);
            this.Result.gameObject.SetActive(true);
        }
    }
}
