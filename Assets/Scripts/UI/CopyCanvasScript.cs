using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyCanvasScript : MonoBehaviour
{
    [SerializeField]
    private InputField inputField;

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
        this.gameObject.SetActive(false);
    }

    public void UpdateUI(string base64String)
    {
        this.inputField.text = base64String;
    }

    public void CopyButton_Click()
    {
        GUIUtility.systemCopyBuffer = this.inputField.text;
    }
}
