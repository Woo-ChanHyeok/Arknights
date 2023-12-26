using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charMenuFrame : MonoBehaviour
{
    public static charMenuFrame instance = null;


    [SerializeField] private GameObject Frame;
    [SerializeField] private GameObject directionSelecter;
    [SerializeField] private RectTransform directionSelector;
    [SerializeField] private BoxCollider boxCollider;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        //OperPos = transform.parent.parent.transform.position;
        //gameObject.transform.position = Camera.main.WorldToScreenPoint(OperPos);
        //Frame = gameObject.GetComponentInChildren<GameObject>();
    }

    public void SetPos(Vector3 OperPos)
    {
        Frame.SetActive(true);
        Frame.transform.position = Camera.main.WorldToScreenPoint(OperPos);
    }
    public void OffFrame()
    {
        Frame.SetActive(false);
    }

    public void BeginDrag()
    {
        //directionSeleter.SetActive(true);
    }

    public void OnDrag()
    {
        Debug.Log("드래그중");
        //directionSeleter.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 selectorPosition = directionSelector.position;

        // 마우스의 현재 위치를 UI 좌표로 변환
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = directionSelector.position.z; // UI 좌표계에서는 z값을 유지해야 함

        // UI 좌표에서 제한된 위치 계산
        Vector3 clampedPosition = ClampToCircularBounds(mousePosition, selectorPosition);

        // 제한된 위치로 방향 선택기 이동
        directionSelector.position = clampedPosition;

    }

    public void EndDrag()
    {
        Debug.Log("드래그끝");
        directionSelecter.transform.localPosition = new Vector3(0, 30f, 0);
    }
        
    Vector3 ClampToCircularBounds(Vector3 position, Vector3 center)
    {
        // 원의 중심에서의 거리 계산
        float distance = Vector3.Distance(center, position);

        // 만약 거리가 반지름보다 크다면, 방향을 유지한 채로 제한된 거리로 위치 설정
        if (distance > 30f)
        {
            return center + (position - center).normalized * 30f;
        }

        // 거리가 반지름 이내라면, 현재 위치 그대로 반환
        return position;
    }
}
