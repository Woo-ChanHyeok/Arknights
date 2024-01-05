using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkColliderScaler : MonoBehaviour
{
    private Transform parentTransform;
    private Vector3 initialLocalScale;

    void Start()
    {
        // �θ� Transform�� ���� ���� ȹ��
        parentTransform = transform.parent;

        // �ʱ� �ڽ��� ���� ������ ����
        initialLocalScale = transform.localScale;
    }

    void LateUpdate()
    {
        // �θ��� ���� ������ ��
        Vector3 parentLocalScale = parentTransform.localScale;

        // �θ��� ������ ��ȭ�� ���� �ڽ��� ũ�⸦ ����
        transform.localScale = new Vector3(
            Mathf.Sign(parentLocalScale.x) * initialLocalScale.x,
            Mathf.Sign(parentLocalScale.y) * initialLocalScale.y,
            Mathf.Sign(parentLocalScale.z) * initialLocalScale.z
        );
    }
}
