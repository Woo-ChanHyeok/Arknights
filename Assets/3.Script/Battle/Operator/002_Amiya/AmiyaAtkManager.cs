using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class AmiyaAtkManager : AtkManager
{
    public float initialSpeed = 2f;
    public float acceleration = 5f;
    public GameObject projectile;

    [SerializeField] private GameObject FrontAtkEffect;
    private GameObject BackAtkEffect;

    private void Start()
    {
        FrontAtkEffect = effect.transform.GetChild(0).gameObject;
        BackAtkEffect = effect.transform.GetChild(1).gameObject;
        skelAni.AnimationState.Event += SpineEventHandler;
    }
    public override void AttackEnemy(GameObject Enemy)
    {
        if (operStatus.operInfo.AtkDelay > elapsedTime)
            return;
        SkeletonDataAsset Current = skelAni.skeletonDataAsset;
        if(skelAni.AnimationName == "Idle")
        {
            if (AngleCalculator(Enemy))
            {
                spineAniController.ChangeBack();
            }
            else
            {
                spineAniController.ChangeFront();
            }
            if(Current != skelAni.skeletonDataAsset)
            {
                skelAni.AnimationState.Event += SpineEventHandler;
            }
            directionCalculator(Enemy);
            spineAniController.StartAttack();
        }
        else
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
            directionCalculator(Enemy);
            spineAniController.AttackStay();
        }


        elapsedTime = 0f;
    }
    private void CreateProjectile()
    {
        EffectOff();
        EffectOn();
        GameObject projectile1 = Instantiate(projectile, transform.position, Quaternion.identity, transform);
        GameObject projectile2 = Instantiate(projectile, transform.position, Quaternion.identity, transform);

        StartCoroutine(MovementProjectile(projectile1));
        StartCoroutine(MovementProjectile(projectile2));
    }

    private IEnumerator MovementProjectile(GameObject projectile)
    {
        //projectile.transform.Translate(new Vector3(-0.3f, 0.3f, 0.0f) * initialSpeed * Time.deltaTime);
        var Position = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 1f), 0);
        var Pos = transform.position + Position;
        float time = 0;
        while (Target != null && time < 0.2f)
        {
            projectile.transform.Translate(Pos * initialSpeed * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }

        while (Target != null && projectile != null)
        {
            Vector3 directionToTarget = (Target.transform.position - projectile.transform.position).normalized;
            projectile.transform.Translate(directionToTarget * acceleration * Time.deltaTime);
            yield return null;
        }
    }

    private void SpineEventHandler(Spine.TrackEntry trackEntry, Spine.Event e)
    {
        string eventname = e.Data.Name;
        if (eventname.Equals("OnAttack"))
        {
            CreateProjectile();
        }
    }

    private void EffectOn()
    {
        if (skelAni.skeletonDataAsset == spineAniController.skelData.FrontAsset)
            FrontAtkEffect.SetActive(true);
        else
            BackAtkEffect.SetActive(true);
    }
    private void EffectOff()
    {
        if (skelAni.skeletonDataAsset == spineAniController.skelData.FrontAsset)
            FrontAtkEffect.SetActive(false);
        else
            BackAtkEffect.SetActive(false);
    }


}
