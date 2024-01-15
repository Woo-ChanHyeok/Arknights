using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Star
{
    Star1 = 1,
    Star2,
    Star3,
    Star4,
    Star5,
    Star6
}
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
    public OperatorLineUp ItsMe;
    public Star OperStar;
    public PositionType OperType;
    public OperClass ClassType;
    public EliteType OperElite;
    public AtkType OperAtkType;
    public string OperName;
    public int Lv;
    public int CurrentHP;
    public int MaxHP;
    public int AtkPower;
    public float AtkDelay;
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
    public Sprite[] OperCardImage;
    public Sprite SkilIcon;
    public void CopyStatus(OperStatus other)
    {
        operInfo = other.operInfo;
        OperCardImage = other.OperCardImage;
        OperImage = other.OperImage;
    }
}
