using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceOperInfo : MonoBehaviour
{
    private OperStatus operStatus;
    private CameraController cameraController;


    private void Start()
    {
        TryGetComponent(out operStatus);
        cameraController = Camera.main.GetComponent<CameraController>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("레이쏘기");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.CompareTag("PlaceOperInfo"))
                {
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

        cameraController.TiltCamera();
        charMenuFrame.instance.SetPos(transform.position);
    }


}
