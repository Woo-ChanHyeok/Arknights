using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public static StageManager instance = null;
    public MapStatus mapStatus;
    public GameObject InformationCanvas;
    public Text SanityText;
    public Text StageNameText;
    public Text StageNumText;
    public Image[] Ranks;
    public Sprite[] RankSprite;
    public GameObject[] GetError;
    public Text DangerrateText;
    public Text InformationText;
    public Button StartBattleBtn;
    public Text SanityCost;

    private void Awake()    
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        StartBattleBtn.onClick.AddListener(StartBattle_Btn);
    }
    public void TurnOnCanvas()
    {
        if (InformationCanvas.activeSelf)
            return;
        InformationCanvas.SetActive(true);
        StartCoroutine(FadeManager.instance.CanvasFadeIn(InformationCanvas.GetComponent<CanvasGroup>()));
    }
    public void TurnOffCanvas()
    {
        if (!InformationCanvas.activeSelf)
            return;
        StartCoroutine(FadeManager.instance.CanvasFadeOut(InformationCanvas.GetComponent<CanvasGroup>(), InformationCanvas));
        //InformationCanvas.SetActive(false);
    }

    public void SyncInformationUI()
    {
        if (mapStatus == null)
            return;

        SanityText.text = $"{PlayerInfoManager.instance.playerInfo.Sanity}/{PlayerInfoManager.instance.playerInfo.MaxSanity}";
        StageNameText.text = mapStatus.mapInfo.MapName;
        StageNumText.text = mapStatus.mapInfo.MapNum;
        DangerrateText.text = mapStatus.mapInfo.Dangerrate;
        InformationText.text = mapStatus.mapInfo.Information;
        SanityCost.text = "-" + mapStatus.mapInfo.SanityCost.ToString();
        if (!mapStatus.mapInfo.isClear)
        {
            GetError[0].SetActive(false);
            GetError[1].SetActive(true);
            for (int i = 0; i < 3; i++)
            {
                Ranks[i].sprite = RankSprite[0];
            }
        }
        else
        {
            GetError[0].SetActive(true);
            GetError[1].SetActive(false);
            Ranks[0].sprite = RankSprite[1];
            Ranks[1].sprite = RankSprite[1];
            Ranks[2].sprite = RankSprite[0];
            if(mapStatus.mapInfo.ClearRank == 3)
            {
                Ranks[2].sprite = RankSprite[1];
            }
        }
    }

    public void StartBattle_Btn()
    {
        //if(PlayerInfoManager.instance.playerInfo.Sanity >= mapStatus.mapInfo.SanityCost)
        //{
        //    SceneManager.LoadSceneAsync("");
        //}
        SquadCavasManager.instance.TurnOnMe();
    }

    public void SaveResult(bool isClear, bool clearRank)
    {
        if(mapStatus.mapInfo.isClear == false)
        {
            mapStatus.mapInfo.isClear = isClear;
        }
        if (!clearRank && mapStatus.mapInfo.ClearRank != 3)
        {
            mapStatus.mapInfo.ClearRank = 2;
        }
        else if (clearRank && mapStatus.mapInfo.ClearRank != 3)
        {
            mapStatus.mapInfo.ClearRank = 3;
        }
        mapStatus.GetComponentInChildren<RankSync>().SyncRank(mapStatus);
    }

}
