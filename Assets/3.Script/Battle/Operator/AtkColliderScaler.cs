using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkColliderScaler : MonoBehaviour
{
    private Transform parentTransform;
    private Vector3 initialLocalScale;

    void Start()
    {
        // 부모 Transform에 대한 참조 획득
        parentTransform = transform.parent;

        // 초기 자식의 로컬 스케일 저장
        initialLocalScale = transform.localScale;
    }

    void LateUpdate()
    {
        // 부모의 로컬 스케일 값
        Vector3 parentLocalScale = parentTransform.localScale;

        // 부모의 스케일 변화에 따라 자식의 크기를 조절
        transform.localScale = new Vector3(
            Mathf.Sign(parentLocalScale.x) * initialLocalScale.x,
            Mathf.Sign(parentLocalScale.y) * initialLocalScale.y,
            Mathf.Sign(parentLocalScale.z) * initialLocalScale.z
        );
    }
}
