using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapInfo
{
    public string MapName;
    public string MapNum;
    public int totalEnemy;
    public int DefenseHP;
    public int Char_Limit;
    public int FirstCost;
    public int SanityCost;
    public string Dangerrate;
    public string Information;
    public bool isClear;
    public int ClearRank;
}

public class MapStatus : MonoBehaviour
{
    public MapInfo mapInfo;   
}
