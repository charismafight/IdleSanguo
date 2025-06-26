using System.IO;
using Assets.Common.Base;
using Assets.Common.Cache;
using Assets.Common.GameConfigurations;
using Assets.Common.Reflector;
using Assets.Scripts.Characters;
using Assets.Scripts.Common;
using Assets.Scripts.Config;
using Assets.Scripts.Config.GameConfigs;
using Assets.Scripts.Sqlite;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    public class TestWindow : EditorWindow
    {
        public TestWindow()
        {
            titleContent = new GUIContent("工具");
        }

        [MenuItem("Tools/IdleSanguo")]
        static void CreateTestWindow()
        {
            GetWindow(typeof(TestWindow));
        }

        void OnEnable() { }

        void OnGUI()
        {
            GUILayout.BeginHorizontal();
            EditorHelper.CreateCommonEditorButton(
                "更新游戏配置表结构",
                "新增/修改model之后使用，刷新db结构",
                140f,
                30f,
                () =>
                {
                    //根据model动态创建游戏配置表
                    BaseModel
                        .GetAllModelTypes<BaseConfigModel>()
                        .ForEach(m =>
                        {
                            ReflectorHelper.InvokeGenericMethod<SqlHelper>(
                                SqlHelper.SqlHelperDev,
                                "CreateTable",
                                null,
                                m
                            );
                        });
                    Debug.Log("更新游戏配置表结构成功");
                }
            );

            if (
                GUILayout.Button(
                    new GUIContent("重置数据库连接"),
                    GUI.skin.button,
                    GUILayout.Width(140),
                    GUILayout.Height(30)
                )
            )
            {
                Debug.Log("数据库连接已重置");
            }

            if (
                GUILayout.Button(
                    new GUIContent("游戏初始化"),
                    GUI.skin.button,
                    GUILayout.Width(140),
                    GUILayout.Height(30)
                )
            )
            {
                Debug.Log("游戏初始化");
            }

            EditorHelper.CreateCommonEditorButton(
                "加载游戏配置表",
                140f,
                30f,
                () =>
                {
                    Debug.Log("加载完毕");
                }
            );

            EditorHelper.CreateCommonEditorButton(
                "对配置表加密并生成文件",
                "发布前使用，构造加密的配置表config.enc",
                140f,
                30f,
                () =>
                {
                    //先删除加密sqlite
                    if (File.Exists(IdleConstants.ConfigEncryptedSqliteDbPath))
                    {
                        File.Delete(IdleConstants.ConfigEncryptedSqliteDbPath);
                    }

                    Encrypter.EncryptFile(
                        IdleConstants.ConfigSqliteSourcePath,
                        IdleConstants.ConfigEncryptedSqliteDbPath
                    );
                    Debug.Log("配置表加密完毕");

                    //更新temp目录文件
                    if (File.Exists(IdleConstants.ConfigDecryptedSqliteDbPath))
                    {
                        File.Delete(IdleConstants.ConfigDecryptedSqliteDbPath);
                        Debug.Log($"{IdleConstants.ConfigDecryptedSqliteDbPath}已删除");
                    }

                    //加密后在temp目录生成程序运行文件
                    Encrypter.DecryptFile(
                        IdleConstants.ConfigEncryptedSqliteDbPath,
                        IdleConstants.ConfigDecryptedSqliteDbPath
                    );
                    Debug.Log($"解密用于调试的db文件：{IdleConstants.ConfigDecryptedSqliteDbPath}生成完毕");
                }
            );

            EditorHelper.CreateDefaultEditorButton(
                "加载缓存",
                "单元测试",
                () =>
                {
                    CacheManager.Instance.GetAll<CharacterConfig>();
                }
            );

            EditorHelper.CreateDefaultEditorButton(
                "断开数据库连接",
                "单元测试",
                () =>
                {
                    SqlHelper.FreeDbConnection();
                }
            );

            GUILayout.EndHorizontal();
        }
    }
}
