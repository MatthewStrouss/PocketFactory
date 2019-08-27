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

    public List<MachineKeyValuePair> machines;
}

[Serializable]
public struct MachineKeyValuePair
{
    public string Key;
    public Machine Value;
}