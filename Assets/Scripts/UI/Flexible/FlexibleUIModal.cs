using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Text))]

public class FlexibleUIModal : FlexibleUI
{
    public Text headerText;

    protected override void OnSkinUI()
    {
        base.OnSkinUI();

        headerText = GetComponent<Text>();
    }
}
