using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class OperDirectionChanger : MonoBehaviour
{
    //private OperSkeletonData[] skelData;
    [SerializeField] private SkeletonDataAsset[] skelData;
    private SkeletonAnimation skelAni;

    private Transform originalTransform;
    private AtkRange atkRange;

    private void Start()
    {
        atkRange = GetComponentInChildren<AtkRange>();
        originalTransform = transform;
        //skelData = GetComponent<SkeletonDataAsset[]>();
        skelAni = GetComponent<SkeletonAnimation>();
    }

    private void Update()
    {
        if (CharMenuFrame.instance.isLeft)
        {
            atkRange.enabled = true;
            atkRange.DefaultDirection();
            skelAni.skeletonDataAsset = skelData[0];
            skelAni.Initialize(true);
            transform.localScale = originalTransform.localScale;

            transform.localScale = new Vector3(-0.27f, originalTransform.localScale.y, originalTransform.localScale.z);
        }
        else if (CharMenuFrame.instance.isRight)
        {
            atkRange.enabled = true;
            atkRange.DefaultDirection();
            skelAni.skeletonDataAsset = skelData[0];
            skelAni.Initialize(true);
            transform.localScale = originalTransform.localScale;

            transform.localScale = new Vector3(0.27f, originalTransform.localScale.y, originalTransform.localScale.z);

        }
        else if (CharMenuFrame.instance.isUp)
        {
            atkRange.enabled = true;
            atkRange.UpDirection();
            skelAni.skeletonDataAsset = skelData[1];
            skelAni.Initialize(true);
            transform.localScale = originalTransform.localScale;

        }
        else if (CharMenuFrame.instance.isDown)
        {
            atkRange.enabled = true;
            atkRange.DownDirection();
            skelAni.skeletonDataAsset = skelData[0];
            skelAni.Initialize(true);
            transform.localScale = originalTransform.localScale;

        }
        else if (CharMenuFrame.instance.isCenter)
        {
            AtkRangeOffManager.instance.MeshRendererOff();
            atkRange.enabled = false;
            //skelAni.skeletonDataAsset = skelData[0];
            //skelAni.Initialize(true);
            //transform.localScale = originalTransform.localScale;

        }
    }
}
