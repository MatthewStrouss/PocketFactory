using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplitterCanvasScriptNewNew : MonoBehaviour
{
    [SerializeField] private MoreLessScript LeftMoreLess;
    [SerializeField] private MoreLessScript UpMoreLess;
    [SerializeField] private MoreLessScript RightMoreLess;
    [SerializeField] private MoreLessScript DownMoreLess;
    [SerializeField] private Image LeftImage;
    [SerializeField] private Image UpImage;
    [SerializeField] private Image RightImage;
    [SerializeField] private Image DownImage;

    private SplitterController SplitterController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Activate()
    {
        this.LeftMoreLess.Activate(
            min: 0,
            current: this.SplitterController.Directions[0].Count,
            max: 99,
            moreAction: this.UpdateCountLeft,
            lessAction: this.UpdateCountLeft
            );

        //this.UpMoreLess.Activate(
        //    min: 1,
        //    current: this.SplitterController.Directions[1].Count,
        //    max: 99,
        //    moreAction: this.UpdateCountUp,
        //    lessAction: this.UpdateCountUp
        //    );

        this.RightMoreLess.Activate(
            min: 0,
            current: this.SplitterController.Directions[2].Count,
            max: 99,
            moreAction: this.UpdateCountRight,
            lessAction: this.UpdateCountRight
            );

        this.DownMoreLess.Activate(
            min: 0,
            current: this.SplitterController.Directions[3].Count,
            max: 99,
            moreAction: this.UpdateCountDown,
            lessAction: this.UpdateCountDown
            );

        this.gameObject.SetActive(true);
        this.UpdateUI();
    }

    public void Activate(SplitterController splitterController)
    {
        this.SplitterController = splitterController;
        this.Activate();
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
        this.LeftImage.gameObject.SetActive(this.SplitterController.Directions[0].Count != 0);
        //this.UpImage.gameObject.SetActive(this.SplitterController.Directions[1].Count != 0);
        this.RightImage.gameObject.SetActive(this.SplitterController.Directions[2].Count != 0);
        this.DownImage.gameObject.SetActive(this.SplitterController.Directions[3].Count != 0);
    }

    public void UpdateCountLeft(int count)
    {
        this.SplitterController.Directions[0].Count = count;
        this.UpdateUI();
    }

    public void UpdateCountUp(int count)
    {
        this.SplitterController.Directions[1].Count = count;
        this.UpdateUI();
    }

    public void UpdateCountRight(int count)
    {
        this.SplitterController.Directions[2].Count = count;
        this.UpdateUI();
    }

    public void UpdateCountDown(int count)
    {
        this.SplitterController.Directions[3].Count = count;
        this.UpdateUI();
    }
}
