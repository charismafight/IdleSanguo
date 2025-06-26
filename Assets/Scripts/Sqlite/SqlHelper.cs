using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Assets.Common.Cache;
using Assets.Scripts.Common;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Sqlite
{
    public class SqlHelper
    {
        public static SqlHelper Instance
        {
            get
            {
#if DEBUG
                return SqlHelperDev;
#else
                return SqlHelperRelease;
#endif
            }
        }

        private static volatile SqlHelper _instanceRelease;
        private static volatile SqlHelper _instanceDev;

        private static object _rLock = new();
        private static object _dLock = new();

        public static SqlHelper SqlHelperRelease
        {
            get
            {
                if (_instanceRelease == null)
                {
                    lock (_rLock)
                    {
                        if (_instanceRelease == null)
                        {
                            _instanceRelease = new SqlHelper(IdleConstants.ConfigDecryptedSqliteDbPath);
                        }
                    }
                }
                return _instanceRelease;
            }
        }

        public static SqlHelper SqlHelperDev
        {
            get
            {
                if (_instanceDev == null)
                {
                    lock (_dLock)
                    {
                        if (_instanceDev == null)
                        {
                            _instanceDev = new SqlHelper(IdleConstants.ConfigSqliteSourcePath);
                        }
                    }
                }
                return _instanceDev;
            }
        }

        private SQLiteConnection conn;

        public SqlHelper(string path)
        {
            conn = new SQLiteConnection(path, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        }

        public List<T> GetAllData<T>() where T : new()
        {
            return conn.Table<T>().ToList();
        }

        public T Get<T>(Expression<Func<T, bool>> predicate) where T : new()
        {
            return conn.Find(predicate);
        }

        public void CreateTable<T>()
        {
            conn.CreateTable<T>();
        }

        public void DropTable<T>()
        {
            conn.DropTable<T>();
        }

        public void Dispose()
        {
            conn?.Close();
            conn?.Dispose();
        }

        public static void FreeDbConnection()
        {
            _instanceRelease?.Dispose();
            _instanceDev?.Dispose();
        }
    }
}
