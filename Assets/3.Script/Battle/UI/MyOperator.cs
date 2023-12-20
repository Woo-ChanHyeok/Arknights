using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyOperator : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private RectTransform Canvas; // 프리팹 부모용

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
        Debug.Log("눌렀니?");

    }

    public void OnPointerUp()
    {
        if (isDrag)
            return;
        Debug.Log("뗐니?");
    }

    public void OnBeginDrag()
    {
        Debug.Log("드래그시작");
        //Invoke("test",0.1f);
        GameObject oper = Instantiate(prefab);
        oper.transform.parent = Canvas;
        oper.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Oper = oper;

        isDrag = true;

        cameraController.ToggleCamera();
        OperInfo.SetActive(true);
    }

    public void OnDrag()
    {
        Debug.Log("드래그중");
        if (Oper != null)
        {
            Oper.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        }
    }

    public void OnEndDrag()
    {
        if (isDrag)
        {
            Debug.Log("드래그 뗌");
            if (Oper != null)
            {
                isDrag = false;
                cameraController.ToggleCamera();
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
