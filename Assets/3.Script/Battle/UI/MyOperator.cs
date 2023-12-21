using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyOperator : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private RectTransform prefabParent; // ������ �θ��

    [SerializeField] private GameObject OperInfo;

    private CameraController cameraController;
    private GameObject Oper;

    private bool isDrag = false;

    private void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    public void OnPointerDown()
    {
        Debug.Log("������?");

    }

    public void OnPointerUp()
    {
        if (isDrag)
            return;
        if (!OperInfo.activeSelf)
        {
            cameraController.TiltCamera();
            OperInfo.SetActive(true);
        }
        else if (OperInfo.activeSelf)
        {
            cameraController.RestoreCamera();
            OperInfo.SetActive(false);
        }
        Debug.Log("�ô�?");
    }

    public void OnBeginDrag()
    {
        Debug.Log("�巡�׽���");
        //Invoke("test",0.1f);
        GameObject oper = Instantiate(prefab);
        oper.transform.parent = prefabParent;
        oper.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Oper = oper;

        isDrag = true;

        cameraController.TiltCamera();
        OperInfo.SetActive(true);
    }

    public void OnDrag()
    {
        Debug.Log("�巡����");
        if (Oper != null)
        {
            Oper.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        }
    }

    public void OnEndDrag()
    {
        if (isDrag)
        {
            Debug.Log("�巡�� ��");
            if (Oper != null)
            {
                isDrag = false;
                cameraController.RestoreCamera();
                OperInfo.SetActive(false);

                Destroy(Oper);
            }
        }
    }
    //public void test()
    //{
    //    GameObject oper = Instantiate(prefab);
    //    oper.transform.parent = Canvas;
    //    oper.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
    //    Oper = oper;
    //}

}
