using Assets.Common.Base;
using Assets.Scripts.Sqlite;

namespace Assets.Scripts.Config.GameConfigs.Stat
{
    //[Table("Stat")]
    public abstract class BaseStat : BaseConfigModel
    {
        public int CharacterId { get; set; }
        public int LV { get; set; }
        public int Exp { get; set; }
        public int MaxExp { get; set; }

        public float MaxHP { get; set; }
        public float HP { get; set; }
        /// <summary>
        /// HP回复值(杀敌后)
        /// </summary>
        public float HPReneneration { get; set; }
        public float HPVampiric { get; set; }

        public float MaxMP { get; set; }
        public float MP { get; set; }

        /// <summary>
        /// MP回复值(杀敌后)
        /// </summary>
        public float MPReneneration { get; set; }

        public float ATK { get; set; }

        public float MATK { get; set; }

        public float DEF { get; set; }
        public float MDEF { get; set; }

        /// <summary>
        /// 怪物提供的经验价值
        /// </summary>
        public int SuppliedEXP { get; set; }

        public int Speed { get; set; } = 20;

        /// <summary>
        /// Stat类型，0-状态，1-成长
        /// </summary>
        public virtual int StatType { get; set; }
    }
}
