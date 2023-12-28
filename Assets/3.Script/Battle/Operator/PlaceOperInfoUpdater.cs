using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceOperInfoUpdater : MonoBehaviour
{
    public static PlaceOperInfoUpdater instance = null;

    private OperStatus operStatus = null;
    private CameraController cameraController;

    private int layer;
    
    [SerializeField] private GameObject operInfo;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //TryGetComponent(out operStatus);
        layer = 1 << LayerMask.NameToLayer("PlaceRayTarget");
        cameraController = Camera.main.GetComponent<CameraController>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("레이쏘기");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
            {
                Debug.Log(hit.collider.gameObject);
                if (hit.collider.gameObject.CompareTag("PlaceOperInfo"))
                {
                    operStatus = hit.collider.GetComponentInParent<OperStatus>();
                    OnPointerUp();
                    Debug.Log("응애");
                }
            }
        }
    }
    public void OnPointerUp()
    {
        Debug.Log("a");
        Oper_InfoUpdater.instance.GetOperStatus(operStatus);
        Oper_InfoUpdater.instance.UpdateUI();
        Oper_InfoUpdater.instance.OperImgAlphaZero();
        Oper_InfoUpdater.instance.OperImgTransform();
        operInfo.SetActive(true);
        cameraController.TiltCamera();
        PlaceOperFrame.instance.SetPos(operStatus.gameObject.transform.position);
        //charMenuFrame.instance.SetPos(transform.position);
    }

    public void Escape_Oper()
    {
        Destroy(operStatus.transform.parent.gameObject);
        PlaceOperFrame.instance.UIOff();
    }
}
