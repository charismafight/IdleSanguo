using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Common;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Assets.Scripts.Chess
{
    public class LayerMove : BaseMonoBehaviour
    {
        void Start()
        {

        }

        void Update()
        {

        }

        public void MoveTo(string layerName)
        {
            var render = GetComponent<Renderer>();
            if (render == null)
            {
                Debug.LogError($"未能找到{transform.name}的render组件，无法运行MoveTo函数");
                return;
            }

            render.sortingLayerName = layerName;
        }
    }
}
