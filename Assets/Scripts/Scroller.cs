using Assets.Common;
using Assets.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Scroller : BaseMonoBehaviour
    {
        ScrollRect sr;
        int itemCount;
        float itemSpacing;

        void Start()
        {
            sr = InitComponent<ScrollRect>();
            //sr.onValueChanged.AddListener(L);
            itemCount = sr.content.transform.childCount;
            //每个item所占位置，-3表示最后一页有多少个item，滚动条的位置不包含最后一页，因为滚动条到位置1显示的是最后一页
            //todo need for test 当只有一页时，是否有异常
            itemSpacing = 1f / (itemCount - 3);

            ////only for test 滚动条显示控件名称
            //foreach (Transform item in sr.content.transform)
            //{
            //    var txt = item.GetComponentInChildren<TextMeshProUGUI>();
            //    if (txt != null)
            //    {
            //        txt.text = item.name;
            //    }
            //    Debug.Log($"item:{item.name}");
            //}
        }

        //public void L(Vector2 v)
        //{
        //    Debug.Log("ListenerMethod: " + sr.horizontalNormalizedPosition);
        //}

        public void MoveLeft()
        {
            sr.horizontalNormalizedPosition -= 3 * itemSpacing;
            if (sr.horizontalNormalizedPosition < 0)
            {
                sr.horizontalNormalizedPosition = 0;
            }
            Debug.Log($"normailized position:{sr.horizontalNormalizedPosition}");
        }

        public void MoveRight()
        {
            sr.horizontalNormalizedPosition += 3 * itemSpacing;
            if (sr.horizontalNormalizedPosition > 1)
            {
                sr.horizontalNormalizedPosition = 1;
            }
            Debug.Log($"normailized position:{sr.horizontalNormalizedPosition}");
        }
    }
}
