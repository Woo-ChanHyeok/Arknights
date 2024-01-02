using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PositionType
{
    Floor,
    Upper
}
public enum OperClass
{
    Vanguard,
    Guard,
    Defender,
    Sniper,
    Caster,
    Medic,
    Supporter,
    Specialist
}
public enum EliteType
{
    Default,
    Elite1,
    Elite2
}
public enum AtkType
{
    None,
    Physical,
    Arts,
    True
}
[System.Serializable]
public class OperInfo
{
    public PositionType OperType;
    public OperClass ClassType;
    public EliteType OperElite;
    public AtkType OperAtkType;
    public string OperName;
    public int Lv;
    public int CurrentHP;
    public int MaxHP;
    public int AtkPower;
    public float AtkSpeed;
    public int Defense;
    public int MagicRes;
    public float RestoreTime;
    public int CardCost;
    public int Block;

    public string Skil_Name;
    public string Skil_Info;
    public int CurrentSP;
    public int MaxSP;
}
public class OperStatus : MonoBehaviour
{
    public OperInfo operInfo;
    public Sprite[] OperImage;
    public Sprite SkilIcon;
}
