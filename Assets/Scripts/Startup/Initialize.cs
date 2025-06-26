using System;
using Assets.Common;
using Assets.Common.Cache;
using Assets.Scripts.Sqlite;

namespace Assets.Scripts.Startup
{
    public class Initialize : BaseMonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var a = CacheManager.Instance;
            LeanTween.init(200000);
        }
    }
}
