using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarterPanelScript : MonoBehaviour
{
    public GameObject chosenImage;
    public StarterController starter;
    public GameObject recipePanel;

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
    }

    public void Deactivate()
    {
        this.starter = null;
        this.gameObject.SetActive(false);
    }

    public void RecipeButton_Click()
    {
        this.gameObject.SetActive(false);
        this.recipePanel.GetComponent<RecipesPanelScript>().Activate(this.starter.gameObject, () => this.Activate());
    }

    public void UpdateUI(StarterController starter)
    {
        this.starter = starter;
        StarterController sc = this.starter.GetComponent<StarterController>();
        this.chosenImage.GetComponent<Image>().sprite = SpriteDatabase.Instance.GetSprite("Resource", sc.chosenRecipe.Result.name);
    }
}
