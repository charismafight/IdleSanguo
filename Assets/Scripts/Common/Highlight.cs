using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Common
{
    public class Highlight : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
    {
        public void OnPointerEnter(PointerEventData eventData)//当鼠标进入UI后执行的事件执行的
        {
            transform.localScale = new Vector3(1.2f, 1.2f, 1f);
        }

        public void OnPointerExit(PointerEventData eventData)//当鼠标离开UI后执行的事件执行的
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
