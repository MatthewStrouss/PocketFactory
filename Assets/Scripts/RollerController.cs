using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerController : MonoBehaviour, IMachineController
{
    public MachineController MachineController;

    private GameObject nextMovingPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        nextMovingPoint = transform.GetChild(0).gameObject;
    }

    public void OnCollision(Collider2D col)
    {
        // Subtract money
        this.MachineController.SubtractElectricityCost();

        // Tell resource to move to new location
        col.gameObject.GetComponent<ResourceController>().Move(nextMovingPoint.transform.position);
    }

    public void AddToInventory(Resource resourceToAdd)
    {
        throw new System.NotImplementedException();
    }

    public void ActionToPerformOnTimer()
    {

    }

    public void OnClick()
    {
        throw new System.NotImplementedException();
    }

    public void SetControllerValues(IMachineController other)
    {
        
    }
}
