using Assets.Scripts.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Common.Reflector
{
    public static class ReflectorHelper
    {
        public static object InvokeMethod<T>(object instance, string methodName, object[] parameters)
        {
            var methodInfo = typeof(T).GetMethod(methodName);
            if (methodInfo == null)
            {
                throw new Exception($"无法找到{typeof(T).Name}的{methodName}方法，请检查调用是否有误");
            }

            return methodInfo.Invoke(instance, parameters);
        }

        /// <summary>
        /// 反射调用泛型方法
        /// </summary>
        /// <typeparam name="T">方法所在类型</typeparam>
        /// <param name="instance">方法所在实例</param>
        /// <param name="methodName">方法名</param>
        /// <param name="parameters">参数</param>
        /// <param name="genericTypes">指定泛型类型</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static object InvokeGenericMethod<T>(object instance, string methodName, object[] parameters, params Type[] genericTypes)
        {
            var methodInfo = typeof(T).GetMethod(methodName);
            if (methodInfo == null)
            {
                throw new Exception($"无法找到{typeof(T).Name}的{methodName}方法，请检查调用是否有误");
            }

            //构造泛型函数
            var genericMethodInfo = methodInfo.MakeGenericMethod(genericTypes);
            
            return genericMethodInfo.Invoke(instance, parameters);
        }
    }
}
