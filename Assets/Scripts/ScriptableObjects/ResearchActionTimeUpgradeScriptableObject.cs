using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(menuName = "Research/New Action Time Upgrade")]
public class ResearchActionTimeUpgradeScriptableObject : ScriptableObject, IResearch
{
    [Header("IResearch")]
    [SerializeField] private bool isUnlocked;
    public bool IsUnlocked { get => this.isUnlocked; set => this.isUnlocked = value; }

    [SerializeField] private long unlockCost;
    public long UnlockCost { get => this.unlockCost; set => this.unlockCost = value; }

    [SerializeField] private string description;
    public string Description { get => this.description; set => this.description = value; }

    [SerializeField] private new string name;
    public string Name { get => this.name; set => this.name = value; }

    [SerializeField] private Sprite sprite;
    public Sprite Sprite { get => this.sprite; set => this.sprite = value; }

    [Header("Class Specific fields")]
    [SerializeField] private MachineScriptableObject machineScriptableObject;
    public MachineScriptableObject MachineScriptableObject { get => this.machineScriptableObject; set => this.machineScriptableObject = value; }

    [SerializeField] private long fieldValue;
    public long FieldValue { get => this.fieldValue; set => this.fieldValue = value; }

    public void Unlock()
    {
        MachineDatabase.database[this.MachineScriptableObject.Name].ActionTime -= this.FieldValue;
    }
}