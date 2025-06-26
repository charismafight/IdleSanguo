using System.Collections;
using System.Collections.Generic;
using Assets.Common;
using TMPro;
using UnityEngine;

public class TooltipScript : BaseMonoBehaviour
{
    private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = InitComponent<TMP_Text>();
    }

    void Update()
    {
        //��������λ�ø������
        //L($"{Input.mousePosition.x}  {Input.mousePosition.y}");
        //�������λ�ã���Ҫ��һ��ת�������0,0����Ļ���½ǣ�����0,0����Ļ���м䣨�������������ʱ=����������00+��Ļ��ߣ�
        //ת����ʽ��
        //���x-��Ļx/2=����������x
        //���y-��Ļy/2=����������y
        var x = Input.mousePosition.x - Screen.width / 2f;
        var y = Input.mousePosition.y - Screen.height / 2f;
        //����ϵת���������ȥ��Ļ��С֮������ͱ�Ϊ-960 0 960 ��������0ȥ���ж���

        //����ת����
        //��Ҫ������Ļλ�ã������ĸ��ǣ���֤��������ȫ��ʾ��
        //��Сλ�ã����� ����w/2 ����h/2������ ����w/2 ��Ļ-h/2������ ��Ļ-w/2 ��Ļ-h/2������ ��Ļ-w/2 ����h/2
        //x��СֵΪ�����һ��ֵ
        //if (x < transform.GetComponent<RectTransform>().sizeDelta.x / 2f)
        //{
        //    x = transform.GetComponent<RectTransform>().sizeDelta.x / 2f;
        //}

        //if (x > Screen.width - transform.localPosition.x / 2f)
        //{
        //    x = Screen.width - transform.localPosition.x / 2f;
        //}

        //if (y < transform.localPosition.y / 2f)
        //{
        //    y = transform.localPosition.y / 2f;
        //}

        //if (y < Screen.height - transform.localPosition.y / 2f)
        //{
        //    y = Screen.height - transform.localPosition.y / 2f;
        //}
        L($"x:{x} y:{y}");
        transform.localPosition = new Vector3(x, y, 0);

        L($"x:{transform.GetComponent<RectTransform>().sizeDelta.x}   y:{transform.GetComponent<RectTransform>().sizeDelta.y}");
    }

    public void SetText(string txt)
    {
        text.text = txt;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Show(string txt)
    {
        SetText(txt);
        Show();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
