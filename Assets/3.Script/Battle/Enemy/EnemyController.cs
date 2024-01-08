using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Spine.Unity;

public class EnemyController : MonoBehaviour
{
    public bool isBlocked = false;
    private float elapsedTime = 0f;

    public OperStatus operStatus;
    public BlockEnemyList blockEnemy;
    private EnemyStatus enemyStatus;
    [SerializeField] private SkeletonAnimation EnemyAni;
    [SerializeField] private GameObject endBox;
    private NavMeshAgent navMesh;
    private void Start()
    {
        endBox = GameObject.FindGameObjectWithTag("endBox");
        TryGetComponent(out enemyStatus);
        TryGetComponent(out navMesh);
        navMesh.SetDestination(endBox.transform.position);
        navMesh.updateRotation = false;
        navMesh.speed = enemyStatus.enemyInfo.MovementSpeed * 0.5f;

        EnemyAni = GetComponentInChildren<SkeletonAnimation>();
        EnemyAni.AnimationState.Event += SpineEventHandler;
    }

    private void FixedUpdate()
    {
        if (enemyStatus.enemyInfo.CurrentHP <= 0)
        {
            Destroy(gameObject);
        }
        if (navMesh.isStopped && EnemyAni.AnimationName == "Move_Loop")
        {
            StopMove_Ani();
        }
        else if(!navMesh.isStopped && (EnemyAni.AnimationName == "Idle" || EnemyAni.AnimationName == "Attack"))
        {
            Debug.Log("제발 한번만 호출돼줘");
            StartMove_Ani();
        }


        elapsedTime += Time.deltaTime;

        if (operStatus != null)
        {
            navMesh.isStopped = true;
            EnemyAttack();
        }
        else if (operStatus == null /*나중에 조건 추가*/)
        {
            navMesh.isStopped = false;
        }
    }

    private void EnemyAttack()
    {
        if (elapsedTime > enemyStatus.enemyInfo.AtkSpeed)
        {
            if (blockEnemy.blockEnemy.Contains(gameObject) && EnemyAni.AnimationName == "Attack")
            {
                StayAttack_Ani();
                elapsedTime = 0f;
            }
            else
            {
                StartAttack_Ani();
                elapsedTime = 0f;
            }
        }
        
    }

    private void RotateMoveDirection()
    {
        Vector3 moveDirection = navMesh.velocity.normalized;
        if (moveDirection != Vector3.zero)
        {
            Vector3 rightVector = Vector3.Cross(Vector3.up, moveDirection);
            Vector3 leftVector = -rightVector;
            //if(rightVector)
        }
    }

    private void SpineEventHandler(Spine.TrackEntry trackEntry, Spine.Event e)
    {
        string eventname = e.Data.Name;
        if (eventname.Equals("OnAttack"))
        {
            AtkCalculator(operStatus, enemyStatus);
        }
    }

    private void AtkCalculator(OperStatus oper, EnemyStatus enemy)
    {
        if (enemy.enemyInfo.EnemyAtkType == AtkType.Physical)
        {
            int damage = enemy.enemyInfo.AtkPower - oper.operInfo.Defense;
            oper.operInfo.CurrentHP -= Mathf.Max(1, damage);
        }
        else if (enemy.enemyInfo.EnemyAtkType == AtkType.Arts)
        {
            int damage = enemy.enemyInfo.AtkPower - (enemy.enemyInfo.AtkPower * oper.operInfo.MagicRes / 100);
            oper.operInfo.CurrentHP -= Mathf.Max(1, damage);
        }
        else if (enemy.enemyInfo.EnemyAtkType == AtkType.True)
        {
            oper.operInfo.CurrentHP -= enemy.enemyInfo.AtkPower;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Operator"))
        {
            operStatus = other.GetComponentInParent<OperStatus>();
            blockEnemy = other.GetComponent<BlockEnemyList>();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (operStatus != null)
            return;
        else if (other.CompareTag("Operator"))
        {
            operStatus = other.GetComponentInParent<OperStatus>();
            blockEnemy = other.GetComponent<BlockEnemyList>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Operator"))
        {
            operStatus = null;
            blockEnemy = null;
        }
    }

    //-----------------------------Animation-----------------------------
    private void StartMove_Ani()
    {
        EnemyAni.AnimationState.SetAnimation(0, "Move_Begin", false);
        EnemyAni.AnimationState.AddAnimation(0, "Move_Loop", true, 0);
    }
    private void StopMove_Ani()
    {
        EnemyAni.AnimationState.SetAnimation(0, "Move_End", false);
        EnemyAni.AnimationState.AddAnimation(0, "Idle", true, 0);
    }
    private void StartAttack_Ani()
    {
        EnemyAni.AnimationState.SetAnimation(0, "Attack", false);
    }
    private void StayAttack_Ani()
    {
        EnemyAni.AnimationState.AddAnimation(0, "Attack", false, 0);
    }
    private void SetIdle_Ani()
    {
        EnemyAni.AnimationState.SetAnimation(0, "Idle", true);
    }
    private void Die_Ani()
    {
        EnemyAni.AnimationState.SetAnimation(0, "Die", false);
    }



}
