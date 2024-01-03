using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleManager : MonoBehaviour
{
    public static TimeScaleManager instance = null;

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
            TimeScale1();
        }
        else if (Input.GetKey(KeyCode.W))
        {
            TimeScale2();
        }
        else if (Input.GetKey(KeyCode.E))
        {
            TimeScaleDot1();
        }
        else if (Input.GetKey(KeyCode.R))
        {
            TimeScaleZero();
        }
    }


    public void TimeScale1()
    {
        Time.timeScale = 1;
    }
    public void TimeScale2()
    {
        Time.timeScale = 1.8f;
    }
    public void TimeScaleDot1()
    {
        Time.timeScale = 0.1f;
    }
    public void TimeScaleZero()
    {
        Time.timeScale = 0f;
    }
}
