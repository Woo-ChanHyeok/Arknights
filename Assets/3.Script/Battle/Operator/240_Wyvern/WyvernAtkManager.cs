using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class WyvernAtkManager : AtkManager
{
    private int damage = 0;

    private void Start()
    {
        skelAni.AnimationState.Event += SpineEventHandler;
    }

    public override void AttackEnemy(GameObject Enemy)
    {
        if (operStatus.operInfo.AtkDelay > elapsedTime && skelAni.AnimationName == "Attack")
            return;
        if (Target == null)
            return;

        damage = (operStatus.operInfo.AtkPower);
        if (directionCalculator(Enemy))
        {
            //¿À¸¥
            StartCoroutine(ScalePlus(skelAni.gameObject));
        }
        else
        {
            StartCoroutine(ScaleMinus(skelAni.gameObject));
        }

        SkeletonDataAsset Current = skelAni.skeletonDataAsset;
        if (skelAni.AnimationName == "Idle")
        {
            if (AngleCalculator(Enemy))
            {
                spineAniController.ChangeBack();
            }
            else
            {
                spineAniController.ChangeFront();
            }
            if (Current != skelAni.skeletonDataAsset)
            {
                skelAni.AnimationState.Event += SpineEventHandler;
            }
            spineAniController.StartAttack();
        }
        else if (skelAni.AnimationName == "Attack")
        {
            if (AngleCalculator(Enemy))
            {
                spineAniController.ChangeBack();
            }
            else
            {
                spineAniController.ChangeFront();
            }
            if (Current != skelAni.skeletonDataAsset)
            {
                skelAni.AnimationState.Event += SpineEventHandler;
            }
            if (Target != null)
            {
                spineAniController.AttackStay();
            }
        }


        elapsedTime = 0f;
    }

    private void SpineEventHandler(Spine.TrackEntry trackEntry, Spine.Event e)
    {
        string eventname = e.Data.Name;
        if (eventname.Equals("OnAttack"))
        {
            DamageCalculator();
        }
    }
    private void DamageCalculator()
    {
        EnemyStatus enemyData = Target.GetComponent<EnemyStatus>();
        int finalDamage = damage - enemyData.enemyInfo.Defense;
        enemyData.enemyInfo.CurrentHP -= finalDamage;
    }
}
