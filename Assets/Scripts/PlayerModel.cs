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
    public static PlayerModel ToPlayerModel(this PlayerScriptableObject playerScript)
    {
        PlayerModel playerModel = new PlayerModel();
        playerModel.Money = playerScript.Money;

        return playerModel;
    }

    public static void ToPlayerScript(this PlayerModel playerModel, PlayerScriptableObject playerScript)
    {
        playerScript.Money = playerModel.Money;
    }
}