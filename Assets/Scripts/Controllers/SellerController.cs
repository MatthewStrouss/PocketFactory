using Assets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SellerController : MonoBehaviour, IMachineController
{
    public MachineController MachineController;

    public Dictionary<string, Resource> Inventory;
    public GameObject SellerGUI;

    public void ActionToPerformOnTimer()
    {
        
    }

    public void AddToInventory(Resource resourceToAdd)
    {
        throw new System.NotImplementedException();
    }

    public void OnClick()
    {
        this.SellerGUI.GetComponent<SellerCanvasScript>().Activate();
        this.UpdateGUI();
    }

    public void OnCollision(Collider2D col)
    {
        this.MachineController.SubtractElectricityCost();

        ResourceController rc = col.GetComponent<ResourceController>();

        this.Inventory[rc.resource.name].Quantity += rc.resource.Quantity;

        rc.SellResource();

        this.UpdateGUI();
    }
    void Awake()
    {
        this.Inventory = new Dictionary<string, Resource>(ResourceDatabase.database);
        
        foreach(KeyValuePair<string, Resource> resource in this.Inventory)
        {
            resource.Value.Quantity = 0;
        }

        this.SellerGUI = PrefabDatabase.Instance.GetPrefab("UI", "Seller");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateGUI()
    {
        this.SellerGUI.GetComponent<SellerCanvasScript>().UpdateGUI(this);
    }

    public void SetControllerValues(IMachineController other)
    {
        SellerController otherController = other as SellerController;
        this.Inventory = otherController.Inventory;
    }
}
