using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellerCanvasScript : MonoBehaviour
{
    public SellerController SellerController;
    public GameObject ScrollViewContent;
    public Button ButtonPrefab;
    [SerializeField] private CancelCanvasScript XButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
        this.XButton.Activate(this.XButton_Click);
    }

    public void UpdateGUI(SellerController sellerController)
    {
        this.SellerController = sellerController;

        foreach (Transform child in this.ScrollViewContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (KeyValuePair<string, Resource> resource in this.SellerController.Inventory)
        {
            if (resource.Value.Quantity > 0)
            {
                Button newButton = Instantiate<Button>(this.ButtonPrefab, this.ScrollViewContent.transform);
                Text[] texts = newButton.GetComponentsInChildren<Text>();

                texts[0].text = resource.Value.name;
                texts[1].text = resource.Value.Quantity.ToString();
                newButton.image.sprite = SpriteDatabase.Instance.GetSprite("Resource", resource.Value.name);
            }
        }
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
        this.XButton.Deactivate();
    }

    public void XButton_Click()
    {
        this.Deactivate();
    }
}
