using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Exceptions
{
    public class NullComponentException : Exception
    {
        public NullComponentException(string ComponentName):base($"组件{ComponentName}为空，请检查该组件是否初始化成功")
        {
        }
    }
}
