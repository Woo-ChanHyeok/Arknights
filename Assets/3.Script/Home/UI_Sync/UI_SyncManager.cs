using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SyncManager : MonoBehaviour
{
    [SerializeField] private Text RearTime;
    [SerializeField] private Text Money;
    [SerializeField] private Text Orundum;
    [SerializeField] private Text Originite;
    [SerializeField] private Text Name;
    [SerializeField] private Text Lv;
    [SerializeField] private Text Uid;
    [SerializeField] private Text Sanity;
    // Start is called before the first frame update
    void Start()
    {
        UpdateStatusBar();
        UpdateLeftSpace();
    }

    // Update is called once per frame
    void Update()
    {
        RearTime.text = WhatTimeisItNow();
        Sanity.text = WhatSanityNow();
    }
    public void UpdateLeftSpace()
    {
        Name.text = PlayerInfoManager.instance.playerInfo.PlayerName;
        Lv.text = PlayerInfoManager.instance.playerInfo.Lv.ToString();
        Uid.text = "ID : " + PlayerInfoManager.instance.playerInfo.Uid.ToString();
    }
    public void UpdateStatusBar()
    {
        Money.text = PlayerInfoManager.instance.playerInfo.Money.ToString();
        Orundum.text = PlayerInfoManager.instance.playerInfo.Orundum.ToString();
        Originite.text = PlayerInfoManager.instance.playerInfo.Originite.ToString();
    }
    private string WhatTimeisItNow()
    {
        return System.DateTime.Now.ToString("yyyy/MM/dd HH:mm").Replace('-', '/');
    }
    private string WhatSanityNow()
    {
        return PlayerInfoManager.instance.playerInfo.Sanity.ToString();
    }
}
