using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConfirmWindow : MonoBehaviour
{
    public string Title;
    public string Content;
    public Action Callback;

    private void Start()
    {
        GameObject.Find("Title").GetComponent<TextMeshProUGUI>().text = Title;
        GameObject.Find("Content").GetComponent<TextMeshProUGUI>().text = Content;
    }

    [HideInInspector]
    public bool DialogResult = false;

    public void Ok()
    {
        DialogResult = true;
        Callback?.Invoke();
        Close();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
