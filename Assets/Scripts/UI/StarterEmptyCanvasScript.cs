using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarterEmptyCanvasScript : MonoBehaviour
{
    [SerializeField] private CancelCanvasScript XButton;
    [SerializeField] private RecipesPanelScript RecipesPanelScript;
    [SerializeField] private Text ClockText;

    private StarterController StarterController;
    private Action Callback;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate(StarterController starterController, Action callback)
    {
        this.StarterController = starterController;
        this.Callback = callback;

        //this.XButton.Activate(() => this.Deactivate(null));

        this.UpdateUI();
        this.gameObject.SetActive(true);
    }

    public void Deactivate(Recipe recipeChosen)
    {
        this.gameObject.SetActive(true);
        this.StarterController.ChosenRecipe = recipeChosen;
        this.Callback();
    }

    public void UpdateUI()
    {
        this.ClockText.text = $"{this.StarterController.MachineController.Machine.ActionTime} sec";
    }

    public void SelectResourceButton_Clicked()
    {
        this.RecipesPanelScript.Activate(
            "Basic",
            this.Deactivate
            );
    }
}
