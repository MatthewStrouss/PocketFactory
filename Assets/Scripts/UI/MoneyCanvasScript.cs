using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyCanvasScript : MonoBehaviour
{
    public Text moneyText;

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
        this.moneyText.text = string.Format("${0}", money);
    }
}
