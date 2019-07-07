using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineController : MonoBehaviour
{
    public MonoBehaviour controller;

    bool IsUnlocked
    {
        get;
        set;
    }

    //float buildCost;

    //float unlockCost;

    //float actionTime;

    //float electricityCost;

    //string machineID;

    //string machineName;

    // Start is called before the first frame update
    void Start()
    {
        controller.InvokeRepeating("ActionToPerformOnTimer", 0.0f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ResourceController>() != null)
        {
            (controller as IMachineController).OnCollision(collision);
        }
    }
}
