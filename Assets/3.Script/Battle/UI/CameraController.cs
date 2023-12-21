using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform mainPos;
    [SerializeField] private Transform secondPos;
    public float transitionDuration = 0.1f; // ��ȯ �ð��� 0.1�ʷ� ����

    public void TiltCamera()
    {
        StartCoroutine(MoveCameraSmoothly(Camera.main.transform, secondPos, transitionDuration));
    }
    public void RestoreCamera()
    {
        StartCoroutine(MoveCameraSmoothly(Camera.main.transform, mainPos, transitionDuration));
    }

    IEnumerator MoveCameraSmoothly(Transform cameraTransform, Transform targetTransform, float duration)
    {
        float elapsedTime = 0.0f;
        Vector3 initialPosition = cameraTransform.position;
        Quaternion initialRotation = cameraTransform.rotation;

        while (elapsedTime < duration)
        {
            // Lerp�� ����Ͽ� �ε巯�� ��ȯ
            cameraTransform.position = Vector3.Lerp(initialPosition, targetTransform.position, elapsedTime / duration);
            cameraTransform.rotation = Quaternion.Slerp(initialRotation, targetTransform.rotation, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ��ȯ �Ϸ� �� ��ġ �� ȸ�� ����
        cameraTransform.position = targetTransform.position;
        cameraTransform.rotation = targetTransform.rotation;
    }
    //public void ToggleCamera()
    //{
    //    if (!toggleCamera)
    //    {
    //        Camera.main.transform.position = secondPos.position;
    //        Camera.main.transform.rotation = secondPos.rotation;
    //        toggleCamera = true;
    //    }
    //    else
    //    {
    //        Camera.main.transform.position = MainPos.position;
    //        Camera.main.transform.rotation = MainPos.rotation;
    //        toggleCamera = false;
    //    }
    //}
}
