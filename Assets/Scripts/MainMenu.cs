using Assets;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : BaseComponent
{
    public void StartStory()
    {
        Debug.Log($"Story started");
        //SceneManager.LoadScene("Board");
    }

    public void StartFree()
    {

    }

    public void Quit()
    {
        Confirm(() =>
        {
            Debug.Log("�˳�");
            Application.Quit();
        }, "ȷ���˳���Ϸ?");
    }
}
