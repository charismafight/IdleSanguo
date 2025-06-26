using Assets.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Config.GameConfigs
{
    public class Story : BaseConfigModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// 选择项图像的背景地址
        /// </summary>
        public string SelectorSpritePath { get; set; }

        /// <summary>
        /// 战场背景图像地址
        /// </summary>
        public string BattleFieldBackgroundSpritePath { get; set; }


    }
}
