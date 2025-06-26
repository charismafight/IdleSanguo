using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Common.Base;

namespace Assets.Scripts.Config.GameConfigs
{
    public class CharacterConfig : BaseConfigModel
    {
        public string Name { get; set; }

        /// <summary>
        /// 例：caocao
        /// 角色的prefix 按规则找资源
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 角色类别
        /// <example>主角、敌人、友军等</example>
        /// </summary>
        public int CharacterType { get; set; }

        public string SpritePath { get; set; }

        public string ProfileSpritePath { get; set; }

        public string AnimationPath { get; set; }

        /// <summary>
        /// 角色（如需）加载预制体的路径
        /// </summary>
        public string PrefabPath { get; set; }
    }
}
