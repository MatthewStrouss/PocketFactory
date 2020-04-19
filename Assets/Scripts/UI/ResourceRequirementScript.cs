using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceRequirementScript : MonoBehaviour
{
    [SerializeField] private Image ResourceImage;
    [SerializeField] private Text ResourceNameText;
    [SerializeField] private Text ResourceQuantityRequiredText;
    [SerializeField] private Text ResourceInventoryQuantityText;

    public Resource Resource;
    private long ResourceInventoryQuantity;

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

    public void Activate(Resource resource)
    {
        this.Resource = resource;
        this.Activate();
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
        this.ResourceImage.sprite = this.Resource.Sprite;
        this.ResourceNameText.text = this.Resource.name;
        this.ResourceQuantityRequiredText.text = Assets.Scripts.Extensions.LongExtensions.FormatNumber(this.Resource.Quantity);
        this.ResourceInventoryQuantityText.text = Assets.Scripts.Extensions.LongExtensions.FormatNumber(this.ResourceInventoryQuantity);
    }

    public void UpdateResourceInventoryQuantity(long? resourceInventoryQuantity)
    {
        this.ResourceInventoryQuantity = resourceInventoryQuantity.GetValueOrDefault(0);
        this.UpdateUI();
    }
}
