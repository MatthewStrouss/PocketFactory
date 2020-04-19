using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutCanvasScript : MonoBehaviour
{
    [SerializeField] private CancelCanvasScript CancelCanvasScript;
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
        this.CancelCanvasScript.Activate(this.Deactivate);
        this.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}
