using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterEnabledCanvas : MonoBehaviour
{

    private StarterController StarterController;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Activate(StarterController starterController)
    {
        this.StarterController = starterController;

        this.UpdateUI();
        this.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void UpdateUI()
    {

    }
}
