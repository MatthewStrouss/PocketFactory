using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FurnaceCanvasScriptNewNew : MonoBehaviour
{
    [SerializeField] private GameObject Content;
    [SerializeField] private Image RadialFill;
    [SerializeField] private Image MasterRadialFill;
    [SerializeField] private ResourceCanvasNew Result;
    [SerializeField] private GameObject ResourceRequirementPrefab;

    private FurnaceController furnaceController;
    public FurnaceController FurnaceController
    {
        get => this.furnaceController;
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

    public void Activate(FurnaceController furnaceController)
    {
        this.furnaceController = furnaceController;
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

            if (i < this.FurnaceController.Inventory.Count)
            {
                child.gameObject.SetActive(true);
                child.GetComponent<ResourceRequirementScript>().Activate(this.FurnaceController.Inventory.ElementAt(i));
                child.GetComponent<ResourceRequirementScript>().UpdateResourceInventoryQuantity(this.FurnaceController.Inventory.ElementAt(i).Quantity);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }

        if (this.FurnaceController.NextItemToCraft == null)
        {
            this.Result.gameObject.SetActive(false);
        }
        else
        {
            this.Result.Activate(this.FurnaceController.NextItemToCraft);
            this.Result.gameObject.SetActive(true);
        }
    }
}
