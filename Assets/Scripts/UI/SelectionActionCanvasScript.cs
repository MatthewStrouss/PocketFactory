using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionActionCanvasScript : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript;
    [SerializeField] private GameObject xButton;
    [SerializeField] private RotationActionCanvasScript rotationCanvas;
    [SerializeField] private OkCancelCanvasScript okCancel;
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
        this.playerScript.MoveMode();

        this.okCancel.Activate(
            "Tap to place center of selection",
            this.playerScript.AcceptMoveSelection,
            this.playerScript.CancelMoveSelection
            );
    }

    public void RotateButton_Clicked()
    {
        this.rotationCanvas.Activate();
        this.gameObject.gameObject.SetActive(false);
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
