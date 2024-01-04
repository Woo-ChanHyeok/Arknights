using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class AtkManager : MonoBehaviour
{
    public AttackableEnemyList AttackableEnemy;
    public BlockEnemyList BlockEnemy;
    public SkeletonAnimation skelAni;
    public SpineAniController spineAniController;
    public OperStatus operStatus;
    public GameObject effect;

    private Vector3 PlusScale = new Vector3(0.27f, 0.27f, 0.27f);
    private Vector3 MinusScale = new Vector3(-0.27f, 0.27f, 0.27f);

    public GameObject Target;

    public float elapsedTime = 0f;
    private void Awake()
    {
        operStatus = GetComponentInParent<OperStatus>();
        skelAni = GetComponentInParent<SkeletonAnimation>();
        spineAniController = GetComponentInParent<SpineAniController>();
        AttackableEnemy = skelAni.GetComponentInChildren<AttackableEnemyList>();
        BlockEnemy = skelAni.GetComponentInChildren<BlockEnemyList>();
        effect = skelAni.transform.Find("Effect").gameObject;
    }
    private void FixedUpdate()
    {
        DecideState();
        //if (Target != null)
        elapsedTime += Time.deltaTime;
    }
    private void DecideState()
    {
        if (AttackableEnemy.attackableEnemy.Count == 0 && BlockEnemy.blockEnemy.Count == 0)
        {
            Target = null;
            if (skelAni.AnimationName == "Attack")
            {
                skelAni.ClearState();
                spineAniController.EndAttack();
            }
            //else if (skelAni.AnimationName != "Idle")
            //{
            //    skelAni.ClearState();
            //    //spineAniController.SetIdle(); 
            //    spineAniController.AddIdle();
            //}
        }
        else if (BlockEnemy.blockEnemy.Count > 0)
        {
            Target = BlockEnemy.blockEnemy[0];
            AttackEnemy(Target);
        }
        else if(AttackableEnemy.attackableEnemy.Count > 0)
        {
            Target = AttackableEnemy.nearEnemy;
            AttackEnemy(Target);
        }

    }

    public virtual void AttackEnemy(GameObject Enemy)
    {

    }

    public bool AngleCalculator(GameObject target) // true = 拉规氢 false = 弊 寇
    {
        Vector3 directionToTarget = target.transform.position - transform.position;

        Vector3 forwardDirection = transform.forward;

        float angle = Vector3.Angle(forwardDirection, directionToTarget);

        if (angle < 45f)
        {
            Debug.Log("Within 60 degrees!");
            return true;
        }
        else
        {
            Debug.Log("Not within 60 degrees.");
            return false;
        }
    }
    public bool directionCalculator(GameObject target) // true = 坷弗率 false = 哭率
    {
        Vector3 relativePosition = target.transform.position - transform.position;

        Vector3 rightDirection = transform.right;

        if (Vector3.Dot(relativePosition, rightDirection) > 0)
        {
            Debug.Log("Target is on the right side");
            return true;
        }
        else
        {
            Debug.Log("Target is on the left side");
            return false;
        }
    }

    public IEnumerator ScaleMinus(GameObject gameObject)
    {
        while (Vector3.Distance(gameObject.transform.localScale, MinusScale) > 0.01f)
        {
            gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, MinusScale, 10f * Time.deltaTime);
            yield return null;
        }
        gameObject.transform.localScale = MinusScale;
    }
    public IEnumerator ScalePlus(GameObject gameObject)
    {
        while (Vector3.Distance(gameObject.transform.localScale, PlusScale) > 0.01f)
        {
            gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, PlusScale, 10f * Time.deltaTime);
            yield return null;
        }
        gameObject.transform.localScale = PlusScale;
    }

}
