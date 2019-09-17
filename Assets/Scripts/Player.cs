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
}