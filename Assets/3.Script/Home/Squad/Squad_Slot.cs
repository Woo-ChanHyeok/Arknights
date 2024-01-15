using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Squad_Slot : MonoBehaviour
{
    private Button btn;
    public OperatorLineUp myOper;
    public OperStatus operStatus;

    public Image SelectedImg;
    public Image BG;
    public Sprite[] BGs;
    public Image OperImg;
    public Image UpperHub;
    public Sprite[] UpperHubs;
    public Image Light;
    public Sprite[] Lights;
    public Image LowerHub;
    public Sprite[] LowerHubs;
    public GameObject Stars;
    public Image Profession;
    public Sprite[] Professions;
    public Text Name_T;
    public GameObject Elite_Hub;
    public Text Lv;


    private void Awake()
    {
        TryGetComponent(out btn);
        TryGetComponent(out operStatus);

        transform.position = transform.parent.position;

        //btn.onClick.AddListener(SquadBtn);
        BG = transform.Find("BG").GetComponent<Image>();
        OperImg = transform.Find("OperImg").GetComponent<Image>();
        UpperHub = transform.Find("UpperHub").GetComponent<Image>();
        Light = transform.Find("Light").GetComponent<Image>();
        LowerHub = transform.Find("LowerHub").GetComponent<Image>();
        Stars = transform.Find("Star").gameObject;
        Profession = transform.Find("Profession").GetComponent<Image>();
        Name_T = transform.Find("Name_Text").GetComponent<Text>();
        Elite_Hub = transform.Find("EliteHub").gameObject;
        Lv = transform.Find("Lv").GetComponentInChildren<Text>();
        SelectedImg = transform.Find("SelectedImage")?.GetComponent<Image>();
    }
    private void OnEnable()
    {
        //UpdateInfo();
    }
    private void Update()
    {
        IsSelect();
    }
    public void IsSelect()
    {
        if(SelectedImg == null)
        {
            return;
        }
        if(SquadManager.Selected == gameObject)
        {
            SelectedImg.enabled = true;
        }
        else
        {
            SelectedImg.enabled = false;
        }
    }

    public void UpdateInfo()
    {
        if (operStatus == null)
            return;
        Name_T.text = operStatus.operInfo.OperName;
        Lv.text = operStatus.operInfo.Lv.ToString();
        OperImg.sprite = operStatus.OperCardImage[(int)operStatus.operInfo.OperElite];
        if (operStatus.operInfo.OperElite != EliteType.Default)
        {
            Elite_Hub.transform.GetChild((int)operStatus.operInfo.OperElite - 1).gameObject.SetActive(true);
        }
        if (operStatus.operInfo.ClassType == OperClass.Vanguard)
        {
            Profession.sprite = Professions[0];
        }
        else if (operStatus.operInfo.ClassType == OperClass.Guard)
        {
            Profession.sprite = Professions[1];
        }
        else if (operStatus.operInfo.ClassType == OperClass.Defender)
        {
            Profession.sprite = Professions[2];
        }
        else if (operStatus.operInfo.ClassType == OperClass.Sniper)
        {
            Profession.sprite = Professions[3];
        }
        else if (operStatus.operInfo.ClassType == OperClass.Caster)
        {
            Profession.sprite = Professions[4];
        }
        else if (operStatus.operInfo.ClassType == OperClass.Medic)
        {
            Profession.sprite = Professions[5];
        }
        else if (operStatus.operInfo.ClassType == OperClass.Supporter)
        {
            Profession.sprite = Professions[6];
        }
        else if (operStatus.operInfo.ClassType == OperClass.Specialist)
        {
            Profession.sprite = Professions[7];
        }



        if (operStatus.operInfo.OperStar == Star.Star1)
        {
            BG.sprite = BGs[0];
            UpperHub.sprite = UpperHubs[0];
            Light.sprite = Lights[0];
            LowerHub.sprite = LowerHubs[0];
            Stars.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (operStatus.operInfo.OperStar == Star.Star2)
        {
            BG.sprite = BGs[0];
            UpperHub.sprite = UpperHubs[1];
            Light.sprite = Lights[1];
            LowerHub.sprite = LowerHubs[0];
            Stars.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (operStatus.operInfo.OperStar == Star.Star3)
        {
            BG.sprite = BGs[0];
            UpperHub.sprite = UpperHubs[2];
            Light.sprite = Lights[2];
            LowerHub.sprite = LowerHubs[0];
            Stars.transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (operStatus.operInfo.OperStar == Star.Star4)
        {
            BG.sprite = BGs[1];
            UpperHub.sprite = UpperHubs[3];
            Light.sprite = Lights[3];
            LowerHub.sprite = LowerHubs[1];
            Stars.transform.GetChild(3).gameObject.SetActive(true);
        }
        else if (operStatus.operInfo.OperStar == Star.Star5)
        {
            BG.sprite = BGs[2];
            UpperHub.sprite = UpperHubs[4];
            Light.sprite = Lights[4];
            LowerHub.sprite = LowerHubs[2];
            Stars.transform.GetChild(4).gameObject.SetActive(true);
        }
        else if (operStatus.operInfo.OperStar == Star.Star6)
        {
            BG.sprite = BGs[3];
            UpperHub.sprite = UpperHubs[5];
            Light.sprite = Lights[5];
            LowerHub.sprite = LowerHubs[3];
            Stars.transform.GetChild(5).gameObject.SetActive(true);
        }

    }
}
