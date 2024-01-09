using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapInfo
{
    public string MapName;
    public int totalEnemy;
    public int DefenseHP;
    public int Char_Limit;
    public int FirstCost;
}
//8
//Max life point
//20
//Initial cost
//10

public class MapStatus : MonoBehaviour
{
    public MapInfo mapInfo;   
}
