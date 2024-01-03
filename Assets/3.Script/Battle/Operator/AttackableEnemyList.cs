using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableEnemyList : MonoBehaviour
{
    public List<GameObject> attackableEnemy = new List<GameObject>();
    public GameObject nearEnemy = null;
    [SerializeField] private Transform endBox;
    private void Start()
    {
        endBox = GameObject.FindGameObjectWithTag("endBox").GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        NullCheck();
        DistanceCheck();
    }

    private void DistanceCheck()
    {
        if (attackableEnemy.Count == 0)
        {
            nearEnemy = null;
        }
        else if (attackableEnemy.Count == 1)
        {
            nearEnemy = attackableEnemy[0];
        }
        else
        {
            float minDistance = float.MaxValue;
            foreach (GameObject enemy in attackableEnemy)
            {
                float distance = Vector3.Distance(endBox.position, enemy.transform.position);
                if(distance < minDistance)
                {
                    minDistance = distance;
                    nearEnemy = enemy;
                }
            }
        }

    }

    private void NullCheck()
    {
        if (attackableEnemy.Count > 0)
        {
            foreach (GameObject enemy in attackableEnemy)
            {
                if (enemy == null)
                {
                    attackableEnemy.Clear();
                    break;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!attackableEnemy.Contains(other.gameObject))
            {
                Debug.Log("addlist");
                attackableEnemy.Add(other.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (attackableEnemy.Contains(other.gameObject))
            {
                attackableEnemy.Remove(other.gameObject);
            }
        }
    }
}


