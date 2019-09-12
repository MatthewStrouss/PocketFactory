using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCanvasScript : MonoBehaviour
{
    [SerializeField] private Image resourceImage;
    [SerializeField] private Text resourceName;
    [SerializeField] private Text resourceQuantity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetResource(Resource resource)
    {
        //this.resourceImage.sprite = SpriteDatabase.Instance.GetSprite("Resource", resource.name);
        this.resourceImage.sprite = resource.Sprite;
        this.resourceName.text = resource.name;

        if (this.resourceQuantity != null)
        {
            this.resourceQuantity.text = resource.Quantity.ToString();
        }

        //Debug.Log(string.Format("Setting resource to {0} and sprite to {1}", resource.name, this.resourceImage.sprite));
    }
}
