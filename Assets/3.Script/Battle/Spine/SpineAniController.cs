using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class SpineAniController : MonoBehaviour
{
    private SkeletonAnimation Ani;

    void Start()
    {
        TryGetComponent(out Ani);
        SetIdle(Ani);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Ani.AnimationName == "Idle")
        {
            StartAttack(Ani);
        }
        else if(Input.GetKeyDown(KeyCode.Space) && Ani.AnimationName == "Attack")
        {
            EndAttack(Ani);
        }
    }

    public void SetIdle(SkeletonAnimation Ani)
    {
        Ani.AnimationState.AddAnimation(0, "Idle", true, 0f);
    }
    public void StartAttack(SkeletonAnimation Ani)
    {
        //Ani.AnimationName = "Attack_Begin";
        Ani.AnimationState.SetAnimation(0, "Attack_Begin", false);
        Ani.AnimationState.AddAnimation(0, "Attack", true, 0f);
    }
    public void EndAttack(SkeletonAnimation Ani)
    {
        //Ani.AnimationName = "Attack_End";
        Ani.AnimationState.SetAnimation(0, "Attack_End", false);
        Ani.AnimationState.AddAnimation(0, "Idle", true, 0f);
    }

}
