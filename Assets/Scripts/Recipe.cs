using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    public class Recipe
    {
        public Recipe()
        { }

        [JsonConstructor]
        public Recipe(string name, Resource result, string type, bool isUnlocked, float unlockCost, List<Resource> requirements)
        {
            this.name = name;
            this.result = result;
            this.type = type;
            this.isUnlocked = isUnlocked;
            this.unlockCost = unlockCost;

            if (requirements != null)
            {
                this.requirements.AddRange(requirements);
            }
        }

        private bool isUnlocked;

        private float unlockCost;

        private Resource result;

        private List<Resource> requirements = new List<Resource>();

        public bool IsUnlocked
        {
            get
            {
                return isUnlocked;
            }
            set
            {
                isUnlocked = value;
            }
        }

        public float UnlockCost
        {
            get
            {
                return unlockCost;
            }
            set
            {
                unlockCost = value;
            }
        }

        public Resource Result
        {
            get
            {
                return result;
            }
            set
            {
                result = value;
            }
        }

        public List<Resource> Requirements
        {
            get
            {
                return requirements;
            }
            set
            {
                requirements = value;
            }
        }

        private string type;
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
            }
        }
    }
}
