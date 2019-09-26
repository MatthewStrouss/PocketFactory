using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCanvasNew : MonoBehaviour
{
    [SerializeField] private Image Image;
    [SerializeField] private Text NameText;
    [SerializeField] private Text QuantityText;

    private Resource Resource;
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
        this.Image.sprite = this.Resource.Sprite;
        this.NameText.text = this.Resource.name;
        this.QuantityText.text = this.Resource.Quantity.ToString();
    }

    public string ResourceName() => this.Resource.name;
}
