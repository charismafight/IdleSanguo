using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Common.Extensions
{
    public static class TransformExtension
    {
        public static List<GameObject> GetChildObjects(this Transform transform, string name = "")
        {
            return GetAllChilds(transform, name);
        }

        public static List<T> GetAllChildComponents<T>(this Transform transform)
        {
            return GetAllChilds(transform).Select(p => p.GetComponent<T>()).ToList();
        }

        public static List<GameObject> GetAllChilds(Transform transform, string name = "")
        {
            var result = new List<GameObject>();
            foreach (Transform item in transform)
            {
                if (item.childCount != 0)
                {
                    result.AddRange(GetAllChilds(item, name));
                }
                else
                {
                    if (string.IsNullOrEmpty(name))
                    {
                        result.Add(item.gameObject);
                    }
                    else if (name == item.gameObject.name)
                    {
                        result.Add(item.gameObject);
                    }
                }
            }

            return result;
        }
    }
}
