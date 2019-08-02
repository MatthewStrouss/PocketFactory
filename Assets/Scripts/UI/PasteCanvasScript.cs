using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasteCanvasScript : MonoBehaviour
{
    [SerializeField]
    private InputField inputField;
    private Action<string> callback;

    private void Awake()
    {
        this.inputField.characterLimit = 0;
    }
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

    public void Activate(Action<string> callback)
    {
        this.callback = callback;
        this.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void UpdateUI()
    {

    }

    public void PasteButton_Click()
    {
        this.Deactivate();
        this.callback(this.inputField.text);
    }

    public void PasteTextButton_Click()
    {
        this.inputField.text = GUIUtility.systemCopyBuffer;
    }
}
