using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[Serializable]
public class Machine
{
    private bool isUnlocked;
    public bool IsUnlocked
    {
        get => this.isUnlocked;
        set => this.isUnlocked = value;
    }

    private int buildCost;
    public int BuildCost
    {
        get => this.buildCost;
        set => this.buildCost = value;
    }

    private int unlockCost;
    public int UnlockCost
    {
        get => this.unlockCost;
        set => this.unlockCost = value;
    }

    private int actionTime;
    public int ActionTime
    {
        get => this.actionTime;
        set => this.actionTime = value;
    }

    private int electricityCost;
    public int ElectricityCost
    {
        get => this.electricityCost;
        set => this.electricityCost = value;
    }

    private int machineID;
    public int MachineID
    {
        get => this.machineID;
        set => this.machineID = value;
    }

    private string machineName;
    public string MachineName
    {
        get => this.machineName;
        set => this.machineName = value;
    }

    private bool canRotate;
    public bool CanRotate
    {
        get => this.canRotate;
        set => this.canRotate = value;
    }

    public Machine()
    { }

    public Machine(bool isUnlocked, int machineID, string machineName, int electricityCost, int actionTime, int buildCost, int unlockCost, bool canRotate)
    {
        this.isUnlocked = isUnlocked;
        this.machineID = machineID;
        this.machineName = machineName;
        this.electricityCost = electricityCost;
        this.actionTime = actionTime;
        this.buildCost = buildCost;
        this.unlockCost = unlockCost;
        this.canRotate = canRotate;
    }

    public Machine(Machine machineToCopy)
    {
        this.isUnlocked = machineToCopy.isUnlocked;
        this.machineID = machineToCopy.machineID;
        this.machineName = machineToCopy.machineName;
        this.electricityCost = machineToCopy.electricityCost;
        this.actionTime = machineToCopy.actionTime;
        this.buildCost = machineToCopy.buildCost;
        this.unlockCost = machineToCopy.unlockCost;
        this.canRotate = machineToCopy.canRotate;
    }
}
