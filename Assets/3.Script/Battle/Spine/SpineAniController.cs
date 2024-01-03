using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class SpineAniController : MonoBehaviour
{
    private SkeletonAnimation Ani;
    public OperSkeletonData skelData;

    void Start()
    {
        TryGetComponent(out skelData);
        TryGetComponent(out Ani);
        SetIdle();
    }

    public void SetIdle()
    {
        Ani.AnimationState.SetAnimation(0, "Idle", true);
    }
    public void AddIdle()
    {
        Ani.AnimationState.AddAnimation(0, "Idle", true, 0f);
    }
    public void StartAttack()
    {
        //Ani.AnimationName = "Attack_Begin";
        Ani.AnimationState.SetAnimation(0, "Attack_Begin", false);
        Ani.AnimationState.AddAnimation(0, "Attack", false, 0f);
    }
    public void AttackStay()
    {
        Ani.AnimationState.AddAnimation(0, "Attack", false, 0f);
    }
    public void EndAttack()
    {
        Ani.AnimationState.SetAnimation(0, "Attack_End", false);
    }

    //----------------------------���̷��� ������-------------------------------
    public void ChangeBack()
    {
        if (Ani.skeletonDataAsset != skelData.BackAsset)
        {
            Ani.ClearState();
            Ani.skeletonDataAsset = skelData.BackAsset;
            Ani.Initialize(true);
        }
    }
    public void ChangeFront()
    {
        if(Ani.skeletonDataAsset != skelData.FrontAsset)
        {
            Ani.ClearState();
            Ani.skeletonDataAsset = skelData.FrontAsset;
            Ani.Initialize(true);
        }
    }





}
