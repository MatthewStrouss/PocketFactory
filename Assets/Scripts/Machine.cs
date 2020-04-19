using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[Serializable]
public class Machine
{
    [SerializeField] private bool isUnlocked;
    public bool IsUnlocked
    {
        get => this.isUnlocked;
        set => this.isUnlocked = value;
    }

    [SerializeField] private long actionTime;
    public long ActionTime
    {
        get => this.actionTime;
        set => this.actionTime = value;
    }

    [SerializeField] private long electricityCost;
    public long ElectricityCost
    {
        get => this.electricityCost;
        set => this.electricityCost = value;
    }

    [SerializeField] private long buildCost;
    public long BuildCost
    {
        get => this.buildCost;
        set => this.buildCost = value;
    }

    [SerializeField] private long unlockCost;
    public long UnlockCost
    {
        get => this.unlockCost;
        set => this.unlockCost = value;
    }

    [SerializeField] private string machineName;
    public string MachineName
    {
        get => this.machineName;
        set => this.machineName = value;
    }

    [SerializeField] private int machineID;
    public int MachineID
    {
        get => this.machineID;
        set => this.machineID = value;
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

    public Machine()
    { }

    public Machine(bool isUnlocked, int electricityCost, int actionTime, MachineScriptableObject machineData)
    {
        this.isUnlocked = isUnlocked;
        this.electricityCost = electricityCost;
        this.actionTime = actionTime;
        //this.machineData = machineData;
    }

    public Machine(Machine machineToCopy)
    {
        this.isUnlocked = machineToCopy.isUnlocked;
        this.electricityCost = machineToCopy.electricityCost;
        this.actionTime = machineToCopy.actionTime;
        //this.machineData = machineToCopy.machineData;
        this.sprite = machineToCopy.Sprite;
    }

    public Machine(MachineScriptableObject machineScriptableObject)
    {
        this.isUnlocked = machineScriptableObject.IsUnlocked;
        this.electricityCost = machineScriptableObject.ElectricityCost;
        this.actionTime = machineScriptableObject.ActionTime;
        this.BuildCost = machineScriptableObject.BuildCost;
        this.UnlockCost = machineScriptableObject.UnlockCost;
        this.MachineName = machineScriptableObject.Name;
        this.MachineID = machineScriptableObject.MachineID;
        this.CanRotate = machineScriptableObject.CanRotate;
        this.sprite = machineScriptableObject.Sprite;
    }

    public void Unlock()
    {
        this.IsUnlocked = true;
    }
}
