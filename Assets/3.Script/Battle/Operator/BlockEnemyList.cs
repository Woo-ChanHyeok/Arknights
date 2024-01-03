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
        //blockEnemy.Capacity = operStatus.operInfo.Block;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(blockEnemy.Count < operStatus.operInfo.Block && other.CompareTag("Enemy"))
        {
            blockEnemy.Add(other.gameObject);
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
