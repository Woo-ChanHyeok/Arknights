using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyOperator : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private RectTransform prefabParent; // 프리팹 부모용

    [SerializeField] private GameObject OperInfo; //UI

    [SerializeField] private Image OperImg;

    private CameraController cameraController;
    private GameObject Oper;

    private bool isDrag = false;

    private void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    private void Update()
    {
    }

    public void OnPointerDown()
    {
        Debug.Log("눌렀니?");
        //Oper_InfoUpdater.instance.GetOperStatus(gameObject.GetComponent<OperStatus>());
        //Oper_InfoUpdater.instance.UpdateUI();

    }

    public void OnPointerUp()
    {
        if (isDrag)
            return;
        OperStatus CurrentStatus = Oper_InfoUpdater.instance.operStatus;
        Oper_InfoUpdater.instance.GetOperStatus(gameObject.GetComponent<OperStatus>());
        Oper_InfoUpdater.instance.UpdateUI();

        if (!OperInfo.activeSelf)
        {
            //Oper_InfoUpdater.instance.GetOperStatus(gameObject.GetComponent<OperStatus>());
            //Oper_InfoUpdater.instance.UpdateUI();
            cameraController.TiltCamera();
            OperInfo.SetActive(true);
        }
        else if (OperInfo.activeSelf && Oper_InfoUpdater.instance.operStatus == CurrentStatus)
        {
            cameraController.RestoreCamera();
            OperInfo.SetActive(false);
        }
        Debug.Log("뗐니?");
    }

    public void OnBeginDrag()
    {
        Debug.Log("드래그시작");
        //Invoke("test",0.1f);
        GameObject oper = Instantiate(prefab);
        oper.transform.parent = prefabParent;
        oper.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Oper = oper;

        isDrag = true;
        Oper_InfoUpdater.instance.GetOperStatus(gameObject.GetComponent<OperStatus>());
        Oper_InfoUpdater.instance.UpdateUI();

        OperImgAlphaZero();
        OperImgTransform();


        cameraController.TiltCamera();
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
                OperImgAlpha();
                cameraController.RestoreCamera();
                OperImgTransformDefault();
                OperInfo.SetActive(false);
                Destroy(Oper);
            }
        }
    }




    private IEnumerator MoveImgSmooth(Vector3 ImgPos, Vector3 targetTransform, float duration)
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            // Lerp를 사용하여 부드러운 전환
            ImgPos = Vector3.Lerp(ImgPos, targetTransform, elapsedTime / duration);
            OperImg.rectTransform.localPosition = ImgPos;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // 전환 완료 후 위치 
        OperImg.rectTransform.localPosition = targetTransform;
    }

    private void OperImgTransform()
    {
        Vector3 target = new Vector3(-165, 0, 0);
        StartCoroutine(MoveImgSmooth(OperImg.rectTransform.localPosition, target, 0.1f));

    }
    private void OperImgTransformDefault()
    {
        Vector3 target = new Vector3(-35, 0, 0);
        OperImg.rectTransform.localPosition = target;
        //StartCoroutine(MoveImgSmooth(OperImg.transform, target, 0.1f));
    }
    private void OperImgAlpha()
    {
        OperImg.color = new Color(OperImg.color.r, OperImg.color.g, OperImg.color.b, 1f);
    }
    private void OperImgAlphaZero()
    {
        OperImg.color = new Color(OperImg.color.r, OperImg.color.g, OperImg.color.b, 0.3f);

    }


    //public void test()
    //{
    //    GameObject oper = Instantiate(prefab);
    //    oper.transform.parent = Canvas;
    //    oper.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
    //    Oper = oper;
    //}

}
