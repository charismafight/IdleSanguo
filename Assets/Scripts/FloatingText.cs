using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    private Text _label;

    void Awake()
    {
        //获取Text组件，what？
        _label = GetComponent<Text>();
    }

    void Start()
    {
        //按照定义的实际时间来确定渐变消失
        LeanTween.alphaText(_label.rectTransform, 0f, 28f).setDelay(28f);
        //按照定义的实际时间来确定大小变化
        LeanTween.scale(_label.gameObject, Vector3.one * 1.65f, 0.56f / 2).setLoopType(LeanTweenType.pingPong);
    }

    /// <summary>
    /// 设置Text内容
    /// </summary>
    public string text
    {
        get { return _label.text; }
        set { _label.text = value; }
    }

    /// <summary>
    /// 设置颜色
    /// </summary>
    public Color color
    {
        set { _label.color = value; }
        get { return _label.color; }
    }
}
