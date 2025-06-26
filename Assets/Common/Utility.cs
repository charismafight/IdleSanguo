using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

namespace Assets.Common
{
    public static class Utility
    {
        public static T GetARandomObject<T>(List<T> list)
        {
            var randomIndex = new Random().Next(list.Count);
            return list[randomIndex];
        }

        public static void ShowTips(string txt)
        {
            //实例化tip窗体，并显示内容
            //坐标基于鼠标位置需要计算一个窗口位置出来
            var tooltipSprite = ResourceManager.GetSprite<GameObject>(IdleConstants.ToolTipPrefabPath);
            Object.Instantiate(tooltipSprite);
        }
    }
}
