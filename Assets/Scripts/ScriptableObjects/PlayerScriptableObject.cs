using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class PlayerScriptableObject : ScriptableObject
{
    [SerializeField] private LongEvent onMoneyUpdate;

    [SerializeField] private long money;
    public long Money
    {
        get
        {
            return this.money;
        }
        set
        {
            this.money = value;
            onMoneyUpdate?.Raise(this.money);
        }
    }

    public void AddMoney(long moneyToAdd, bool addToAverage = true)
    {
        this.Money += moneyToAdd;

        if (addToAverage)
        {

        }
    }

    public void SubMoney(long moneyToSub, bool addToAverage = true)
    {
        this.Money -= moneyToSub;

        if (addToAverage)
        {

        }
    }
}

