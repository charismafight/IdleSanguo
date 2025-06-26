using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Common.Base;

namespace Assets.Scripts.Config.GameConfigs
{
    public class StoryCharacter : BaseConfigModel
    {
        public int StoryId { get; set; }

        public int CharacterId { get; set; }

        public int Level { get; set; }

        /// <summary>
        /// 难度系数
        /// </summary>
        public float Difficulty { get; set; }
    }
}
