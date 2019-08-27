using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManagerController : MonoBehaviour
{
    [Header("Main GUI Elements")]
    public GameObject researchCanvas;
    public GameObject buildCanvas;
    public GameObject selectorCanvas;
    public GameObject selectResourceCanvas;

    [Header("Machine GUI Elements")]
    public GameObject starterCanvas;
    public GameObject splitterCanvas;
    public GameObject crafterCanvas;
    public GameObject sellerCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
public struct GUIKeyValuePair
{
    public string Key;
    public GameObject Value;
}
