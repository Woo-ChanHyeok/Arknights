using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public int Lv;
    public string PlayerName;
    public int Uid;
    public List<OperatorLineUp> haveOperList;
    public int Money;
    public int Orundum;
    public int Originite;
    public int Sanity;
    public int MaxSanity;
}
public class PlayerInfoManager : MonoBehaviour
{
    public static PlayerInfoManager instance = null;
    public PlayerInfo playerInfo;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);
    }
}
