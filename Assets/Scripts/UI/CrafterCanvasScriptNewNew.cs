using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrafterCanvasScriptNewNew : MonoBehaviour
{
    [SerializeField] private CrafterCanvasEmptyScript CrafterCanvasEmptyScript;
    [SerializeField] private CrafterCanvasActiveStateScript CrafterCanvasActiveStateScript;

    private CrafterController CrafterController;

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

    public void Activate(CrafterController crafterController)
    {
        this.CrafterController = crafterController;
        this.Activate();
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
        this.CrafterCanvasEmptyScript.gameObject.SetActive(false);
        this.CrafterCanvasActiveStateScript.gameObject.SetActive(false);

        if (this.CrafterController.ChosenRecipe.Name == "(None)")
        {
            this.CrafterCanvasEmptyScript.Activate(this.CrafterController, this.UpdateUI);
        }
        else
        {
            this.CrafterCanvasActiveStateScript.Activate(this.CrafterController);
        }
    }
}
