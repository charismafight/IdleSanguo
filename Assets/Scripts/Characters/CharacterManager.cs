using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Common.Cache;
using Assets.Scripts.Config.GameConfigs;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    public static class CharacterManager
    {
        public static CharacterConfig SelectedCharacterConfig;

        static CharacterManager()
        {
            //todo only for test
            SelectedCharacterConfig = CacheManager.Instance.Get<CharacterConfig>(c => c.Id == 1);
        }
    }
}
