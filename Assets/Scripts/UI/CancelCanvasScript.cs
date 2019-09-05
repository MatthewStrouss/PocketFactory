using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelCanvasScript : MonoBehaviour
{
    public GameObject Navbar;
    public GameObject objectToCancel;
    private Action action;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
        this.Navbar.SetActive(true);
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
        this.Navbar.SetActive(false);
    }

    public void Activate(GameObject objectToCancel, Action action = null)
    {
        this.objectToCancel = objectToCancel;
        this.action = action;
        this.Activate();
    }

    public void XButton_Clicked()
    {
        this.objectToCancel.SetActive(false);
        this.Deactivate();
        this.action?.DynamicInvoke();
    }
}
