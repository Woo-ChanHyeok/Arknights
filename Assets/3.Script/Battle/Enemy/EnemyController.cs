using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyStatus enemyStatus;
    private void Start()
    {
        TryGetComponent(out enemyStatus);
    }

    private void FixedUpdate()
    {
        if (enemyStatus.enemyInfo.CurrentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
