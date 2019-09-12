using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public static class ResourceDatabase
    {
        public static Dictionary<string, Resource> database = new Dictionary<string, Resource>(StringComparer.InvariantCultureIgnoreCase);

        static ResourceDatabase()
        {
            //database = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Resource>>(Resources.Load(@"Data/Resources").ToString());
            foreach (KeyValuePair<string, ScriptableObject> resource in (Resources.Load(@"ScriptableObjects/ResourceDatabase", typeof(ScriptableObjectDatabase)) as ScriptableObjectDatabase).database)
            {
                database.Add(resource.Key, new Resource(resource.Value as ResourceScriptableObject));
            }
        }

        //public ResourceDatabase(Resource resource)
        //{
        //    RegisterResource(resource);
        //    Newtonsoft.Json.JsonConvert.SerializeObject(database);
        //}

        //public ResourceDatabase(string json)
        //{

        //}

        //public int id = 0;
        //public void RegisterResource(Resource resource)
        //{
        //    if (!database.TryGetValue(resource.name.ToString(), out Resource existingResource))
        //    {
        //        resource.id = id++;
        //        database.Add(resource.name, resource);
        //    }
        //}

        public static Resource GetResource(string resourceName)
        {
            database.TryGetValue(resourceName, out Resource resource);

            return resource;
        }
    }
}
