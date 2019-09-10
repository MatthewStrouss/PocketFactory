using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public static class Player
{
    public static PlayerModel playerModel;

    static Player()
    {
        playerModel = new PlayerModel();
    }
    //public static LongEvent OnMoneyUpdate;

    //[SerializeField] private static long money;
    //public static long Money
    //{
    //    get
    //    {
    //        return money;
    //    }
    //    set
    //    {
    //        money = value;
    //        OnMoneyUpdated(new MoneyEventArgs() { money = money });
    //    }
    //}

    //public static void AddMoney(long moneyToAdd, bool addToAverage = true)
    //{
    //    Money += moneyToAdd;

    //    if (addToAverage)
    //    {

    //    }
    //}

    //public static void SubMoney(long moneyToSub, bool addToAverage = true)
    //{
    //    Money -= moneyToSub;

    //    if (addToAverage)
    //    {

    //    }
    //}

    //public static void SetMoney(long moneyToSet)
    //{
    //    Money = moneyToSet;
    //}

    //public static void OnMoneyUpdated(MoneyEventArgs e)
    //{
    //    EventHandler handler = MoneyUpdated;
    //    handler?.Invoke(null, e);
    //}

    //public static event EventHandler MoneyUpdated;
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