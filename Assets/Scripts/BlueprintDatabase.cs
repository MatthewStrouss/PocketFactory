using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintDatabase
{
    private static BlueprintDatabase instance;
    public static BlueprintDatabase Instance
    {
        get
        {
            if (instance is null)
            {
                instance = new BlueprintDatabase();
            }

            return instance;
        }
    }

    public Dictionary<string, object> blueprints = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);

    public BlueprintDatabase()
    {
        //blueprints = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(Resources.Load(@"").ToString());
    }
}
