using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

public class MouseAction : MonoBehaviour
{
    [SerializeField] private GameObject prefab; //마우스 따라다닐 이미지
    [SerializeField] private Transform followImgParent;

    private OperStatus operStat;
    private Respawn_UI respawn_UI;

    [SerializeField] private GameObject InstantiatePrefabs;
    [SerializeField] private Transform InstantiateParent;
    //[SerializeField] private SkeletonDataAsset[] skelData;

    [SerializeField] private RectTransform IconRect;
    [SerializeField] private Image OperIcon;
    [SerializeField] private Sprite[] OperIcon_img;
    [SerializeField] private GameObject OperInfo; //UI

    [SerializeField] private Image OperImg;
    [SerializeField] private Image ChosenImg;

    private GameObject Oper; //FollowImg용

    private bool isDrag = false;

    [SerializeField] public static bool isHoldOper = false;

    private int MapLayer;
    private string FloorTag = "Floor";
    private string UpperTag = "UpFloor";
    public float mouseOverThreshold = 0.5f;
    private Plane plane;//마우스 따라오는 이미지용


    private void Start()
    {
        TryGetComponent(out operStat);
        TryGetComponent(out IconRect);
        TryGetComponent(out respawn_UI);
        MapLayer = 1 << LayerMask.NameToLayer("Map");
        plane = new Plane(Vector3.up, new Vector3(0, 0.23f, 0));
        followImgParent = GameObject.Find("FollowImgParent").transform;
        OperInfo = Oper_InfoUpdater.instance.InfoUI;

        DecideIcon_img();
    }

    private void Update()
    {
        if (Oper_InfoUpdater.instance.operStatus == operStat && OperInfo.activeSelf)
        {
            IconPosUp();
        }
        else
        {
            IconPosDown();
        }
    }

    public void OnPointerDown()
    {
        Debug.Log("눌렀니?");

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
            Debug.Log("냐아악");
            OperStatus CurrentStatus = Oper_InfoUpdater.instance.operStatus;
            Oper_InfoUpdater.instance.GetOperStatus(gameObject.GetComponent<OperStatus>());
            Oper_InfoUpdater.instance.UpdateUI();
            Oper_InfoUpdater.instance.OperImgTransformDefault();
            Oper_InfoUpdater.instance.OperImgAlpha();

            if (!OperInfo.activeSelf)
            {
                //Oper_InfoUpdater.instance.GetOperStatus(gameObject.GetComponent<OperStatus>());
                //Oper_InfoUpdater.instance.UpdateUI();
                CameraController.instance.TiltCamera();
                OperInfo.SetActive(true);
            }
            else if (OperInfo.activeSelf && Oper_InfoUpdater.instance.operStatus == CurrentStatus)
            {
                CameraController.instance.RestoreCamera();
                OperInfo.SetActive(false);
            }
            Debug.Log("뗐니?");
        }
        isDrag = false;
        PlaceOperFrame.instance.Frame.SetActive(false);
    }

    public void OnBeginDrag()
    {
        isDrag = true;
        if (operStat.operInfo.CardCost > CardCostManager.instance.Cost || Map_Information.instance.char_Limit <= 0)
            return;
        else
        {
            if (respawn_UI.RespawnTime <= 0)
            {
                Debug.Log("드래그시작");
                CharMenuFrame.instance.mouseAction = this;
                PlaceOperFrame.instance.UIOff();
                GameObject oper = Instantiate(prefab, followImgParent);
                //oper.transform.parent = prefabParent;
                oper.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                Oper = oper;

                isDrag = true;
                Oper_InfoUpdater.instance.GetOperStatus(gameObject.GetComponent<OperStatus>());
                Oper_InfoUpdater.instance.UpdateUI();

                Oper_InfoUpdater.instance.OperImgAlphaZero();
                Oper_InfoUpdater.instance.OperImgTransform();


                CameraController.instance.TiltCamera();
                OperInfo.SetActive(true);
            }
        }
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
                Vector3 platformCenter = hit.collider.bounds.center;

                Oper.transform.position = hit.collider.transform.position + new Vector3(0, 0, -0.3f);
                isHoldOper = true;
                InstantiateParent = hit.collider.transform;
            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, MapLayer) && hit.collider.CompareTag(FloorTag) && gameObject.GetComponent<OperStatus>().operInfo.OperType.Equals(PositionType.Floor))
            {
                Debug.Log("FloorTag!");
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
                Oper_InfoUpdater.instance.OperImgAlpha();
                CameraController.instance.RestoreCamera();
                Oper_InfoUpdater.instance.OperImgTransformDefault();
                OperInfo.SetActive(false);
                Destroy(Oper);

            }
            else if (isHoldOper)
            {
                CharMenuFrame.instance.SetPos(Oper.transform.position);
            }
            MatColorSetter.instance.SetFloorZero();
            MatColorSetter.instance.SetUpperFloorZero();
        }
        isDrag = false;
    }

    public void IconPosUp()
    {
        //IconRect.sizeDelta = new Vector2(IconRect.sizeDelta.x, 200f);
        //IconRect.transform.position = new Vector3(IconRect.transform.position.x, 107.5f, IconRect.transform.position.z);
        IconRect.transform.localPosition = new Vector3(IconRect.transform.localPosition.x, 12.5f, IconRect.transform.localPosition.z);
        ChosenImg.enabled = true;
    }
    public void IconPosDown()
    {
        //IconRect.sizeDelta = new Vector2(IconRect.sizeDelta.x, 155f);
        //IconRect.transform.position = new Vector3(IconRect.transform.position.x, 77.5f, IconRect.transform.position.z);
        IconRect.transform.localPosition = new Vector3(IconRect.transform.localPosition.x, -12.5f, IconRect.transform.localPosition.z);
        ChosenImg.enabled = false;
    }

    public void instantiateOper()
    {
        GameObject Operator = Instantiate(InstantiatePrefabs, InstantiateParent.position, new Quaternion(0,0,0,0) , InstantiateParent);
        Operator.transform.localPosition += new Vector3(0, 0.5f, 0);
        
        GameObject frame = Operator.transform.GetChild(0).gameObject;
        SkeletonAnimation skelAni = Operator.GetComponentInChildren<SkeletonAnimation>();
        OperSkeletonData skelData = Operator.GetComponentInChildren<OperSkeletonData>();
        AtkRange atkRange = Operator.GetComponentInChildren<AtkRange>();
        SkeletonDataAsset Front = skelData.FrontAsset;
        SkeletonDataAsset Back = skelData.BackAsset;

        if (CharMenuFrame.instance.isLeft)
        {
            frame.transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);
            skelAni.gameObject.transform.localScale = new Vector3(-0.27f, 0.27f, 0.27f);
            //atkRange.gameObject.transform.localRotation = Quaternion.Euler(53.5f, 0, 0);
        }
        else if (CharMenuFrame.instance.isRight)
        {
            frame.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            skelAni.gameObject.transform.localScale = new Vector3(0.27f, 0.27f, 0.27f);
            //atkRange.gameObject.transform.localRotation = Quaternion.Euler(53.5f, 0, 180);
        }
        else if (CharMenuFrame.instance.isUp)
        {
            frame.transform.localRotation = Quaternion.Euler(0, 0, 90f);
            skelAni.skeletonDataAsset = Back;
            skelAni.AnimationState.ClearTracks();
            skelAni.Initialize(true);
            atkRange.gameObject.transform.localRotation = Quaternion.Euler(53.5f, 0, 90);
        }
        else if (CharMenuFrame.instance.isDown)
        {
            frame.transform.localRotation = Quaternion.Euler(0, 0, -90f);
            atkRange.gameObject.transform.localRotation = Quaternion.Euler(53.5f, 0, -90);
        }
        Operator.GetComponent<EscapeOperator>().Oper_UI = gameObject;
        gameObject.SetActive(false);
        isDrag = false;
        isHoldOper = false;
        CardCostManager.instance.Cost -= operStat.operInfo.CardCost;
        Map_Information.instance.char_Limit--;
        Map_Information.instance.UpdateInfo();
        Destroy(Oper);
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
