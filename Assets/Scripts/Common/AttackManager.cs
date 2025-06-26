using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Config.GameConfigs.Stat;

namespace Assets.Scripts.Common
{
    /// <summary>
    /// 攻击管理类，主要负责与攻击相关的算法管理
    /// </summary>
    public static class AttackManager
    {
        /// <summary>
        /// 根据攻击和被攻击者的状态（或许会改为其他信息，比如技能？暂时简单处理）选择攻击方式
        /// </summary>
        /// <param name="attackerStat"></param>
        /// <param name="sufferStat"></param>
        /// <returns></returns>
        public static bool UseSkill(Stat attackerStat, Stat sufferStat)
        {
            //如果有蓝可以用则优先使用技能
            if (AffordSkill(attackerStat))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool AffordSkill(Stat stat)
        {
            return false;
        }

        public static int CalculateAttackDamage(Stat attackerStat, Stat sufferStat)
        {
            if (attackerStat.ATK <= sufferStat.DEF)
            {
                return 0;
            }

            if (attackerStat.ATK - sufferStat.DEF < 1)
            {
                return 1;
            }

            return Convert.ToInt32(attackerStat.ATK - sufferStat.DEF);
        }
    }
}
