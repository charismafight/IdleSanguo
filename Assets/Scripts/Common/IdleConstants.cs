using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public static class IdleConstants
    {
        public static string ConfigSqliteSourcePath = $"{Application.streamingAssetsPath}/config.db";
        public static string ConfigEncryptedSqliteDbPath = $"{Application.streamingAssetsPath}/config.enc";
        public static string ConfigDecryptedSqliteDbPath = $"{Application.temporaryCachePath}/config.db";

        public static string DataEncryptedSqliteDbPath = $"{Application.streamingAssetsPath}/data.enc";
        public static string DataDecryptedSqliteDbPath = $"{Application.temporaryCachePath}/data.db";

        //profile为头像存储路径
        public static string ProfileSpritesFolder = "Sprites/Profiles/";

        public static string CharacterSpriteFolder = "Sprites/Characters/";

        //动画目录
        public static string ProtagonistCharacterAnimatorFolder = "Animations/Battlefield/";
        public static string EnemyCharacterAnimatorFolder = "Animations/Battlefield/";

        //角色sprite规则
        //Protagonist接主角色，目录内为主角相关sprite
        //Enemy目录为敌人
        //两个目录中再分具体角色拼音目录
        //拼音为配置表中的code
        public static string ProtagonistCharacterSpritesPath = "Sprites/Characters/protagonist";
        public static string EnemyCharacterSpritesPath = "Sprites/Characters/enemies";

        #region prefabs

        //悬浮窗
        public static string ToolTipPrefabPath = "Prefabs/Tips/Tooltip";

        #endregion
    }
}
