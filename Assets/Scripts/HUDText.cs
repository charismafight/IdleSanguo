using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDText : MonoBehaviour
{
    /// <summary>
    /// 漂浮文字生成到某个父Canvas
    /// </summary>
    Canvas ParnetCanvas;


    /// <summary>
    /// 漂浮文字预制体
    /// </summary>
    public GameObject TextPrefab;
    /// <summary>
    /// 时间
    /// </summary>
    public float FadeTime = 0.56f;

    void Start()
    {
        ParnetCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    /// <summary>
    /// 创建一个伤害漂浮文本
    /// </summary>
    /// <param name="content"></param>
    /// <param name="worldpos"></param>
    /// <param name="color"></param>
    /// <param name="type"></param>
    public void Add(string content, Vector3 worldpos, Color color)
    {
        //实例化出伤害漂浮文字, 这里最优的方法是使用对象池来控制
        //建议使用poolmanager插件,当然你可以自己写
        //本案例简单使用Instantiate方法,比较消耗性能
        GameObject text = Instantiate(TextPrefab) as GameObject;
        RectTransform floatingTextT = text.transform as RectTransform;
        floatingTextT.SetParent(ParnetCanvas.transform, false);
        floatingTextT.localScale = Vector3.one;
        FloatingText floatingText = floatingTextT.GetComponent<FloatingText>();
        floatingText.text = content;
        floatingText.color = color;

        //UGUI处理坐标转换
        Vector2 pos;
        //先转化物体坐标为屏幕坐标
        Vector3 screenpos = Camera.main.WorldToScreenPoint(worldpos);
        //然后通过RectTransformUtility.ScreenPointToLocalPointInRectangle转化为UGUI真实坐标
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(ParnetCanvas.transform as RectTransform, screenpos, ParnetCanvas.worldCamera, out pos))
        {
            //设置漂浮文字坐标
            floatingTextT.localPosition = pos;
            //leanTween用法请查看相关帮助,其实还是比较简单的
            //向上漂浮
            LeanTween.moveLocalY(floatingTextT.gameObject, floatingTextT.localPosition.y + 100f, FadeTime).setEaseInOutQuad().setOnComplete(OnComplete, text);
        }
    }

    /// <summary>
    /// 漂浮文字移动结束,移除掉
    /// </summary>
    /// <param name="o"></param>
    private void OnComplete(object o)
    {
        Destroy((GameObject)o);
    }
}
