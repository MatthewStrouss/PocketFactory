using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IResearch
    {
        bool IsUnlocked
        {
            get;
            set;
        }

        [JsonIgnore]
        long UnlockCost
        {
            get;
            set;
        }

        [JsonIgnore]
        string Description
        {
            get;
            set;
        }

        [JsonIgnore]
        string Name
        {
            get;
            set;
        }

        [JsonIgnore]
        Sprite Sprite
        {
            get;
            set;
        }

        void Unlock();
    }
}
