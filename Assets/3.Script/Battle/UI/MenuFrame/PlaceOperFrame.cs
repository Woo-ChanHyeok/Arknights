using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceOperFrame : MonoBehaviour
{
    public static PlaceOperFrame instance = null;

    [SerializeField] private GameObject MenuUI;
    public GameObject Frame;
    [SerializeField] private Camera secondCamera;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPos(Vector3 OperPos)
    {
        MenuUI.SetActive(true);
        Frame.SetActive(true);
        //Frame.transform.position = Camera.main.WorldToScreenPoint(OperPos);
        Frame.transform.position = RectTransformUtility.WorldToScreenPoint(secondCamera, OperPos);
        //RectTransformUtility.WorldToScreenPoint(secondCamera, OperPos);
    }

    public void UIOff()
    {
        CameraController.instance.RestoreCamera();
        MenuUI.SetActive(false);
        Frame.SetActive(false);
    }


}