using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarterPanelScript : MonoBehaviour
{
    public GameObject chosenImage;
    public StarterController starter;
    public GameObject recipePanel;

    public InputField quantityInputField;

    public GameObject ResourceGameObject;
    public CancelCanvasScript xButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void XButton_Click()
    {
        this.Deactivate();
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
        this.xButton.Activate(this.gameObject);
    }

    public void Deactivate()
    {
        this.starter = null;
        this.gameObject.SetActive(false);
        this.xButton.Deactivate();
    }

    public void RecipeButton_Click()
    {
        this.gameObject.SetActive(false);
        this.recipePanel.GetComponent<RecipesPanelScript>().Activate("Basic", (r) => 
        {
            Debug.Log(string.Format("Clicked on the recipe: {0}", r.Name));
            this.starter.SetRecipe(r);
            this.Activate();
        });
    }

    public void UpdateUI(StarterController starter)
    {
        this.starter = starter;
        StarterController sc = this.starter.GetComponent<StarterController>();
        //this.chosenImage.GetComponent<Image>().sprite = SpriteDatabase.Instance.GetSprite("Resource", sc.ChosenRecipe.Result.name);
        this.ResourceGameObject.GetComponent<ResourceCanvasScript>().SetResource(this.starter.ChosenRecipe.Result);
        this.quantityInputField.text = sc.SpawnCount.ToString();
    }

    public void QuantityInputField_OnEndEdit()
    {
        this.quantityInputField.text = Mathf.Clamp(Convert.ToInt32(this.quantityInputField.text), 1, 3).ToString();
        this.starter.SpawnCount = Convert.ToInt32(this.quantityInputField.text);
    }
}
