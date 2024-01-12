using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSync : MonoBehaviour
{

    private Text RearTime;
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out RearTime);
    }

    // Update is called once per frame
    void Update()
    {
        RearTime.text = WhatTimeisItNow();
    }
    private string WhatTimeisItNow()
    {
        return System.DateTime.Now.ToString("yyyy/MM/dd HH:mm").Replace('-', '/');

    }
}
