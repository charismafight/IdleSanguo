﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Common.Interfaces
{
    public interface IConfig
    {
        List<V> GetModels<V>();
    }
}
