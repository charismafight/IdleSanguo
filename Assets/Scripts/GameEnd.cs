using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;

    public float endDuration = 3f;
    float counter = 0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Timer();
    }

    void EndGame()
    {
        Application.Quit();
    }

    void Timer()
    {
        counter += Time.fixedDeltaTime;
        if (counter >= endDuration)
        {
            EndGame();
        }
    }
}
