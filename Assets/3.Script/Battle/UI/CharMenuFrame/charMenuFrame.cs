using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class charMenuFrame : MonoBehaviour
{
    public static charMenuFrame instance = null;

    public MouseAction mouseAction;

    [SerializeField] private GameObject MenuUI;
    [SerializeField] private GameObject Frame;

    [SerializeField] private RectTransform selecterRect;

    [SerializeField] private GameObject followImgParent;

    [SerializeField] private GameObject Cancel_Btn;
    [SerializeField] private GameObject DragBack_Img;

    [SerializeField] private Image Left;
    [SerializeField] private Image Right;
    [SerializeField] private Image Up;
    [SerializeField] private Image Down;

    public bool isLeft = false;
    public bool isRight = false;
    public bool isUp = false;
    public bool isDown = false;
    public bool isCenter = true;
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

    public void SetPos(Vector3 OperPos)
    {
        Frame.SetActive(true);
        Frame.transform.position = Camera.main.WorldToScreenPoint(OperPos);
        DirectionImg("");
        TurnOnCancel_Btn();
    }
    public void OffFrame()
    {
        Frame.SetActive(false);
    }

    public void BeginDrag()
    {
        TurnOnDragBack_Img();
    }

    public void OnDrag()
    {
        Debug.Log("드래그중");

        Vector3 mousePosition = Input.mousePosition;
        selecterRect.localPosition = new Vector3(selecterRect.localPosition.x, selecterRect.localPosition.y, 0f);
        mousePosition.z = selecterRect.position.z;

        Vector3 clampedPosition = ClampToCircularBounds(mousePosition);

        selecterRect.position = clampedPosition;

        Vector3 center = Frame.transform.position;
        float distance = Vector3.Distance(center, clampedPosition);
        if(distance > 100f)
        {
            float distanceX = Mathf.Abs(center.x - clampedPosition.x);
            float distanceY = Mathf.Abs(center.y - clampedPosition.y);

            if (distanceX > distanceY) // 수평 거리가 수직 거리보다 크면
            {
                if (clampedPosition.x > center.x)
                {
                    DirectionImg("Right");
                }
                else
                {
                    DirectionImg("Left");
                }
            }
            else // 수직 거리가 수평 거리보다 크면
            {
                if (clampedPosition.y > center.y)
                {
                    DirectionImg("Up");
                }
                else
                {
                    DirectionImg("Down");
                }
            }
        }
        else
        {
            DirectionImg("");
        }

    }

    public void EndDrag()
    {
        if (!isCenter)
        {
            mouseAction.instantiateOper();
            UIOff();
        }
        Debug.Log("드래그끝");
        selecterRect.localPosition = new Vector3(0, 30f, 0);
        
        TurnOnCancel_Btn();
        DirectionImg("");
    }
        
    private Vector3 ClampToCircularBounds(Vector3 position)
    {
        Vector3 center = Frame.transform.position;

        float distance = Vector3.Distance(center, position);

        if (distance > 300f)
        {
            return center + (position - center).normalized * 300f;
        }

        return position;
    }

    public void CancelBtn()
    {
        Debug.Log("Cancel!");
        followImgParent = GameObject.Find("FollowImgParent");
        Destroy(followImgParent.transform.GetChild(0).gameObject);

        UIOff();
        
    }

    private void UIOff()
    {
        CameraController.instance.RestoreCamera();
        TurnOnCancel_Btn();
        DirectionImg("");
        MenuUI.SetActive(false);
        Frame.SetActive(false);
        mouseAction.IconPosDown();
    }

    private void TurnOnCancel_Btn()
    {
        Cancel_Btn.SetActive(true);
        DragBack_Img.SetActive(false);
    }
    private void TurnOnDragBack_Img()
    {
        Cancel_Btn.SetActive(false);
        DragBack_Img.SetActive(true);
    }

    private void DirectionImg(string direction)
    {
        if (direction =="Left")
        {
            Left.enabled = true;
            Right.enabled = false;
            Up.enabled = false;
            Down.enabled = false;

            isLeft = true;
            isRight = false;
            isUp = false;
            isDown = false;
            isCenter = false;
        }
        else if (direction == "Right")
        {
            Left.enabled = false;
            Right.enabled = true;
            Up.enabled = false;
            Down.enabled = false;

            isLeft = false;
            isRight = true;
            isUp = false;
            isDown = false;
            isCenter = false;
        }
        else if (direction == "Up")
        {
            Left.enabled = false;
            Right.enabled = false;
            Up.enabled = true;
            Down.enabled = false;

            isLeft = false;
            isRight = false;
            isUp = true;
            isDown = false;
            isCenter = false;
        }
        else if (direction == "Down")
        {
            Left.enabled = false;
            Right.enabled = false;
            Up.enabled = false;
            Down.enabled = true;

            isLeft = false;
            isRight = false;
            isUp = false;
            isDown = true;
            isCenter = false;
        }
        else
        {
            Left.enabled = false;
            Right.enabled = false;
            Up.enabled = false;
            Down.enabled = false;

            isLeft = false;
            isRight = false;
            isUp = false;
            isDown = false;
            isCenter = true;
        }
    }

    //public void instantiateOper(GameObject Oper, Transform parent)
    //{
    //    Instantiate(Oper, Oper.transform.position, Quaternion.identity, parent);
    //    Instantiate(mouseAction., Oper.transform.position, Quaternion.identity, parent);
    //}



}
