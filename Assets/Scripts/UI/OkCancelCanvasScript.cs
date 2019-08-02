using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OkCancelCanvasScript : MonoBehaviour
{
    private Action okButtonAction;
    private Action cancelButtonAction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate(Action okButtonAction, Action cancelButtonAction)
    {
        this.okButtonAction = okButtonAction;
        this.cancelButtonAction = cancelButtonAction;
        this.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void OkButton_Clicked()
    {
        okButtonAction?.DynamicInvoke();
        this.Deactivate();
    }

    public void CancelButton_Clicked()
    {
        cancelButtonAction?.DynamicInvoke();
        this.Deactivate();
    }
}
