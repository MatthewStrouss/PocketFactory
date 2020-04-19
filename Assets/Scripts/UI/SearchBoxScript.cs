using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchBoxScript : MonoBehaviour
{
    [SerializeField] private InputField InputField;
    [SerializeField] private Button ClearSearchButton;

    public Action<string> OnSearchUpdated;
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

    public void Activate(Action<string> onSearchUpdated)
    {
        this.OnSearchUpdated = onSearchUpdated;
        this.Activate();
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void InputField_Updated()
    {
        this.OnSearchUpdated?.DynamicInvoke(this.InputField.text);
        this.UpdateUI();
    }

    public void UpdateUI()
    {
        this.ClearSearchButton.gameObject.SetActive(!string.IsNullOrWhiteSpace(this.InputField.text));
    }

    public void ClearButton_Clicked()
    {
        this.InputField.text = string.Empty;
        this.UpdateUI();
    }
}
