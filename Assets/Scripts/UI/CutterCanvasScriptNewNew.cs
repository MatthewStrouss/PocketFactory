using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CutterCanvasScriptNewNew : MonoBehaviour
{
    [SerializeField] private GameObject Content;
    [SerializeField] private Image RadialFill;
    [SerializeField] private Image MasterRadialFill;
    [SerializeField] private ResourceCanvasNew Result;
    [SerializeField] private GameObject ResourceRequirementPrefab;

    private CutterController cutterController;
    public CutterController CutterController
    {
        get => this.cutterController;
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

    public void Activate(CutterController cutterController)
    {
        this.cutterController = cutterController;
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

            if (i < this.CutterController.Inventory.Count)
            {
                child.gameObject.SetActive(true);
                child.GetComponent<ResourceRequirementScript>().Activate(this.CutterController.Inventory.ElementAt(i));
                child.GetComponent<ResourceRequirementScript>().UpdateResourceInventoryQuantity(this.CutterController.Inventory.ElementAt(i).Quantity);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }

        if (this.CutterController.NextItemToCraft == null)
        {
            this.Result.gameObject.SetActive(false);
        }
        else
        {
            this.Result.Activate(this.CutterController.NextItemToCraft);
            this.Result.gameObject.SetActive(true);
        }
    }
}
