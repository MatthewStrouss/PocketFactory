using System;
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
        //RegisterPrefab("Machine", "Selector", Resources.Load(@"Prefabs/Machines/SelectorPrefab", typeof(GameObject)) as GameObject);
        RegisterPrefab("Machine", "Seller", Resources.Load(@"Prefabs/Machines/SellerPrefab", typeof(GameObject)) as GameObject);
        RegisterPrefab("Machine", "Splitter", Resources.Load(@"Prefabs/Machines/SplitterPrefab", typeof(GameObject)) as GameObject);
        RegisterPrefab("Machine", "Starter", Resources.Load(@"Prefabs/Machines/StarterPrefab", typeof(GameObject)) as GameObject);
        //this.GetPrefab("Machine", "Starter").GetComponent<StarterController>().starterGUI = this.GetPrefab("UI", "Starter");
        RegisterPrefab("Machine", "Wire Drawer", Resources.Load(@"Prefabs/Machines/WireDrawerPrefab", typeof(GameObject)) as GameObject);
        //RegisterPrefab("Machine", "Left Splitter", Resources.Load(@"Prefabs/Machines/LeftSplitter", typeof(GameObject)) as GameObject);
        //RegisterPrefab("Machine", "Right Splitter", Resources.Load(@"Prefabs/Machines/RightSplitter", typeof(GameObject)) as GameObject);
        RegisterPrefab("Machine", "Left Selector", Resources.Load(@"Prefabs/Machines/LeftSelector", typeof(GameObject)) as GameObject);
        RegisterPrefab("Machine", "Right Selector", Resources.Load(@"Prefabs/Machines/RightSelector", typeof(GameObject)) as GameObject);
        RegisterPrefab("Machine", "Multi Selector", Resources.Load(@"Prefabs/Machines/MultiSelector", typeof(GameObject)) as GameObject);
    }

    private void SetupMachines()
    {
        this.GetPrefabsForType("Machine").Values.ToList().ForEach(x => x.GetComponent<MachineController>().SetupMachine());
    }

    private void RegisterUI()
    {
        Transform Parent = GameObject.Find("Canvas").transform;

        RegisterPrefab("UI", "Money", Parent.Find("MoneyCanvas").gameObject);
        RegisterPrefab("UI", "Cheat", Parent.Find("CheatCanvas").gameObject);
        RegisterPrefab("UI", "Error", Parent.Find("ErrorCanvas").gameObject);
        RegisterPrefab("UI", "Recipe", Parent.Find("RecipesPanel").gameObject);
        RegisterPrefab("UI", "OkCancelCanvas", Parent.Find("NavCanvas").transform.Find("OkCancelCanvas").gameObject);
        RegisterPrefab("UI", "SelectionActionCanvas", Parent.Find("NavCanvas").transform.Find("SelectionActionCanvas").gameObject);

        RegisterPrefab("UI", "RotationActionCanvas", Parent.Find("NavCanvas").transform.Find("RotationActionCanvas").gameObject);

        RegisterPrefab("UI", "MachineBaseCanvas", Parent.Find("MachineBase").gameObject);
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
