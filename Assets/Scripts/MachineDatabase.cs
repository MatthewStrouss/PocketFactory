using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineDatabase
{ 
    private static MachineDatabase instance;
    public static MachineDatabase Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MachineDatabase();
            }

            return instance;
        }
    }

    public Dictionary<string, Machine> machines = new Dictionary<string, Machine>(StringComparer.InvariantCultureIgnoreCase);

    public MachineDatabase()
    {
        machines = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Machine>>(Resources.Load(@"Data/Machines").ToString());
    }

    public void RegisterMachine(string machineName, Machine machine)
    {
        if (machines.TryGetValue(machineName, out Machine existingMachine))
        {
            throw new Exception(string.Format("Machine already exists in MachineDatabase: {0}", machineName));
        }
        else
        {
            machines.Add(machineName, machine);
        }
    }

    public Machine GetMachine(string machineName)
    {
        Machine machineToReturn = null;

        machines.TryGetValue(machineName, out machineToReturn);

        return machineToReturn;
    }

    public void OverwriteValues(Dictionary<string, Machine> newMachineData)
    {
        foreach (string key in newMachineData.Keys)
        {
            if (machines.TryGetValue(key, out Machine existingMachine))
            {
                existingMachine = new Machine(newMachineData[key]);
            }
            else
            {
                machines.Add(key, newMachineData[key]);
            }
        }
    }
}