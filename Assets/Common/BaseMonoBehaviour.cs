using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Common
{
    public class BaseMonoBehaviour : MonoBehaviour
    {
        protected GameObject TipWindow;

        private GameObject _tipWindow;

        public BaseMonoBehaviour()
        {
            if (TipWindow != null)
            {
                _tipWindow = Instantiate(TipWindow, new Vector3(0, 0, 0), Quaternion.identity);
            }
        }

        protected T InitComponent<T>()
        {
            var t = GetComponent<T>();
            if (t == null)
            {
                Debug.LogWarning($"未能初始化{name}的[{typeof(T).Name}]组件");
                return default;
            }

            return t;
        }

        protected T InitComponent<T>(GameObject go)
        {
            if (go == null)
            {
                return default;
            }


            var t = go.GetComponent<T>();
            if (t == null)
            {
                Debug.LogError($"未能初始化{go.name}的组件[{typeof(T).Name}]");
                return default;
            }

            return t;
        }

        protected T TryInitComponent<T>()
        {
            return GetComponent<T>();
        }

        protected T TryInitComponent<T>(GameObject go)
        {
            if (go == null)
            {
                return default;
            }

            return go.GetComponent<T>();
        }

        protected T InitUniqueComponentIncludeChirldren<T>()
        {
            var t = gameObject.GetComponentInChildren<T>();
            if (t == null)
            {
                Debug.LogWarning($"未能初始化{name}的[{typeof(T).Name}]组件");
                return default;
            }

            return t;
        }

        protected T InitUniqueComponentIncludeChirldren<T>(GameObject go)
        {
            if (go == null)
            {
                return default;
            }

            return go.GetComponentInChildren<T>();
        }

        protected void L(object s)
        {
            Debug.Log(s);
        }

        protected void E(object s)
        {
            Debug.LogError(s);
        }
    }
}
