using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMachineButtonScript : MonoBehaviour
{
    public Machine machine;
    [SerializeField] private Text nameText;
    [SerializeField] private Text costText;
    [SerializeField] private Image image;

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
    }

    public void Activate(Machine machine)
    {
        this.machine = machine;
        this.Activate();
    }

    public void UpdateUI()
    {
        this.image.sprite = this.machine.Sprite;
        this.nameText.text = this.machine.MachineName;
        this.costText.text = this.machine.BuildCost.ToString();
    }
}
