using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBarSync : MonoBehaviour
{
    [SerializeField] private Text Money;
    [SerializeField] private Text Orundum;
    [SerializeField] private Text Originite;
    
    void Start()
    {
        UpdateStatusBar();
    }

    public void UpdateStatusBar()
    {
        Money.text = PlayerInfoManager.instance.playerInfo.Money.ToString();
        Orundum.text = PlayerInfoManager.instance.playerInfo.Orundum.ToString();
        Originite.text = PlayerInfoManager.instance.playerInfo.Originite.ToString();
    }
}
