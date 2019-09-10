using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationActionCanvasScript : MonoBehaviour
{
    [SerializeField] private CancelCanvasScript XButton;
    [SerializeField] private PlayerScript playerScript;

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
        this.XButton.Activate(this.Deactivate);
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
        this.XButton.Deactivate();
    }

    public void RotateLeftButton_Clicked()
    {
        this.playerScript.RotateSelection(Quaternion.Euler(0f, 0f, 270f));
    }

    public void RotateUpButton_Clicked()
    {
        this.playerScript.RotateSelection(Quaternion.Euler(0f, 0f, 180f));
    }

    public void RotateRightButton_Clicked()
    {
        this.playerScript.RotateSelection(Quaternion.Euler(0f, 0f, 90f));
    }

    public void RotateDownButton_Clicked()
    {
        this.playerScript.RotateSelection(Quaternion.Euler(0f, 0f, 0f));
    }
}
