using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    public class ResourceDatabase
    {
        private static ResourceDatabase instance;

        public Dictionary<string, Resource> resources = new Dictionary<string, Resource>(StringComparer.InvariantCultureIgnoreCase);

        public static ResourceDatabase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ResourceDatabase();
                }

                return instance;
            }
        }

        public ResourceDatabase()
        {
            resources = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Resource>>(System.IO.File.ReadAllText(@"Assets/StreamingAssets/Resources.json"));
        }

        public ResourceDatabase(Resource resource)
        {
            RegisterResource(resource);
            Newtonsoft.Json.JsonConvert.SerializeObject(resources);
        }

        public ResourceDatabase(string json)
        {

        }

        public int id = 0;
        public void RegisterResource(Resource resource)
        {
            if (!resources.TryGetValue(resource.name.ToString(), out Resource existingResource))
            {
                resource.id = id++;
                resources.Add(resource.name, resource);
            }
        }
    }
}
