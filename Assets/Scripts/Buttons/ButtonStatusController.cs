using Assets.Scripts;
using Assets.Scripts.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStatusController : MonoBehaviour
{
    public bool Locked = false;
    public bool HideTextWhenLocked = false;
    GameObject Locker;
    Button Btn;
    TextMeshProUGUI TextMeshProUGUI;

    bool Initiated = false;

    // Start is called before the first frame update
    void Start()
    {
        Locker = transform.Find(Constants.ImageLocker)?.gameObject;
        if (Locker == null)
        {
            throw new NullComponentException(Constants.ImageLocker);
        }

        Btn = GetComponent<Button>();
        if (Btn == null)
        {
            throw new NullComponentException("Button");
        }

        TextMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>(true);
        if (TextMeshProUGUI == null)
        {
            throw new NullComponentException("TextMeshProUGUI");
        }

        Lock(Locked);
        Initiated = true;
    }

    void Lock(bool lockedStatus = false)
    {
        //锁定，则显示锁，即true
        Locker.SetActive(lockedStatus);
        //锁定，则禁止按钮交互，即false
        Btn.interactable = !lockedStatus;
        //根据选项处理在锁定时隐藏文本
        //如果没有初始化，也不要处理，hover才出现的按钮是这个逻辑
        if (HideTextWhenLocked && Initiated)
        {
            TextMeshProUGUI?.gameObject.SetActive(!lockedStatus);
        }
    }
}
