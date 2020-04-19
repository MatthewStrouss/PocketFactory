using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarterEmptyCanvasScript : MonoBehaviour
{
    [SerializeField] private CancelCanvasScript XButton;
    [SerializeField] private Text ClockText;

    private StarterController StarterController;
    private Action<bool> Callback;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate(StarterController starterController, Action<bool> callback)
    {
        this.StarterController = starterController;
        this.Callback = callback;

        //this.XButton.Activate(() => this.Deactivate(null));

        this.UpdateUI();
        this.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
        this.Callback.DynamicInvoke(false);
    }

    public void UpdateUI()
    {
        this.ClockText.text = $"{this.StarterController.MachineController.Machine.ActionTime} sec";
    }

    public void SelectResourceButton_Clicked()
    {
        this.Callback.DynamicInvoke(true);
    }
}
