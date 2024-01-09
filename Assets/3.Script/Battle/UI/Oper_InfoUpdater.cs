using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oper_InfoUpdater : MonoBehaviour
{
    public static Oper_InfoUpdater instance = null;

    private OperInfo operInfo;
    public OperStatus operStatus;

    [SerializeField] private Image  OperImg;
    [SerializeField] private Image  Class_Img;
    [SerializeField] private Text   OperName;
    [SerializeField] private Image  Elite_Icon;
    [SerializeField] private Text   Lv;
    [SerializeField] private Text   AtkPower;
    [SerializeField] private Text   Defense;
    [SerializeField] private Text   MagicRes;
    [SerializeField] private Text   Block;
    [SerializeField] private Slider Slider_HP;
    [SerializeField] private Text   CurrentHP;
    [SerializeField] private Text   Skil_Info;
    [SerializeField] private Text   Skil_Name;
    [SerializeField] private Image  Skil_Icon;


    [SerializeField] private Sprite[] ClassImg;
    [SerializeField] private Sprite[] EliteIcon;
    public Sprite[] ClassIcon;
    private void Awake()
    {
        TryGetComponent(out operStatus);
        if (instance == null)
        {
            instance = this;
            return;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        Slider_HP.onValueChanged.AddListener(delegate { UpdateCurrentHP(); });
    }

    private void Update()
    {
        if (Slider_HP.enabled && operInfo != null && Slider_HP.value != operInfo.CurrentHP)
            Slider_HP.value = operInfo.CurrentHP;
    }

    public void GetOperStatus(OperStatus operStatus)
    {
        this.operStatus = operStatus;
        operInfo = operStatus.operInfo;
    }
    public void UpdateUI()
    {
        OperImg.sprite = operStatus.OperImage[(int)operInfo.OperElite];
        Class_Img.sprite = ClassImg[(int)operInfo.ClassType];
        OperName.text = operInfo.OperName;
        Elite_Icon.sprite = EliteIcon[(int)operInfo.OperElite];
        Lv.text = operInfo.Lv.ToString();
        AtkPower.text = operInfo.AtkPower.ToString();
        Defense.text = operInfo.Defense.ToString();
        MagicRes.text = operInfo.MagicRes.ToString();
        Block.text = operInfo.Block.ToString();
        Slider_HP.maxValue = operInfo.MaxHP;
        Slider_HP.value = operInfo.CurrentHP;
        CurrentHP.text = $"{operInfo.CurrentHP}/{operInfo.MaxHP}";
        //Skil_Icon = operStatus.SkilIcon;
        Skil_Name.text = operInfo.Skil_Name;
        Skil_Info.text = operInfo.Skil_Info;
    }

    public IEnumerator MoveImgSmooth(Vector3 ImgPos, Vector3 targetTransform, float duration)
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

    public void OperImgTransform()
    {
        Vector3 target = new Vector3(-165, 0, 0);
        StartCoroutine(MoveImgSmooth(OperImg.rectTransform.localPosition, target, 0.1f));

    }
    public void OperImgTransformDefault()
    {
        Vector3 target = new Vector3(-35, 0, 0);
        OperImg.rectTransform.localPosition = target;
    }
    public void OperImgAlpha()
    {
        OperImg.color = new Color(OperImg.color.r, OperImg.color.g, OperImg.color.b, 1f);
    }
    public void OperImgAlphaZero()
    {
        OperImg.color = new Color(OperImg.color.r, OperImg.color.g, OperImg.color.b, 0.3f);

    }

    public void UpdateCurrentHP()
    {
        CurrentHP.text = $"{operInfo.CurrentHP}/{operInfo.MaxHP}";
    }
}
