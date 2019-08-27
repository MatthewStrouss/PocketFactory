using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class MachineController : MonoBehaviour
{
    public MonoBehaviour controller;

    private Machine machine;
    public Machine Machine
    {
        get
        {
            if (this.machine == null)
            {
                this.SetupMachine();
            }

            return this.machine;
        }
    }

    [SerializeField] private Machine machine2;
    [SerializeField] private MachineScriptableObject machineData;

    public GameObject SelectedGameObject;
    public GameObject ArrowGameObject;

    public string MachineName;

    public float nextActionTime;

    public PlayerScriptableObject playerScriptableObject;
    public GameManagerController gameManager;

    private void Awake()
    {
        this.SelectedGameObject?.SetActive(false);

        if (this.ArrowGameObject != null)
        {
            this.ArrowGameObject.SetActive(false);
        }

        this.SetupMachine();
        this.IsOn = true;
    }

    public void SetupMachine()
    {
        this.machine = MachineDatabase.Instance.GetMachine(this.MachineName);
        //this.machine2 = this.gameManager.machineDatabase.machines.FirstOrDefault(x => x.Key.Equals(this.MachineName, StringComparison.InvariantCultureIgnoreCase)).Value;
    }

    private bool isOn;
    public bool IsOn
    {
        get => this.isOn;
        set
        {
            if (value)
            {
                this.isOn = true;

                if (controller != null && this.Machine != null)
                {
                    this.nextActionTime = Time.time + this.Machine.ActionTime;
                    //controller.InvokeRepeating("ActionToPerformOnTimer", 0.0f, this.Machine.ActionTime);
                }
            }
            else
            {
                if (controller != null && this.Machine != null)
                {
                    //controller.CancelInvoke("ActionToPerformOnTimer");
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //if (this.IsOn)
        //{
        //    controller.InvokeRepeating("ActionToPerformOnTimer", 0.0f, this.Machine.ActionTime);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManagerController.Instance.IsPaused)
        {
            if (this.nextActionTime > 0)
            {
                float currentTime = Time.time;
                if (currentTime >= this.nextActionTime)
                {
                    this.nextActionTime = currentTime + this.Machine.ActionTime;

                    (this.controller as IMachineController).ActionToPerformOnTimer();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ResourceController>() != null)
        {
            (controller as IMachineController).OnCollision(collision);
        }
    }

    public void OnClick()
    {
        (controller as IMachineController).OnClick();
    }

    public void ActivateSelected()
    {
        this.SelectedGameObject.SetActive(true);
    }

    public void DeactivateSelected()
    {
        this.SelectedGameObject.SetActive(false);
    }

    public void ToggleArrow()
    {
        if (this.ArrowGameObject != null)
        {
            this.ArrowGameObject.SetActive(!this.ArrowGameObject.activeSelf);
        }
    }

    public void ActivateArrow()
    {
        this.ArrowGameObject.SetActive(true);
    }

    public void DeactivateArrow()
    {
        this.ArrowGameObject.SetActive(false);
    }

    public void Sell(bool sellFullAmount = false)
    {
        long moneyToAdd = Mathf.RoundToInt((sellFullAmount ? this.Machine.BuildCost : 0.9f * this.Machine.BuildCost));

        this.playerScriptableObject.AddMoney(moneyToAdd, false);
        Destroy(this.gameObject);
    }

    public void Unlock()
    {
        if (this.playerScriptableObject.Money - this.Machine.UnlockCost >= 0)
        {
            this.playerScriptableObject.SubMoney(Convert.ToInt64(this.Machine.UnlockCost), false);
            MachineDatabase.Instance.GetMachine(this.MachineName).IsUnlocked = true;
        }
    }

    public GameObject Place(Vector3 position, Quaternion rotation)
    {
        GameObject gameObjectToReturn = null;

        if (this.playerScriptableObject.Money - this.Machine.BuildCost >= 0)
        {
            this.playerScriptableObject.SubMoney(Convert.ToInt64(this.Machine.BuildCost), false);

            gameObjectToReturn = Instantiate(this.gameObject, position, rotation);
            gameObjectToReturn.GetComponent<MachineController>().SetupMachine();
            gameObjectToReturn.GetComponent<BoxCollider2D>().enabled = true;
        }

        return gameObjectToReturn;
    }

    public GameObject Place(Vector3 position, Quaternion rotation, MonoBehaviour otherController)
    {
        GameObject gameObjectToReturn = null;

        if (this.playerScriptableObject.Money - this.Machine.BuildCost >= 0)
        {
            this.playerScriptableObject.SubMoney(Convert.ToInt64(this.Machine.BuildCost), false);

            gameObjectToReturn = Instantiate(this.gameObject, position, rotation);
            gameObjectToReturn.GetComponent<MachineController>().SetupMachine();
            gameObjectToReturn.GetComponent<BoxCollider2D>().enabled = true;
            gameObjectToReturn.GetComponent<MachineController>().SetControllerValues(otherController);
        }

        return gameObjectToReturn;
    }

    public void SetControllerValues(MonoBehaviour other)
    {
        (this.controller as IMachineController).SetControllerValues(other as IMachineController);
    }

    public void SubtractElectricityCost()
    {
        this.playerScriptableObject.SubMoney(Convert.ToInt64(this.Machine.ElectricityCost));
    }
}
