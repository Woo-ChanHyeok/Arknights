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
        Debug.Log("�巡����");
        //directionSeleter.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 selectorPosition = directionSelector.position;

        // ���콺�� ���� ��ġ�� UI ��ǥ�� ��ȯ
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = directionSelector.position.z; // UI ��ǥ�迡���� z���� �����ؾ� ��

        // UI ��ǥ���� ���ѵ� ��ġ ���
        Vector3 clampedPosition = ClampToCircularBounds(mousePosition, selectorPosition);

        // ���ѵ� ��ġ�� ���� ���ñ� �̵�
        directionSelector.position = clampedPosition;

    }

    public void EndDrag()
    {
        Debug.Log("�巡�׳�");
        directionSelecter.transform.localPosition = new Vector3(0, 30f, 0);
    }
        
    Vector3 ClampToCircularBounds(Vector3 position, Vector3 center)
    {
        // ���� �߽ɿ����� �Ÿ� ���
        float distance = Vector3.Distance(center, position);

        // ���� �Ÿ��� ���������� ũ�ٸ�, ������ ������ ä�� ���ѵ� �Ÿ��� ��ġ ����
        if (distance > 30f)
        {
            return center + (position - center).normalized * 30f;
        }

        // �Ÿ��� ������ �̳����, ���� ��ġ �״�� ��ȯ
        return position;
    }
}
