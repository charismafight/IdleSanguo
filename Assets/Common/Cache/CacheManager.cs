using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Assets.Common.Base;
using Assets.Common.Reflector;
using Assets.Scripts.Config;
using Assets.Scripts.Config.GameConfigs;
using Assets.Scripts.Sqlite;
using UnityEngine;

namespace Assets.Common.Cache
{
    /// <summary>
    /// 缓存管理器
    /// </summary>
    public class CacheManager
    {
        private static volatile CacheManager _instance;
        private static object _lock = new();

        public static CacheManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new CacheManager();
                        }
                    }
                }
                return _instance;
            }
        }

        protected readonly ILookup<string, BaseConfigModel> ConfigCache;

        private CacheManager()
        {
            var models = new List<BaseConfigModel>();
            //实例化时到数据库中读取所有数据，并形成缓存
            var modelTypes = BaseModel.GetAllModelTypes<BaseConfigModel>().ToList();
            modelTypes.ForEach(m =>
            {
#if DEBUG
                var mList = (ReflectorHelper.InvokeGenericMethod<SqlHelper>(SqlHelper.SqlHelperDev, "GetAllData", null, m) as IList)?.Cast<BaseConfigModel>().ToList();
#else
                var mList = (ReflectorHelper.InvokeGenericMethod<SqlHelper>(SqlHelper.SqlHelperRelease, "GetAllData", null, m) as IList)?.Cast<BaseConfigModel>().ToList();
#endif

                if (mList == null)
                {
                    throw new Exception($"无法正确获取{m.FullName}的数据，请确认缓存加载执行情况");
                }

                models.AddRange(mList);
            });

            ConfigCache = models.ToLookup(m =>
            {
                return m.GetType().Name;
            });

            Debug.Log($"游戏配置{ConfigCache.Count}张表加载进入缓存");
        }

        public List<T> GetAll<T>() where T : BaseConfigModel
        {
            if (ConfigCache.Contains(typeof(T).Name))
            {
                return ConfigCache[typeof(T).Name].Cast<T>().ToList();
            }

            return new List<T>();
        }

        public T Get<T>(Func<T, bool> exp) where T : BaseConfigModel
        {
            return GetAll<T>().SingleOrDefault(exp);
        }

        public List<T> GetList<T>(Func<T, bool> exp) where T : BaseConfigModel
        {
            return GetAll<T>().Where(exp).ToList();
        }
    }
}
