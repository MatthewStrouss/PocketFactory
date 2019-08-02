using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPanelScript : MonoBehaviour
{
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
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void XButton_Click()
    {
        this.Deactivate();
    }

    public void RotateLeftButton_Clicked()
    {
        Camera.main.GetComponent<PlayerScript>().SetRotation(Quaternion.Euler(0f, 0f, 270f));
        this.gameObject.SetActive(false);
    }

    public void RotateUpButton_Clicked()
    {
        Camera.main.GetComponent<PlayerScript>().SetRotation(Quaternion.Euler(0f, 0f, 180f));
        this.gameObject.SetActive(false);
    }

    public void RotateRightButton_Clicked()
    {
        Camera.main.GetComponent<PlayerScript>().SetRotation(Quaternion.Euler(0f, 0f, 90f));
        this.gameObject.SetActive(false);
    }

    public void RotateDownButton_Clicked()
    {
        Camera.main.GetComponent<PlayerScript>().SetRotation(Quaternion.Euler(0f, 0f, 0f));
        this.gameObject.SetActive(false);
    }
}
