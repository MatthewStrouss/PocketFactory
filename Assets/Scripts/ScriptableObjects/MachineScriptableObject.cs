using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Machine")]
public class MachineScriptableObject : ScriptableObject
{
    //[SerializeField] private bool isUnlocked;
    //public bool IsUnlocked
    //{
    //    get => this.isUnlocked;
    //    set => this.isUnlocked = value;
    //}

    [SerializeField] private int buildCost;
    public int BuildCost
    {
        get => this.buildCost;
        set => this.buildCost = value;
    }

    [SerializeField] private int unlockCost;
    public int UnlockCost
    {
        get => this.unlockCost;
        set => this.unlockCost = value;
    }

    [SerializeField] private int actionTime;
    public int ActionTime
    {
        get => this.actionTime;
        set => this.actionTime = value;
    }

    [SerializeField] private int electricityCost;
    public int ElectricityCost
    {
        get => this.electricityCost;
        set => this.electricityCost = value;
    }

    [SerializeField] private int machineID;
    public int MachineID
    {
        get => this.machineID;
        set => this.machineID = value;
    }

    [SerializeField] private string machineName;
    public string MachineName
    {
        get => this.machineName;
        set => this.machineName = value;
    }

    [SerializeField] private bool canRotate;
    public bool CanRotate
    {
        get => this.canRotate;
        set => this.canRotate = value;
    }

    [SerializeField] private Sprite sprite;
    public Sprite Sprite
    {
        get => this.sprite;
        set => this.sprite = value;
    }
}
