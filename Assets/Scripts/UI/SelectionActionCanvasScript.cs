using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SelectionActionCanvasScript : MonoBehaviour
{
    [SerializeField] private PlayerScript playerScript;
    [SerializeField] private CancelCanvasScript xButton;
    [SerializeField] private GameObject navBar;
    [SerializeField] private OkCancelCanvasScript okCancel;

    [SerializeField] private RotationActionCanvasScript rotationActionCanvasScript;
    [SerializeField] private FlipSelectionCanvasScript FlipSelectionCanvasScript;
    [SerializeField] private MoveModeCanvasScript MoveModeCanvasScript;
    [SerializeField] private SaveBlueprintScript SaveBlueprintScript;

    private List<GameObject> SelectedGameObjects;

    private Action action;

    public void Activate()
    {
        this.gameObject.SetActive(true);
        this.xButton.Activate(this.gameObject, () => this.Deactivate());
    }

    public void Activate(Action action)
    {
        this.action = action;
        this.Activate();
    }

    public void Activate(List<GameObject> selectedGameObjects)
    {
        this.SelectedGameObjects = selectedGameObjects;
        this.Activate();
    }

    public void Deactivate()
    {
        this.SelectedGameObjects?.ForEach(x => x?.GetComponent<MachineController>()?.DeactivateSelected());
        this.SelectedGameObjects?.Clear();

        this.gameObject.SetActive(false);
        this.xButton.Deactivate();
        this.action?.DynamicInvoke();
    }

    public void MoveButton_Clicked()
    {
        this.MoveModeCanvasScript.Activate(
            this.SelectedGameObjects,
            this.AcceptMove,
            this.CancelMove
            );
    }

    public void AcceptMove()
    {
        this.gameObject.gameObject.SetActive(true);
    }

    public void CancelMove()
    {
        this.gameObject.gameObject.SetActive(true);
    }


    public void RotateButton_Clicked()
    {
        this.rotationActionCanvasScript.Activate(
            this.SelectedGameObjects,
            this.AcceptRotate,
            this.CancelRotate
            );

        this.gameObject.gameObject.SetActive(false);
    }

    public void AcceptRotate()
    {
        this.gameObject.gameObject.SetActive(true);
    }

    public void CancelRotate()
    {
        this.gameObject.gameObject.SetActive(true);
    }

    public void FlipButton_Clicked()
    {
        this.FlipSelectionCanvasScript.Activate(
            this.SelectedGameObjects,
            this.AcceptFlip,
            this.CancelFlip
            );
    }

    public void AcceptFlip()
    {
        this.gameObject.gameObject.SetActive(true);
    }

    public void CancelFlip()
    {
        this.gameObject.gameObject.SetActive(true);
    }

    public void SaveBlueprintButton_Clicked()
    {
        this.SaveBlueprintScript.Activate(
            this.SelectedGameObjects,
            this.AcceptSaveBlueprint,
            this.CancelSaveBlueprint
            );
    }

    public void AcceptSaveBlueprint()
    {
        this.gameObject.gameObject.SetActive(true);
    }
    
    public void CancelSaveBlueprint()
    {
        this.gameObject.gameObject.SetActive(true);
    }

    public void SellButton_Clicked()
    {
        if (this.SelectedGameObjects.Count > 0)
        {
            this.okCancel.Activate(
                $"Are you sure you want to sell {this.SelectedGameObjects.Count} machines for {Mathf.RoundToInt(0.9f * this.SelectedGameObjects.Sum(x => x.GetComponent<MachineController>().Machine.BuildCost))}?",
                this.AcceptSell,
                this.CancelSell
                );
        }
    }

    public void AcceptSell()
    {
        this.SelectedGameObjects?.ForEach(x => x.GetComponent<MachineController>().Sell());
        this.SelectedGameObjects?.Clear();
    }

    public void CancelSell()
    {
        this.okCancel.Deactivate();
    }
}
