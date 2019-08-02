using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerModel
{
    public long Money;
}

public static class Extensions
{
    public static PlayerModel ToPlayerModel(this PlayerScript playerScript)
    {
        PlayerModel playerModel = new PlayerModel();
        playerModel.Money = playerScript.Money;

        return playerModel;
    }

    public static void ToPlayerScript(this PlayerModel playerModel, PlayerScript playerScript)
    {
        playerScript.Money = playerModel.Money;
    }
}