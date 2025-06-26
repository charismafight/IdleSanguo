using System;
using Assets.Scripts.Sqlite;

namespace Assets.Scripts.Config.GameConfigs.Stat
{
    [Table("Stat")]
    [Serializable]
    public class Stat : BaseStat
    {
        //public override int StatType => 0;

        [Ignore]
        public bool Injured => HP < MaxHP;

        public void GrowUp(BaseStat s)
        {
            if (s == null)
            {
                return;
            }

            //数值型的数据需要在这里进行计算实现，这样可以让Buff行为以设计器配置实现
            MaxHP += s.MaxHP;
            HP += s.HP;
            MaxMP += s.MaxMP;
            MP += s.MP;
            ATK += s.ATK;
            MATK += s.MATK;
            DEF += s.DEF;
            MDEF += s.MDEF;
            Speed += s.Speed;
        }
    }
}
