using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsCanvasScript : MonoBehaviour
{
    [SerializeField] private OptionsMenuCanvasScript OptionsMenuCanvasScript;

    public void OptionsMenuButton_Clicked()
    {
        this.OptionsMenuCanvasScript.Activate();
    }
}
