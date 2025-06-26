using System;
using Assets.Common;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Assets.Common.Extensions;
using Assets.Scripts.Config.GameConfigs.Stat;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// 属性面板脚本，用于处理属性显示
/// </summary>
public class PropertiesBoard : BaseMonoBehaviour
{
    List<string> statProperties;

    public GameObject character;

    Stat stat;

    public void InitStat(Stat s)
    {
        stat = s;
    }

    void Awake()
    {
        if (statProperties == null)
        {
            statProperties = typeof(BaseStat).GetProperties().Select(p => p.Name).ToList();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (character == null)
        {
            Debug.Log($"{transform.name}的character属性异常，无法进行属性更新");
            return;
        }

        LoadStat();
        LoadBar();
    }

    public void Refresh()
    {
        LoadStat();
        LoadBar();
    }

    //通过一定的规则（规定属性名+Text作为查找文本控件的条件）给控件赋值用以显示
    void LoadStat()
    {
        foreach (var p in statProperties)
        {
            var controlName = $"{p}Text";
            var control = transform.GetChildObjects(controlName);
            if (!control.Any())
            {
                //L($"未能找到{controlName}");
                continue;
            }

            //获取到了控件后找textmeshpro
            var txt = control.First().GetComponent<TMP_Text>();
            try
            {
                var property = typeof(BaseStat).GetProperty(p);
                if (property == null)
                {
                    continue;
                }

                if (property.PropertyType == typeof(float))
                {
                    txt.text = ((float)property.GetValue(stat)).ToString("#");
                }
                else
                {
                    txt.text = property.GetValue(stat).ToString();
                }
            }
            catch (Exception e)
            {

            }
        }
    }

    void LoadBar()
    {
        var hpSlider = transform.Find("HP").GetComponentInChildren<Slider>();
        var mpSlider = transform.Find("MP").GetComponentInChildren<Slider>();
        Slider expSlider;
        if (transform.Find("LVEXP") != null)
        {
            expSlider = transform.Find("LVEXP").GetComponentInChildren<Slider>();
            if (stat.MaxExp != 0)
            {
                //经验
                expSlider.value = stat.Exp / stat.MaxExp;
            }
            else
            {
                expSlider.value = 0;
            }
        }

        if (stat.MaxHP != 0)
        {
            //红
            hpSlider.value = stat.HP / stat.MaxHP;
        }
        else
        {
            hpSlider.value = 0;
        }

        if (stat.MaxMP != 0)
        {
            //蓝
            mpSlider.value = stat.MP / stat.MaxMP;
        }
        else
        {
            mpSlider.value = 0;
        }
    }
}
