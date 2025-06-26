using Assets.Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeroInfo : BaseMonoBehaviour
{
    TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = InitComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowHeroDescription(string s)
    {
        text.text =  s;
    }
}
