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
/// �������ű������ڴ���������ʾ
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
            Debug.Log($"{transform.name}��character�����쳣���޷��������Ը���");
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

    //ͨ��һ���Ĺ��򣨹涨������+Text��Ϊ�����ı��ؼ������������ؼ���ֵ������ʾ
    void LoadStat()
    {
        foreach (var p in statProperties)
        {
            var controlName = $"{p}Text";
            var control = transform.GetChildObjects(controlName);
            if (!control.Any())
            {
                //L($"δ���ҵ�{controlName}");
                continue;
            }

            //��ȡ���˿ؼ�����textmeshpro
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
                //����
                expSlider.value = stat.Exp / stat.MaxExp;
            }
            else
            {
                expSlider.value = 0;
            }
        }

        if (stat.MaxHP != 0)
        {
            //��
            hpSlider.value = stat.HP / stat.MaxHP;
        }
        else
        {
            hpSlider.value = 0;
        }

        if (stat.MaxMP != 0)
        {
            //��
            mpSlider.value = stat.MP / stat.MaxMP;
        }
        else
        {
            mpSlider.value = 0;
        }
    }
}
