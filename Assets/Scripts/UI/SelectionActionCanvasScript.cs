using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionActionCanvasScript : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript;
    [SerializeField] private GameObject xButton;
    private Action action;

    public void Activate()
    {
        this.gameObject.SetActive(true);
        this.xButton.GetComponent<CancelCanvasScript>().Activate(this.gameObject, () => this.Deactivate());
    }

    public void Activate(Action action)
    {
        this.action = action;
        this.Activate();
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
        this.xButton.GetComponent<CancelCanvasScript>().Deactivate();
        this.action?.DynamicInvoke();
    }

    public void MoveButton_Clicked()
    {

    }

    public void RotateButton_Clicked()
    {

    }

    public void FlipXButton_Clicked()
    {
        this.playerScript.FlipSelectionX();
    }

    public void FlipYButton_Clicked()
    {
        this.playerScript.FlipSelectionY();
    }

    public void SaveBlueprintButton_Clicked()
    {
        this.playerScript.SaveBlueprint();
    }

    public void SellButton_Clicked()
    {
        this.playerScript.SellSelection();
    }
}
