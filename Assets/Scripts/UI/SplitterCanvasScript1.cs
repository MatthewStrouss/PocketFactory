using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplitterCanvasScript1 : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private Canvas leftCanvas;
    [SerializeField] private Canvas forwardCanvas;
    [SerializeField] private Canvas rightanvas;
    [SerializeField] private Canvas XButton;
    [SerializeField] private InputField leftText;
    [SerializeField] private InputField forwardText;
    [SerializeField] private InputField rightText;
    private GameObject splitterGameObject;
    private SplitterController splitterController;

    private long leftIndex;
    private long forwardIndex;
    private long rightIndex;

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

    public void Activate(GameObject splitterGameObject)
    {
        this.splitterGameObject = splitterGameObject;
        this.splitterController = this.splitterGameObject.GetComponent<SplitterController>();

        Debug.Log(string.Format("Rotating panel {0}", this.splitterGameObject.transform.rotation.eulerAngles.z));
        this.mainPanel.transform.rotation = Quaternion.Euler(this.mainPanel.transform.rotation.eulerAngles.x, this.mainPanel.transform.rotation.eulerAngles.y, this.splitterGameObject.transform.rotation.eulerAngles.z);
        Debug.Log(string.Format("Changing leftText rotation from {0} to {1}", this.leftText.transform.rotation.eulerAngles.z, - 1*this.splitterGameObject.transform.rotation.eulerAngles.z));
        this.leftText.transform.rotation = Quaternion.Euler(this.leftText.transform.rotation.eulerAngles.x, this.leftText.transform.rotation.eulerAngles.y, 0);
        this.forwardText.transform.rotation = Quaternion.Euler(this.forwardText.transform.rotation.eulerAngles.x, this.forwardText.transform.rotation.eulerAngles.y, 0);
        this.rightText.transform.rotation = Quaternion.Euler(this.rightText.transform.rotation.eulerAngles.x, this.rightText.transform.rotation.eulerAngles.y, 0);

        if (this.splitterController.splitterDirectionEnum == SplitterDirectionEnum.LEFT)
        {
            this.leftCanvas.gameObject.SetActive(true);
            this.leftIndex = 0;
            this.rightanvas.gameObject.SetActive(false);
            this.rightIndex = 0;
        }
        else if (this.splitterController.splitterDirectionEnum == SplitterDirectionEnum.RIGHT)
        {
            this.leftCanvas.gameObject.SetActive(false);
            this.leftIndex = 0;
            this.rightanvas.gameObject.SetActive(true);
            this.rightIndex = 0;
        }
        else if (this.splitterController.splitterDirectionEnum == SplitterDirectionEnum.MULTI)
        {
            this.leftCanvas.gameObject.SetActive(true);
            this.leftIndex = 0;
            this.rightanvas.gameObject.SetActive(true);
            this.rightIndex = 2;
        }

        this.forwardCanvas.gameObject.SetActive(true);
        this.forwardIndex = 1;

        this.Activate();
    }

    public void Deactivate()
    {
        this.XButton.GetComponent<CancelCanvasScript>().Deactivate();
    }

    public void UpdateUI()
    {
        this.leftText.text = this.splitterController.Directions[this.leftIndex].Count.ToString();
        this.forwardText.text = this.splitterController.Directions[this.forwardIndex].Count.ToString();
        this.rightText.text = this.splitterController.Directions[this.rightIndex].Count.ToString();
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

    public void ForwardPlusButton_Clicked()
    {
        if (this.splitterController.Directions[this.forwardIndex].Count + 1 >= 0)
        {
            this.splitterController.Directions[this.forwardIndex].Count++;
        }

        this.forwardText.text = this.splitterController.Directions[this.forwardIndex].Count.ToString();
    }

    public void ForwardMinusButton_Clicked()
    {
        if (this.splitterController.Directions[this.forwardIndex].Count - 1 >= 0)
        {
            this.splitterController.Directions[this.forwardIndex].Count--;
        }

        this.forwardText.text = this.splitterController.Directions[this.forwardIndex].Count.ToString();
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

    public void LeftText_Changed()
    {
        this.splitterController.Directions[this.leftIndex].Count = Convert.ToInt32(this.leftText.text);
    }

    public void RightText_Changed()
    {
        this.splitterController.Directions[this.rightIndex].Count = Convert.ToInt32(this.rightText.text);
    }

    public void ForwardText_Changed()
    {
        this.splitterController.Directions[this.forwardIndex].Count = Convert.ToInt32(this.forwardText.text);
    }
}
