using Assets.Common.Cache;
using Assets.Scripts.Config.GameConfigs;
using Assets.Scripts.Config.GameConfigs.Stat;
using UnityEngine;

namespace Assets.Scripts.AnimationMaker
{
    public class AnimationManagerScript : MonoBehaviour
    {
        public GameObject enemy;

        // Start is called before the first frame update
        void Start()
        {
            var cconfig = CacheManager.Instance.Get<CharacterConfig>(c => c.Id == 1);
            enemy.GetComponent<Character>().InitCConfig(cconfig);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
