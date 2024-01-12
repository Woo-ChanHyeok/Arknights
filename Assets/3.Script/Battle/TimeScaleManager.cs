using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleManager : MonoBehaviour
{
    public static TimeScaleManager instance = null;
    public float currentTimeScale= 1f;
    public bool isPause = false;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            TimeScale1x();
        }
        else if (Input.GetKey(KeyCode.W))
        {
            TimeScale2x();
        }
        else if (Input.GetKey(KeyCode.E))
        {
            TimeScaleDot2();
        }
        else if (Input.GetKey(KeyCode.R))
        {
            TimeScaleZero();
        }

    }


    public void TimeScale1x()
    {
        currentTimeScale = 1f;
        if (!isPause)
        {
            Time.timeScale = currentTimeScale;
        }
    }
    public void TimeScale2x()
    {
        currentTimeScale = 1.8f;
        if (!isPause)
        {
            Time.timeScale = currentTimeScale;
        }
    }
    public void TimeScaleDot2()
    {
        Time.timeScale = 0.2f;
    }
    public void TimeScaleZero()
    {
        Time.timeScale = 0f;
    }
    public void TimeScaleCurrent()
    {
        Time.timeScale = currentTimeScale;
    }
}
