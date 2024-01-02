using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyInfo
{
    public AtkType EnemyAtkType;
    public string EnemyName;
    public int MaxHP;
    public int CurrentHP;
    public int AtkPower;
    public float AtkSpeed;
    public int Defense;
    public int MagicRes;
    public float MovementSpeed;
    public int LP_Penalty;
}

public class EnemyStatus : MonoBehaviour
{
    public EnemyInfo enemyInfo;
}
