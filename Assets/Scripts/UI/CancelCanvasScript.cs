using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelCanvasScript : MonoBehaviour
{
    public GameObject Navbar;
    public GameObject objectToCancel;

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

    public void Activate(GameObject objectToCancel)
    {
        this.objectToCancel = objectToCancel;
        this.Activate();
    }

    public void XButton_Clicked()
    {
        this.objectToCancel.SetActive(false);
        this.Deactivate();
    }
}
