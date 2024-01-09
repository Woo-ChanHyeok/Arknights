using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Spine.Unity;

public class EnemyController : MonoBehaviour
{
    public bool isBlocked = false;
    private float elapsedTime = 0f;

    private bool isActive = true;

    public OperStatus operStatus;
    public BlockEnemyList blockEnemy;
    private EnemyStatus enemyStatus;
    [SerializeField] private SkeletonAnimation EnemyAni;
    [SerializeField] private GameObject endBox;
    private NavMeshAgent navMesh;
    private bool isChange = false;
    private float changeTime = 0.1f;
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
        if (!isActive)
            return;

        if (Vector3.Distance(endBox.transform.position, transform.position)  < 0.5f)
        {
            isActive = false;
            Debug.Log("체력 깎아버리기");
            Map_Information.instance.deathEnemy++;
            Map_Information.instance.defenseHP--;
            Map_Information.instance.UpdateInfo();
            //Destroy(gameObject);
            FadeOut();
            return;
        }

        if (enemyStatus.enemyInfo.CurrentHP <= 0)
        {
            isActive = false;
            Destroy(gameObject);
            return;
        }
        if (!navMesh.isStopped)
        {
            RotateMoveDirection();
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
            //Vector3 rightVector = Vector3.Cross(Vector3.up, moveDirection);
            //float Angle = Vector3.Dot(Vector3.up, rightVector);
            if (moveDirection.x >= 0)
            {
                Debug.Log("RightSide");
                StartCoroutine(RotateSmooth(true));
            }
            else
            {
                Debug.Log("LeftSide");
                StartCoroutine(RotateSmooth(false));
            }
        }
    }
    private IEnumerator RotateSmooth(bool direction)
    {
        if (isChange)
            yield break;

        float elapsedTime = 0f;
        if (direction)
        {
            while (elapsedTime < changeTime)
            {
                isChange = true;
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, elapsedTime / changeTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.localScale = Vector3.one;
        }
        else
        {
            while (elapsedTime < changeTime)
            {
                isChange = true;
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(-1, 1, 1), elapsedTime / changeTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.localScale = new Vector3(-1, 1, 1);
        }
        isChange = false;
        yield break;
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
        if (!isActive)
            return;
        if (other.CompareTag("Operator"))
        {
            operStatus = other.GetComponentInParent<OperStatus>();
            blockEnemy = other.GetComponent<BlockEnemyList>();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!isActive)
            return;
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
        if (!isActive)
            return;
        if (other.CompareTag("Operator"))
        {
            operStatus = null;
            blockEnemy = null;
        }
    }


    private void FadeOut()
    {
        Renderer renderer = EnemyAni.GetComponent<Renderer>();
        if (renderer != null)
        {
            Material copyMaterial = new Material(renderer.material); //복제
            renderer.material = copyMaterial;
            StartCoroutine(FadeOut_Co(copyMaterial));
        }
        else
        {
            Debug.Log("Renderer 찾기 실패");
        }
    }
    private IEnumerator FadeOut_Co(Material copyMaterial)
    {
        float elapsedTime = 0f;
        while (elapsedTime < 0.1f) //SetBlack
        {
            copyMaterial.color = Color.Lerp(copyMaterial.color, Color.black, elapsedTime / 0.1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elapsedTime = 0f;
        while(elapsedTime < 0.1f) //FadeOut
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / 0.1f);
            copyMaterial.color = new Color(copyMaterial.color.r, copyMaterial.color.g, copyMaterial.color.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield break;
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
