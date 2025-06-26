using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Common.Interfaces
{
    interface IConfigCache<K, V>
    {
        /// <summary>
        /// 从缓存中获取Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        V Get(K key);

        /// <summary>
        /// 将对象放入缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Put(K key, V value);

        /// <summary>
        /// 载入所有Models
        /// </summary>
        /// <returns></returns>
        List<V> GetModels();
    }
}
