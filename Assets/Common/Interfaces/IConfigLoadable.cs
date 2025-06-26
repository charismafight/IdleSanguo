using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Common.Interfaces
{
    interface IConfigLoadable
    {
        void Load();

        void SetConfig(IConfig config);

        IConfig GetConfig();
    }
}
