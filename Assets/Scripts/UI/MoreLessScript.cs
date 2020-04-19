using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoreLessScript : MonoBehaviour
{
    [SerializeField] private Button LessButton;
    [SerializeField] private InputField InputField;
    [SerializeField] private Button MoreButton;

    private Action<int> MoreAction;
    private Action<int> LessAction;
    private int Min;
    private int Current;
    private int Max;

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
        this.gameObject.SetActive(true);
        this.UpdateUI();
    }

    public void Activate(int min, int current, int max, Action<int> moreAction, Action<int> lessAction)
    {
        this.Min = min;
        this.Current = current;
        this.Max = max;
        this.MoreAction = moreAction;
        this.LessAction = lessAction;
        this.Activate();
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
        this.LessButton.interactable = this.Current != this.Min;
        this.MoreButton.interactable = this.Current != this.Max;
        this.InputField.text = this.Current.ToString();
    }

    public void LessButton_Clicked()
    {
        this.Current--;
        this.Current = Mathf.Clamp(this.Current, this.Min, this.Max);
        this.LessAction?.DynamicInvoke(this.Current);
        this.UpdateUI();
    }

    public void MoreButton_Clicked()
    {
        this.Current++;
        this.Current = Mathf.Clamp(this.Current, this.Min, this.Max);
        this.MoreAction?.DynamicInvoke(this.Current);
        this.UpdateUI();
    }

    public void InputField_EndEdit()
    {
        int oldCurrent = this.Current;
        this.Current = Convert.ToInt32(this.InputField.text);
        this.Current = Mathf.Clamp(this.Current, this.Min, this.Max);

        if (oldCurrent < this.Current)
        {
            this.MoreAction?.DynamicInvoke(this.Current);
        }
        else
        {
            this.LessAction?.DynamicInvoke(this.Current);
        }

        this.UpdateUI();
    }

    public void Rotate(Quaternion rotation)
    {
        this.InputField.transform.rotation = rotation;
    }
}
