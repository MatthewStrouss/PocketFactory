using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionCanvasScript : MonoBehaviour
{
    private PlayerScript playerScript;

    private void Awake()
    {
        this.playerScript = Camera.main.GetComponent<PlayerScript>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleActive()
    {
        if (this.gameObject.activeSelf)
        {
            this.Deactivate();
        }
        else
        {
            this.Activate();
        }
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void SelectButton_Click()
    {
        this.playerScript.StartSelectMode();
    }

    public void CopyButton_Click()
    {
        this.playerScript.Copy();
    }

    public void PasteButton_Click()
    {
        this.playerScript.PasteUI();
    }

    public void SellButton_Click()
    {
        this.playerScript.SellSelection();
    }

    public void FlipXButton_Click()
    {
        this.playerScript.FlipSelectionX();
    }

    public void FlipYButton_Click()
    {
        this.playerScript.FlipSelectionY();
    }
}
