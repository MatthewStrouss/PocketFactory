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
        //this.moneyText.text = this.GetMoneyString(money);
        this.moneyText.text = this.FormatNumber(money);
    }

    public void MoneyUpdated(object sender, EventArgs e)
    {
        this.UpdateUI((e as MoneyEventArgs).money);
    }

    private string FormatNumber(long num)
    {
        // Ensure number has max 3 significant digits (no rounding up can happen)
        if (num > 0)
        {
            long i = (long)Math.Pow(10, (int)Math.Max(0, Math.Log10(num) - 2));
            num = num / i * i;


            if (num >= 1000000000000000000)
                return (num / 1000000000000000000D).ToString("0.##") + "Qi";
            if (num >= 1000000000000000)
                return (num / 1000000000000000D).ToString("0.##") + "Qa";
            if (num >= 1000000000000)
                return (num / 1000000000000D).ToString("0.##") + "T";
            if (num >= 1000000000)
                return (num / 1000000000D).ToString("0.##") + "B";
            if (num >= 1000000)
                return (num / 1000000D).ToString("0.##") + "M";
            if (num >= 1000)
                return (num / 1000D).ToString("0.##") + "K";
        }

        return num.ToString("#,0");
    }
}
