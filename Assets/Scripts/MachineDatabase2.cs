using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineDatabase2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<MachineDatabaseEntry> machines;
}

[Serializable]
public struct MachineDatabaseEntry
{
    public string Name;
    public Machine ClassData;
    public GameObject Prefab;
    public MachineScriptableObject MachineData;
}