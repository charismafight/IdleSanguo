using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Common;
using UnityEngine;

namespace Assets.Scripts.Chess
{
    public class ChessLock : BaseMonoBehaviour
    {
        public Sprite lockedSprite;

        public Sprite unlockedSprite;

        private bool locked;

        private int effectiveCount = 0;

        /// <summary>
        /// 锁定当前物体，并更新展示的图像效果
        /// 锁定物体，即不可再点击
        /// </summary>
        public void Lock()
        {
            effectiveCount++;

            if (locked)
            {
                return;
            }

            var c = transform.GetComponent<Collider2D>();
            if (c)
            {
                c.enabled = false;
            }

            var a = transform.GetComponent<SpriteRenderer>()?.sprite;
            if (a != null)
            {
                transform.GetComponent<SpriteRenderer>().sprite = lockedSprite;
            }

            locked = true;
        }

        public void Unlock()
        {
            if (!locked)
            {
                return;
            }

            //只有生效的计数器为1（即当前只有一个敌人锁定时，unlock成功，否则就减少计数器）
            if (effectiveCount != 1)
            {
                effectiveCount--;
                return;
            }

            var c = transform.GetComponent<Collider2D>();
            if (c)
            {
                c.enabled = true;
            }

            var a = transform.GetComponent<SpriteRenderer>()?.sprite;
            if (a != null)
            {
                transform.GetComponent<SpriteRenderer>().sprite = unlockedSprite;
            }

            locked = false;
            effectiveCount--;
        }
    }
}
