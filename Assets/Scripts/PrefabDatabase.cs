using System;
using System.Collections;
using System.Collections.Generic;
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
        RegisterPrefab("Resource", "ResourcePrefab", AssetDatabase.LoadAssetAtPath(@"Assets/Prefabs/Resources/Resource.prefab", typeof(GameObject)) as GameObject);
        RegisterUI();
        RegisterMachines();
    }

    private void RegisterMachines()
    {
        RegisterPrefab("Machine", "Crafter", AssetDatabase.LoadAssetAtPath(@"Assets/Prefabs/Machines/CrafterPrefab.prefab", typeof(GameObject)) as GameObject);
        RegisterPrefab("Machine", "Cutter", AssetDatabase.LoadAssetAtPath(@"Assets/Prefabs/Machines/CutterPrefab.prefab", typeof(GameObject)) as GameObject);
        RegisterPrefab("Machine", "Furnace", AssetDatabase.LoadAssetAtPath(@"Assets/Prefabs/Machines/FurnacePrefab.prefab", typeof(GameObject)) as GameObject);
        RegisterPrefab("Machine", "Hydraulic Press", AssetDatabase.LoadAssetAtPath(@"Assets/Prefabs/Machines/HydraulicPressPrefab.prefab", typeof(GameObject)) as GameObject);
        RegisterPrefab("Machine", "Roller", AssetDatabase.LoadAssetAtPath(@"Assets/Prefabs/Machines/RollerPrefab.prefab", typeof(GameObject)) as GameObject);
        RegisterPrefab("Machine", "Selector", AssetDatabase.LoadAssetAtPath(@"Assets/Prefabs/Machines/SelectorPrefab.prefab", typeof(GameObject)) as GameObject);
        RegisterPrefab("Machine", "Seller", AssetDatabase.LoadAssetAtPath(@"Assets/Prefabs/Machines/SellerPrefab.prefab", typeof(GameObject)) as GameObject);
        RegisterPrefab("Machine", "Splitter", AssetDatabase.LoadAssetAtPath(@"Assets/Prefabs/Machines/SplitterPrefab.prefab", typeof(GameObject)) as GameObject);
        RegisterPrefab("Machine", "Starter", AssetDatabase.LoadAssetAtPath(@"Assets/Prefabs/Machines/StarterPrefab.prefab", typeof(GameObject)) as GameObject);
        this.GetPrefab("Machine", "Starter").GetComponent<StarterController>().starterGUI = this.GetPrefab("UI", "Starter");
        RegisterPrefab("Machine", "Wire Drawer", AssetDatabase.LoadAssetAtPath(@"Assets/Prefabs/Machines/WireDrawerPrefab.prefab", typeof(GameObject)) as GameObject);
    }

    private void RegisterUI()
    {
        RegisterPrefab("UI", "Arrow", AssetDatabase.LoadAssetAtPath(@"Assets/Prefabs/UI/ArrowObject.prefab", typeof(GameObject)) as GameObject);
        RegisterPrefab("UI", "Starter", GameObject.Find("Canvas").transform.GetChild(4).gameObject);
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
