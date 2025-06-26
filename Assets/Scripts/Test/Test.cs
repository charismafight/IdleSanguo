using Assets.Common.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    private ScrollRect sr;

    private Animator anim;

    private GameObject go;

    private int animatorHash = Animator.StringToHash("Rotation");

    private void Start()
    {
        sr = GetComponent<ScrollRect>();

        go = GameObject.Find("Image (1)");
        anim = go.GetComponent<Animator>();
    }

    public void TestMove(Vector2 v)
    {
        //Debug.Log($"x:{v.x}");

        if (anim.isActiveAndEnabled)
        {
            anim.Play(animatorHash, -1, v.x);
            anim.speed = 0;
        }
    }
}