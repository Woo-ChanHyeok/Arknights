using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperStateTimeScale : MonoBehaviour
{
    private void OnEnable()
    {
        TimeScaleManager.instance.TimeScaleDot2();
    }
    private void OnDisable()
    {
        TimeScaleManager.instance.TimeScaleCurrent();
    }
}
