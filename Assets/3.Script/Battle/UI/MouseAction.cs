using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

public class MouseAction : MonoBehaviour
{
    [SerializeField] private GameObject prefab; //마우스 따라다닐 이미지
    [SerializeField] private RectTransform prefabParent; // 프리팹 부모용

    private OperStatus operStat;

    [SerializeField] private GameObject InstantiatePrefabs;
    [SerializeField] private Transform InstantiateParent;
    //[SerializeField] private SkeletonDataAsset[] skelData;

    [SerializeField] private RectTransform IconRect;
    [SerializeField] private Image OperIcon;
    [SerializeField] private Sprite[] OperIcon_img;
    [SerializeField] private GameObject OperInfo; //UI

    [SerializeField] private Image OperImg;

    private GameObject Oper; //FollowImg용

    private bool isDrag = false;

    [SerializeField] private bool isHoldOper = false;

    private int MapLayer;
    private string FloorTag = "Floor";
    private string UpperTag = "UpFloor";
    public float mouseOverThreshold = 0.5f;
    private Plane plane;//마우스 따라오는 이미지용


    private void Start()
    {
        TryGetComponent(out operStat);
        TryGetComponent(out IconRect);
        MapLayer = 1 << LayerMask.NameToLayer("Map");
        plane = new Plane(Vector3.up, new Vector3(0, 0.23f, 0));



        DecideIcon_img();
    }

    private void Update()
    {
    }

    public void OnPointerDown()
    {
        Debug.Log("눌렀니?");
        //Oper_InfoUpdater.instance.GetOperStatus(gameObject.GetComponent<OperStatus>());
        //Oper_InfoUpdater.instance.UpdateUI();

        isHoldOper = false;

    }

    public void OnPointerUp()
    {
        if (isDrag)
            return;
        else if (isHoldOper)
            return;

        else
        {


            OperStatus CurrentStatus = Oper_InfoUpdater.instance.operStatus;
            Oper_InfoUpdater.instance.GetOperStatus(gameObject.GetComponent<OperStatus>());
            Oper_InfoUpdater.instance.UpdateUI();

            if (!OperInfo.activeSelf)
            {
                //Oper_InfoUpdater.instance.GetOperStatus(gameObject.GetComponent<OperStatus>());
                //Oper_InfoUpdater.instance.UpdateUI();
                IconPosUp();
                CameraController.instance.TiltCamera();
                OperInfo.SetActive(true);
            }
            else if (OperInfo.activeSelf && Oper_InfoUpdater.instance.operStatus == CurrentStatus)
            {
                IconPosDown();
                CameraController.instance.RestoreCamera();
                OperInfo.SetActive(false);
            }
            Debug.Log("뗐니?");
        }
    }

    public void OnBeginDrag()
    {
        Debug.Log("드래그시작");
        charMenuFrame.instance.mouseAction = this;

        GameObject oper = Instantiate(prefab);
        oper.transform.parent = prefabParent;
        oper.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Oper = oper;

        isDrag = true;
        Oper_InfoUpdater.instance.GetOperStatus(gameObject.GetComponent<OperStatus>());
        Oper_InfoUpdater.instance.UpdateUI();

        OperImgAlphaZero();
        OperImgTransform();

        IconPosUp();
        CameraController.instance.TiltCamera();
        OperInfo.SetActive(true);
    }

    public void OnDrag()
    {
        Debug.Log("드래그중");
        if (Oper != null)
        {
            if (gameObject.GetComponent<OperStatus>().operInfo.OperType.Equals(PositionType.Upper))
            {
                MatColorSetter.instance.SetUpperFloorGreen();

            }
            else if (gameObject.GetComponent<OperStatus>().operInfo.OperType.Equals(PositionType.Floor))
            {
                MatColorSetter.instance.SetFloorGreen();

            }


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, MapLayer) && hit.collider.CompareTag(UpperTag) && gameObject.GetComponent<OperStatus>().operInfo.OperType.Equals(PositionType.Upper))
            {
                Debug.Log("UpperTag!");
                // 마우스가 발판 위에 있을 때
                Vector3 platformCenter = hit.collider.bounds.center;

                Oper.transform.position = hit.collider.transform.position + new Vector3(0, 0, -0.3f);
                isHoldOper = true;
                InstantiateParent = hit.collider.transform;
            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, MapLayer) && hit.collider.CompareTag(FloorTag) && gameObject.GetComponent<OperStatus>().operInfo.OperType.Equals(PositionType.Floor))
            {
                Debug.Log("FloorTag!");
                // 마우스가 발판 위에 있을 때
                Vector3 platformCenter = hit.collider.bounds.center;

                Oper.transform.position = hit.collider.transform.position + new Vector3(0, 0, -0.2f);
                isHoldOper = true;
                InstantiateParent = hit.collider.transform;
            }
            else
            {
                isHoldOper = false;
                if (plane.Raycast(ray, out float distance))
                {
                    Vector3 hitPoint = ray.GetPoint(distance);
                    Oper.transform.position = hitPoint;
                }
            }
        }



    }

    public void OnEndDrag()
    {
        if (isDrag)
        {
            Debug.Log("드래그 뗌");
            if (Oper != null && !isHoldOper)
            {
                isDrag = false;
                OperImgAlpha();
                CameraController.instance.RestoreCamera();
                OperImgTransformDefault();
                OperInfo.SetActive(false);
                Destroy(Oper);
                IconPosDown();

            }
            else if (isHoldOper)
            {
                charMenuFrame.instance.SetPos(Oper.transform.position);
            }
            MatColorSetter.instance.SetFloorZero();
            MatColorSetter.instance.SetUpperFloorZero();
        }
        else
        {
            IconPosDown();
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
    public void IconPosUp()
    {
        //IconRect.sizeDelta = new Vector2(IconRect.sizeDelta.x, 200f);
        Debug.Log(IconRect.transform.position);
        IconRect.transform.position = new Vector3(IconRect.transform.position.x, 107.5f, IconRect.transform.position.z);
    }
    public void IconPosDown()
    {
        //IconRect.sizeDelta = new Vector2(IconRect.sizeDelta.x, 155f);
        IconRect.transform.position = new Vector3(IconRect.transform.position.x, 77.5f, IconRect.transform.position.z);
    }

    public void instantiateOper()
    {
        GameObject Operator = Instantiate(InstantiatePrefabs, InstantiateParent.position, new Quaternion(0,0,0,0) , InstantiateParent);
        Operator.transform.localPosition += new Vector3(0, 0.5f, 0);
        
        GameObject frame = Operator.transform.GetChild(0).gameObject;
        //GameObject realOper = GetComponentInChildren<GameObject>();
        SkeletonAnimation skelAni = Operator.GetComponentInChildren<SkeletonAnimation>();
        OperSkeletonData skelData = Operator.GetComponentInChildren<OperSkeletonData>();
        SkeletonDataAsset Front = skelData.FrontAsset;
        SkeletonDataAsset Back = skelData.BackAsset;

        if (charMenuFrame.instance.isLeft)
        {
            frame.transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);
            skelAni.gameObject.transform.localScale = new Vector3(-0.27f, 0.27f, 0.27f);
        }
        else if (charMenuFrame.instance.isRight)
        {
            frame.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            skelAni.gameObject.transform.localScale = new Vector3(0.27f, 0.27f, 0.27f);
        }
        else if (charMenuFrame.instance.isUp)
        {
            frame.transform.localRotation = Quaternion.Euler(0, 0, 90f);
            skelAni.skeletonDataAsset = Back;
            skelAni.AnimationState.ClearTracks();
            skelAni.Initialize(true);
            skelAni.AnimationState.SetAnimation(0, "Start", false);
            skelAni.AnimationState.AddAnimation(0, "Idle", true, 0);
        }
        else if (charMenuFrame.instance.isDown)
        {
            frame.transform.localRotation = Quaternion.Euler(0, 0, -90f);
        }
        Destroy(Oper);
        IconPosDown();

    }

    private void DecideIcon_img()
    {
        if (operStat.operInfo.OperElite.Equals(EliteType.Default))
        {
            OperIcon.sprite = OperIcon_img[0];
        }
        else if (operStat.operInfo.OperElite.Equals(EliteType.Elite1))
        {
            OperIcon.sprite = OperIcon_img[1];
        }
        else if (operStat.operInfo.OperElite.Equals(EliteType.Elite2))
        {
            OperIcon.sprite = OperIcon_img[2];

        }
    }

}
