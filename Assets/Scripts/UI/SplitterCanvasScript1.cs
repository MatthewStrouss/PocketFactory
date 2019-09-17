using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplitterCanvasScript1 : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private Canvas XButton;

    [SerializeField] private Canvas leftCanvas;
    [SerializeField] private Canvas upCanvas;
    [SerializeField] private Canvas rightCanvas;
    [SerializeField] private Canvas downCanvas;

    [SerializeField] private InputField leftText;
    [SerializeField] private InputField upText;
    [SerializeField] private InputField rightText;
    [SerializeField] private InputField downText;

    private GameObject splitterGameObject;
    private SplitterController splitterController;

    private long leftIndex;
    private long upIndex;
    private long rightIndex;
    private long downIndex;

    private Action callback;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
        this.UpdateUI();
        this.XButton.GetComponent<CancelCanvasScript>().Activate(this.gameObject);
    }

    public void Activate(GameObject splitterGameObject, Action callback)
    {
        this.callback = callback;

        this.splitterGameObject = splitterGameObject;
        this.splitterController = this.splitterGameObject.GetComponent<SplitterController>();

        this.mainPanel.transform.rotation = Quaternion.Euler(this.mainPanel.transform.rotation.eulerAngles.x, this.mainPanel.transform.rotation.eulerAngles.y, this.splitterGameObject.transform.rotation.eulerAngles.z);

        this.leftText.transform.rotation = Quaternion.Euler(this.leftText.transform.rotation.eulerAngles.x, this.leftText.transform.rotation.eulerAngles.y, 0);
        this.leftCanvas.gameObject.SetActive(true);
        this.leftIndex = 0;

        this.upText.transform.rotation = Quaternion.Euler(this.upText.transform.rotation.eulerAngles.x, this.upText.transform.rotation.eulerAngles.y, 0);
        this.upCanvas.gameObject.SetActive(true);
        this.upIndex = 1;

        this.rightText.transform.rotation = Quaternion.Euler(this.rightText.transform.rotation.eulerAngles.x, this.rightText.transform.rotation.eulerAngles.y, 0);
        this.rightCanvas.gameObject.SetActive(true);
        this.rightIndex = 2;

        this.downText.transform.rotation = Quaternion.Euler(this.downText.transform.rotation.eulerAngles.x, this.downText.transform.rotation.eulerAngles.y, 0);
        this.downCanvas.gameObject.SetActive(true);
        this.downIndex = 3;

        this.Activate();
    }

    public void Deactivate()
    {
        this.XButton.GetComponent<CancelCanvasScript>().Deactivate();
        this.callback?.DynamicInvoke();
    }

    public void UpdateUI()
    {
        this.leftText.text = this.splitterController.Directions[this.leftIndex].Count.ToString();
        this.upText.text = this.splitterController.Directions[this.upIndex].Count.ToString();
        this.rightText.text = this.splitterController.Directions[this.rightIndex].Count.ToString();
        this.downText.text = this.splitterController.Directions[this.downIndex].Count.ToString();
    }

    public void LeftPlusButton_Clicked()
    {
        if (this.splitterController.Directions[this.leftIndex].Count + 1 >= 0)
        {
            this.splitterController.Directions[this.leftIndex].Count++;
        }

        this.leftText.text = this.splitterController.Directions[this.leftIndex].Count.ToString();
    }

    public void LeftMinusButton_Clicked()
    {
        //if (this.splitterController.Directions[this.leftIndex].Count - 1 >= 0)
        //{
        //    this.splitterController.Directions[this.leftIndex].Count--;
        //}

        this.splitterController.Directions[this.leftIndex].Count--;
        this.leftText.text = this.splitterController.Directions[this.leftIndex].Count.ToString();
    }

    public void UpPlusButton_Clicked()
    {
        if (this.splitterController.Directions[this.upIndex].Count + 1 >= 0)
        {
            this.splitterController.Directions[this.upIndex].Count++;
        }

        this.upText.text = this.splitterController.Directions[this.upIndex].Count.ToString();
    }

    public void UpMinusButton_Clicked()
    {
        if (this.splitterController.Directions[this.upIndex].Count - 1 >= 0)
        {
            this.splitterController.Directions[this.upIndex].Count--;
        }

        this.upText.text = this.splitterController.Directions[this.upIndex].Count.ToString();
    }

    public void RightPlusButton_Clicked()
    {
        if (this.splitterController.Directions[this.rightIndex].Count + 1 >= 0)
        {
            this.splitterController.Directions[this.rightIndex].Count++;
        }

        this.rightText.text = this.splitterController.Directions[this.rightIndex].Count.ToString();
    }

    public void RightMinusButton_Clicked()
    {
        if (this.splitterController.Directions[this.rightIndex].Count - 1 >= 0)
        {
            this.splitterController.Directions[this.rightIndex].Count--;
        }

        this.rightText.text = this.splitterController.Directions[this.rightIndex].Count.ToString();
    }

    public void DownPlusButton_Clicked()
    {
        if (this.splitterController.Directions[this.downIndex].Count + 1 >= 0)
        {
            this.splitterController.Directions[this.downIndex].Count++;
        }

        this.downText.text = this.splitterController.Directions[this.downIndex].Count.ToString();
    }

    public void DownMinusButton_Clicked()
    {
        if (this.splitterController.Directions[this.downIndex].Count - 1 >= 0)
        {
            this.splitterController.Directions[this.downIndex].Count--;
        }

        this.downText.text = this.splitterController.Directions[this.downIndex].Count.ToString();
    }

    public void LeftText_Changed()
    {
        this.splitterController.Directions[this.leftIndex].Count = Convert.ToInt32(this.leftText.text);
    }

    public void UpText_Changed()
    {
        this.splitterController.Directions[this.upIndex].Count = Convert.ToInt32(this.upText.text);
    }

    public void RightText_Changed()
    {
        this.splitterController.Directions[this.rightIndex].Count = Convert.ToInt32(this.rightText.text);
    }

    public void DownText_Changed()
    {
        this.splitterController.Directions[this.downIndex].Count = Convert.ToInt32(this.downText.text);
    }
}
