using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplitterCanvasScript : MonoBehaviour
{
    public SplitterController splitter;
    public InputField leftText;
    public InputField upText;
    public InputField rightText;
    public InputField downText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void XButton_Click()
    {
        this.Deactivate();
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        this.splitter = null;
        this.gameObject.SetActive(false);
    }

    public void UpdateUI(SplitterController splitter)
    {
        this.splitter = splitter;
        SplitterController sc = this.splitter.GetComponent<SplitterController>();

        this.leftText.text = sc.Directions[0].Count.ToString();
        this.upText.text = sc.Directions[1].Count.ToString();
        this.rightText.text = sc.Directions[2].Count.ToString();
        this.downText.text = sc.Directions[3].Count.ToString();
    }

    public void LeftText_Changed()
    {
        this.splitter.GetComponent<SplitterController>().Directions[0].Count = Convert.ToInt32(this.leftText.text);
    }

    public void UpText_Changed()
    {
        this.splitter.GetComponent<SplitterController>().Directions[1].Count = Convert.ToInt32(this.upText.text);
    }

    public void RightText_Changed()
    {
        this.splitter.GetComponent<SplitterController>().Directions[2].Count = Convert.ToInt32(this.rightText.text);
    }

    public void DownText_Changed()
    {
        this.splitter.GetComponent<SplitterController>().Directions[3].Count = Convert.ToInt32(this.downText.text);
    }
}
