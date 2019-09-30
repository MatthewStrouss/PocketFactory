using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CrafterCanvasActiveStateScript : MonoBehaviour
{
    [SerializeField] private GameObject RecipeContent;
    [SerializeField] private ResourceCanvasNew Result;
    [SerializeField] private ResourceCanvasNew ResourceCanvasNewPrefab;
    [SerializeField] private Image RadialFill;
    [SerializeField] private Image MasterRadialFill;
    [SerializeField] private GameObject ResourceRequirementPrefab;

    public CrafterController CrafterController;
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
        this.SetupUI();
        this.UpdateUI();
        this.gameObject.SetActive(true);
    }

    public void Activate(CrafterController crafterController)
    {
        this.CrafterController = crafterController;
        this.Activate();
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void SetupUI()
    {
        foreach (Transform child in this.RecipeContent.transform)
        {
            Destroy(child.gameObject);
        }

        this.CrafterController.ChosenRecipe.Requirements.ForEach(x =>
        {
            //ResourceCanvasNew newRC = Instantiate(this.ResourceCanvasNewPrefab, this.RecipeContent.transform);
            //newRC.Activate(x);

            GameObject newGO = Instantiate(this.ResourceRequirementPrefab, this.RecipeContent.transform);
            newGO.GetComponent<ResourceRequirementScript>().Activate(x);
        });
    }

    public void UpdateUI()
    {
        foreach (Transform child in this.RecipeContent.transform)
        {
            ResourceRequirementScript resourceRequirementScript = child.GetComponent<ResourceRequirementScript>();
            resourceRequirementScript.UpdateResourceInventoryQuantity(this.CrafterController.Inventory.FirstOrDefault(x => x.id.Equals(resourceRequirementScript.Resource.id))?.Quantity);
        }

        this.Result.Activate(this.CrafterController.ChosenRecipe.Result);
    }
}
