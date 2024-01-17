using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEnemyList : MonoBehaviour
{
    private OperStatus operStatus;
    public List<GameObject> blockEnemy = new List<GameObject>();
    private void Start()
    {
        operStatus = GetComponentInParent<OperStatus>();
    }
    private void Update()
    {
        if (blockEnemy.Count > 0)
        {
            for (int i = 0; i < blockEnemy.Count; i++)
            {
                if (blockEnemy[i] == null)
                {
                    blockEnemy.Remove(blockEnemy[i]);
                }
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(blockEnemy.Count < operStatus.operInfo.Block && other.CompareTag("Enemy"))
        {
            blockEnemy.Add(other.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (blockEnemy.Contains(other.gameObject) && other.GetComponent<EnemyStatus>().enemyInfo.CurrentHP <= 0)
            {

                blockEnemy.Remove(other.gameObject);
            }
            else if (blockEnemy.Count < operStatus.operInfo.Block)
            {
                blockEnemy.Add(other.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (blockEnemy.Contains(other.gameObject))
            {
                blockEnemy.Remove(other.gameObject);
            }
        }
    }
}
