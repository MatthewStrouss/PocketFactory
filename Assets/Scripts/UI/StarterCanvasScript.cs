using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterCanvasScript : MonoBehaviour
{
    [SerializeField] private CancelCanvasScript XButton;
    [SerializeField] private StarterEmptyCanvasScript StarterEmptyCanvasScript;
    [SerializeField] private StarterEnabledCanvas StarterEnabledCanvas;

    private StarterController StarterController;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate(StarterController starterController)
    {
        this.StarterController = starterController;

        this.XButton.Activate(this.Deactivate);

        
        this.UpdateUI();
        this.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
        if (this.StarterController.ChosenRecipe?.Name != "(None)")
        {
            // Not empty
            this.StarterEmptyCanvasScript.gameObject.SetActive(false);
            this.StarterEnabledCanvas.gameObject.SetActive(true);
        }
        else
        {
            // Empty
            this.StarterEmptyCanvasScript.gameObject.SetActive(true);
            this.StarterEnabledCanvas.gameObject.SetActive(false);

            this.StarterEmptyCanvasScript.Activate(
                this.StarterController,
                this.UpdateUI
                );
        }
    }
}
