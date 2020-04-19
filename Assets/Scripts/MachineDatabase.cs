using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MachineDatabase
{
    public static Dictionary<string, Machine> database = new Dictionary<string, Machine>(StringComparer.InvariantCultureIgnoreCase);

    static MachineDatabase()
    {
        foreach (KeyValuePair<string, ScriptableObject> machine in (Resources.Load(@"ScriptableObjects/MachineDatabase", typeof(ScriptableObjectDatabase)) as ScriptableObjectDatabase).database)
        {
            database.Add(machine.Key, new Machine(machine.Value as MachineScriptableObject));
        }
    }
}