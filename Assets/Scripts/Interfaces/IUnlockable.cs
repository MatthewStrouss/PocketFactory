﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfaces
{
    public interface IUnlockable
    {
        bool IsUnlocked
        {
            get;
            set;
        }

        long UnlockCost
        {
            get;
            set;
        }

        void Unlock();
        void Lock();
    }
}
