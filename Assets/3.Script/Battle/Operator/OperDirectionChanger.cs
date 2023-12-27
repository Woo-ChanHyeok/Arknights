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

    private void Start()
    {
        originalTransform = transform;
        //skelData = GetComponent<SkeletonDataAsset[]>();
        skelAni = GetComponent<SkeletonAnimation>();
    }

    private void Update()
    {
        if (charMenuFrame.instance.isLeft)
        {
            skelAni.skeletonDataAsset = skelData[0];
            skelAni.Initialize(true);
            transform.localScale = new Vector3(-0.27f, originalTransform.localScale.y, originalTransform.localScale.z);
        }
        else if (charMenuFrame.instance.isRight)
        {
            skelAni.skeletonDataAsset = skelData[0];
            skelAni.Initialize(true);
            transform.localScale = new Vector3(0.27f, originalTransform.localScale.y, originalTransform.localScale.z);

        }
        else if (charMenuFrame.instance.isUp)
        {
            skelAni.skeletonDataAsset = skelData[1];
            skelAni.Initialize(true);
            transform.localScale = originalTransform.localScale;

        }
        else if (charMenuFrame.instance.isDown)
        {
            skelAni.skeletonDataAsset = skelData[0];
            skelAni.Initialize(true);
            transform.localScale = originalTransform.localScale;

        }
        else if (charMenuFrame.instance.isCenter)
        {
            //skelAni.skeletonDataAsset = skelData[0];
            //skelAni.Initialize(true);
            //transform.localScale = originalTransform.localScale;

        }
    }
}
