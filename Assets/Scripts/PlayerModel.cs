using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerModel
{
    public event EventHandler MoneyUpdated;

    [SerializeField] private long money;
    public long Money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
            OnMoneyUpdated(new MoneyEventArgs() { money = this.money });
        }
    }

    [SerializeField] private bool hasClickedDonationButton;
    public bool HasClickedDonationButton
    {
        get => this.hasClickedDonationButton;
        set => this.hasClickedDonationButton = value;
    }

    public void SetValues(PlayerModel otherPlayerModel)
    {
        this.Money = otherPlayerModel.Money;
    }

    public void AddMoney(long moneyToAdd, bool addToAverage = true)
    {
        Money += moneyToAdd;

        if (addToAverage)
        {

        }
    }

    public void SubMoney(long moneyToSub, bool addToAverage = true)
    {
        Money -= moneyToSub;

        if (addToAverage)
        {

        }
    }

    public void SetMoney(long moneyToSet)
    {
        Money = moneyToSet;
    }

    public void OnMoneyUpdated(MoneyEventArgs e)
    {
        EventHandler handler = MoneyUpdated;
        handler?.Invoke(null, e);
    }

    public bool CanAfford(long moneyNeeded)
    {
        return this.money >= moneyNeeded;
    }
}

public class MoneyEventArgs : EventArgs
{
    public long money;
}

//public static class Extensions
//{
//    public static PlayerModel ToPlayerModel(this PlayerScriptableObject playerScript)
//    {
//        PlayerModel playerModel = new PlayerModel();
//        playerModel.Money = playerScript.Money;

//        return playerModel;
//    }

//    public static void ToPlayerScript(this PlayerModel playerModel, PlayerScriptableObject playerScript)
//    {
//        playerScript.Money = playerModel.Money;
//    }
//}