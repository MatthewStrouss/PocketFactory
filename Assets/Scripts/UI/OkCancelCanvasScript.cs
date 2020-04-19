using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OkCancelCanvasScript : MonoBehaviour
{
    public Text instructionText;
    public Text instructionText2;
    private Action okButtonAction;
    private Action cancelButtonAction;
    public GameObject Navbar;
    public Button okButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
        this.Navbar.SetActive(false);
    }

    public void Activate(Action okButtonAction, Action cancelButtonAction)
    {
        this.okButtonAction = okButtonAction;
        this.cancelButtonAction = cancelButtonAction;
        this.Activate();
    }

    public void Activate(string instructionText, Action okButtonAction, Action cancelButtonAction, bool isOkButtonActive = true)
    {
        UpdateInstructionText(instructionText);
        this.okButtonAction = okButtonAction;
        this.cancelButtonAction = cancelButtonAction;
        this.SetOkButtonActive(isOkButtonActive);
        this.Activate();
    }

    public void Activate(string instructionText, string instructionText2, Action okButtonAction, Action cancelButtonAction, bool isOkButtonActive = true)
    {
        UpdateInstructionText(instructionText);
        UpdateInstructionText2(instructionText2);
        this.okButtonAction = okButtonAction;
        this.cancelButtonAction = cancelButtonAction;
        this.SetOkButtonActive(isOkButtonActive);
        this.Activate();
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
        this.Navbar.SetActive(true);
    }

    public void SetOkButtonActive(bool isOkButtonActive)
    {
        this.okButton.interactable = isOkButtonActive;
    }

    public void OkButton_Clicked()
    {
        this.Deactivate();
        okButtonAction?.DynamicInvoke();
    }

    public void CancelButton_Clicked()
    {
        this.Deactivate();
        cancelButtonAction?.DynamicInvoke();
    }

    public void UpdateInstructionText(string instructionText)
    {
        this.instructionText.text = instructionText;
    }

    public void UpdateInstructionText2(string instructionText)
    {
        this.instructionText2.text = instructionText;
    }
}
