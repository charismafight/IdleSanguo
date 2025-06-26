using Assets.Common.GameConfigurations;
using Assets.Scripts.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Common;
using Assets.Scripts.Sqlite;
using UnityEngine;

namespace Assets.Scripts.Config
{
    /// <summary>
    /// 关于配置的说明
    /// 游戏配置表使用sqlite作为数据库存储
    /// 在开发时，使用的是游戏目录中$"{Application.streamingAssetsPath}/config.db"的路径
    /// 数据结构的维护通过对model修改后，在editor工具中生成加密配置文件$"{Application.streamingAssetsPath}/config.enc"
    /// 生成后的加密文件则是提供给游戏启动时记载的运行数据
    /// 
    /// 在游戏运行时，系统解密配置文件，然后在temp目录中生成，游戏读取配置文件后把数据放入内存并删除temp文件
    /// </summary>
    public static class ConfigHelper
    {
        //public static void LoadConfig(bool forceLoad = false)
        //{
        //    if (!forceLoad && ConfigLoaded)
        //    {
        //        Debug.Log($"配置表已加载，默认不重复加载");
        //        return;
        //    }

        //    if (!FileHelper.CheckFile(IdleConstants.ConfigEncryptedSqliteDbPath))
        //    {
        //        Debug.LogError($"未能找到{IdleConstants.ConfigEncryptedSqliteDbPath},无法加载配置表文件");
        //        return;
        //    }

        //    try
        //    {
        //        //解密配置，并构造数据库连接
        //        Encrypter.DecryptFile(IdleConstants.ConfigEncryptedSqliteDbPath, IdleConstants.ConfigDecryptedSqliteDbPath);
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.LogError($"配置文件加载异常，{e}");
        //    }
        //}
    }
}
