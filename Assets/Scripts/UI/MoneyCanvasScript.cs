using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyCanvasScript : MonoBehaviour
{
    public Text moneyText;

    private void Awake()
    {
        //PlayerModel.OnMoneyUpdate.RegisterListener(this);
        Player.playerModel.MoneyUpdated += this.MoneyUpdated;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI(long money)
    {
        this.moneyText.text = string.Format("{0:n0}", money);
    }

    public void MoneyUpdated(object sender, EventArgs e)
    {
        this.UpdateUI((e as MoneyEventArgs).money);
    }
}
