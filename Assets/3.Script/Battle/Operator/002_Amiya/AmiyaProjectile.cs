using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmiyaProjectile : MonoBehaviour
{
    private AmiyaAtkManager atkManager;
    private OperStatus operstatus;
    private int damage = 0;
    void Start()
    {
        operstatus = transform.parent.parent.GetComponentInChildren<OperStatus>();
        atkManager = transform.parent.parent.GetComponentInChildren<AmiyaAtkManager>();
        //atkManager = GetComponentInParent<AmiyaAtkManager>();
        //operstatus = GetComponentInParent<OperStatus>();
    }

    private void Update()
    {
        damage = (int)(operstatus.operInfo.AtkPower * 0.5f);
        if(atkManager.Target == null)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && other.gameObject == atkManager.Target)
        {

            EnemyStatus enemyData = other.GetComponent<EnemyStatus>();
            int finalDamage = damage - (int)(damage * (float)enemyData.enemyInfo.MagicRes / 100);
            enemyData.enemyInfo.CurrentHP -= finalDamage;
            Destroy(gameObject);

        }
    }
}
