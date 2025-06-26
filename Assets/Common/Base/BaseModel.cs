using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Sqlite;

namespace Assets.Common.Base
{
    public abstract class BaseModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        protected BaseModel()
        {
        }

        /// <summary>
        /// 获取所有BaseModel的子类
        /// </summary>
        /// <returns></returns>
        public static List<Type> GetAllModelTypes<T>()
        {
            return Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(T)) && !t.IsAbstract).ToList();
        }
    }
}
