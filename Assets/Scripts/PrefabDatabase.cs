﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PrefabDatabase
{
    private static PrefabDatabase instance;

    public Dictionary<string, Dictionary<string, GameObject>> prefabs = new Dictionary<string, Dictionary<string, GameObject>>(StringComparer.InvariantCultureIgnoreCase);

    public static PrefabDatabase Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PrefabDatabase();
            }

            return instance;
        }
    }

    public PrefabDatabase()
    {
        RegisterPrefab("Resource", "ResourcePrefab", Resources.Load(@"Prefabs/Resources/Resource", typeof(GameObject)) as GameObject);
        RegisterUI();
        RegisterMachines();
        SetupMachines();
    }

    private void RegisterMachines()
    {
        RegisterPrefab("Machine", "Crafter", Resources.Load(@"Prefabs/Machines/CrafterPrefab", typeof(GameObject)) as GameObject);
        RegisterPrefab("Machine", "Cutter", Resources.Load(@"Prefabs/Machines/CutterPrefab", typeof(GameObject)) as GameObject);
        RegisterPrefab("Machine", "Furnace", Resources.Load(@"Prefabs/Machines/FurnacePrefab", typeof(GameObject)) as GameObject);
        RegisterPrefab("Machine", "Hydraulic Press", Resources.Load(@"Prefabs/Machines/HydraulicPressPrefab", typeof(GameObject)) as GameObject);
        RegisterPrefab("Machine", "Roller", Resources.Load(@"Prefabs/Machines/RollerPrefab", typeof(GameObject)) as GameObject);
        RegisterPrefab("Machine", "Selector", Resources.Load(@"Prefabs/Machines/SelectorPrefab", typeof(GameObject)) as GameObject);
        RegisterPrefab("Machine", "Seller", Resources.Load(@"Prefabs/Machines/SellerPrefab", typeof(GameObject)) as GameObject);
        RegisterPrefab("Machine", "Splitter", Resources.Load(@"Prefabs/Machines/SplitterPrefab", typeof(GameObject)) as GameObject);
        RegisterPrefab("Machine", "Starter", Resources.Load(@"Prefabs/Machines/StarterPrefab", typeof(GameObject)) as GameObject);
        //this.GetPrefab("Machine", "Starter").GetComponent<StarterController>().starterGUI = this.GetPrefab("UI", "Starter");
        RegisterPrefab("Machine", "Wire Drawer", Resources.Load(@"Prefabs/Machines/WireDrawerPrefab", typeof(GameObject)) as GameObject);
    }

    private void SetupMachines()
    {
        this.GetPrefabsForType("Machine").Values.ToList().ForEach(x => x.GetComponent<MachineController>().SetupMachine());
    }

    private void RegisterUI()
    {
        RegisterPrefab("UI", "Arrow", Resources.Load(@"Prefabs/UI/ArrowObject", typeof(GameObject)) as GameObject);
        //RegisterPrefab("UI", "Starter", GameObject.Find("Canvas").transform.GetChild(4).gameObject);
        RegisterPrefab("UI", "Starter", GameObject.Find("Canvas").transform.Find("StarterCanvas").gameObject);
        RegisterPrefab("UI", "Splitter", GameObject.Find("Canvas").transform.Find("SplitterCanvas").gameObject);
        RegisterPrefab("UI", "Crafter", GameObject.Find("Canvas").transform.Find("CrafterCanvas").gameObject);
        RegisterPrefab("UI", "Seller", GameObject.Find("Canvas").transform.Find("SellerCanvas").gameObject);


        //RegisterPrefab("UI", "Selection", GameObject.Find("Canvas").transform.Find("MainGUICanvas").transform.Find("SelectionCanvas").gameObject);
        RegisterPrefab("UI", "Copy", GameObject.Find("Canvas").transform.Find("CopyCanvas").gameObject);
        RegisterPrefab("UI", "Paste", GameObject.Find("Canvas").transform.Find("PasteCanvas").gameObject);
        RegisterPrefab("UI", "Money", GameObject.Find("Canvas").transform.Find("MoneyCanvas").gameObject);
        RegisterPrefab("UI", "Cheat", GameObject.Find("Canvas").transform.Find("CheatCanvas").gameObject);
        RegisterPrefab("UI", "Recipe", Resources.Load(@"UI/RecipeCanvas.prefab", typeof(GameObject)) as GameObject);

        RegisterPrefab("UI", "MainGUI", GameObject.Find("MainGUICanvas"));
        RegisterPrefab("UI", "MainUI", this.GetPrefab("UI", "MainGUI").transform.Find("MainUICanvas").gameObject);
        RegisterPrefab("UI", "BuildUI", this.GetPrefab("UI", "MainGUI").transform.Find("BuildUICanvas").gameObject);
        RegisterPrefab("UI", "SelectionUI", this.GetPrefab("UI", "MainGUI").transform.Find("SelectionUICanvas").gameObject);
        RegisterPrefab("UI", "OkCancelUI", this.GetPrefab("UI", "MainGUI").transform.Find("OkCancelUICanvas").gameObject);
    }

    public void RegisterPrefab(string prefabType, string prefabName, GameObject prefab)
    {
        if (prefabs.TryGetValue(prefabType, out Dictionary<string, GameObject> existingPrefabType))
        {
            if (existingPrefabType.TryGetValue(prefabName, out GameObject existingGO))
            {
                Console.WriteLine(string.Format("Machine name {0} already exists in machine dictionary", prefabName));
            }
            else
            {
                existingPrefabType.Add(prefabName, prefab);
            }
        }
        else
        {
            prefabs.Add(prefabType, new Dictionary<string, GameObject>(StringComparer.InvariantCultureIgnoreCase));
            prefabs[prefabType].Add(prefabName, prefab);
        }
    }

    public Dictionary<string, GameObject> GetPrefabsForType(string prefabType)
    {
        prefabs.TryGetValue(prefabType, out Dictionary<string, GameObject> matchingPrefab);
        return matchingPrefab;
    }

    public GameObject GetPrefab(string prefabType, string prefabName)
    {
        GameObject go = null;

        if (prefabs.TryGetValue(prefabType, out Dictionary<string, GameObject> matchingPrefabType))
        {
            matchingPrefabType.TryGetValue(prefabName, out go);
        }

        return go;
    }
}