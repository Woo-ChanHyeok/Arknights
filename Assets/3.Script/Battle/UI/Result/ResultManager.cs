using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class ResultManager : MonoBehaviour
{
    public static ResultManager instance = null;

    private bool is3Rank = false;
    private bool isFadeComplete = false;
    [SerializeField] private Image fadeImg;
    [SerializeField] private GameObject ResultFrame;
    [SerializeField] private GameObject RankFrame;
    private float fadeTime = 0f;
    private float elapsedTime = 0f;
    [SerializeField] private PostProcessVolume postVolume;
    [SerializeField] private GameObject[] ranks;
    [SerializeField] private GameObject[] particles;
    [SerializeField] private Sprite[] rankImg;
    [SerializeField] private GameObject BackToMain;

    private float operImgFadeTime = 0f;
    [SerializeField] private Image operImg;
    [SerializeField] private GameObject OperatorSpace;
    [SerializeField] private List<OperStatus> operlist = new List<OperStatus>();
    [SerializeField] private List<Sprite> operSprites = new List<Sprite>();
    private Vector3 originScale;
    private Vector3 smallScale;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        postVolume = Camera.main.GetComponent<PostProcessVolume>();
        originScale = new Vector3(1.5f, 1.5f, 1.5f);
        smallScale = new Vector3(0.1f, 0.1f, 0.1f);
        for (int i = 0; i < ranks.Length; i++)
        {
            ranks[i].transform.localScale = originScale;
            ranks[i].GetComponent<Image>().sprite = rankImg[0];
            particles[i].SetActive(false);
        }
        ResultFrame.SetActive(false);
        RankFrame.SetActive(false);
        BackToMain.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Result();
    }

    public void Result()
    {
        TimeScaleManager.instance.TimeScaleZero();
        DecideRank(Map_Information.instance.mapStatus.mapInfo.DefenseHP, Map_Information.instance.defenseHP);
        fadeImg.enabled = true;
        for (int i = 0; i < ranks.Length; i++)
        {
            ranks[i].transform.localScale = originScale;
            ranks[i].GetComponent<Image>().sprite = rankImg[0];
            particles[i].SetActive(false);
        }
        StartCoroutine(FadeOut());
        

    }

    public void Defeat()
    {

    }
    private void GetOperImg()
    {
        for(int i = 0; i < OperatorSpace.transform.childCount; i++)
        {
            operlist.Add(OperatorSpace.transform.GetChild(i).GetComponent<OperStatus>());

            int elite = ((int)operlist[i].operInfo.OperElite);
            operSprites.Add(operlist[i].OperImage[elite]);
        }
        int random = Random.Range(0, operSprites.Count);
        operImg.sprite = operSprites[random];
    }
    private IEnumerator FadeInOperImg()
    {
        operImg.enabled = true;
        while(operImgFadeTime < 0.5f)
        {
            operImgFadeTime += Time.fixedDeltaTime;
            float alpha = Mathf.Lerp(0, 1, operImgFadeTime / 0.5f);
            operImg.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        operImgFadeTime = 0f;
        yield break;
    }
    private IEnumerator FadeOut()
    {
        Time.timeScale = 0f;
        fadeImg.color = Color.clear;
        while (fadeTime < 0.3f)
        {
            float alpha = Mathf.Lerp(0, 1, fadeTime / 0.3f);
            fadeTime += Time.fixedDeltaTime;
            fadeImg.color = new Color(0, 0, 0, alpha);
            yield return null;            
        }
        GetOperImg();
        fadeImg.color = Color.black;
        fadeTime = 0f;
        postVolume.weight = 1f;
        ResultFrame.SetActive(true);
        RankFrame.SetActive(true);
        StartCoroutine(FadeIn());
        yield break;
    }
    private IEnumerator FadeIn()
    {
        yield return new WaitForSecondsRealtime(2f);
        fadeImg.color = Color.black;
        StartCoroutine(FadeInOperImg());
        while (fadeTime < 0.3f)
        {
            float alpha = Mathf.Lerp(1, 0, fadeTime / 0.3f);
            fadeTime += Time.fixedDeltaTime;
            fadeImg.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        fadeImg.color = Color.clear;
        fadeImg.enabled = false;
        fadeTime = 0f;
        isFadeComplete = true;
        PlayerInfoManager.instance.playerInfo.Sanity -= Map_Information.instance.mapStatus.mapInfo.SanityCost;
        StageManager.instance.SaveResult(true, is3Rank);
        StageManager.instance.SyncInformationUI();
        StartCoroutine(Rank(is3Rank));
        BackToMain.SetActive(true);
        yield break;
    }

    private void DecideRank(int max, int current)
    {
        if (max == current) // 3Rank
        {
            is3Rank = true;
            return;
        }
        else                // 2Rank
        {
            is3Rank = false;
            return;
        }
    }
    private IEnumerator Rank(bool is3Rank)
    {
        if (!is3Rank)
        {
            StartCoroutine(rankSize(0));
            yield return new WaitForSecondsRealtime(0.5f);
            StartCoroutine(rankSize(1));
        }
        else
        {
            StartCoroutine(rankSize(0));
            yield return new WaitForSecondsRealtime(0.5f);
            StartCoroutine(rankSize(1));
            yield return new WaitForSecondsRealtime(0.5f);
            StartCoroutine(rankSize(2));
        }
        
        yield break;
    }

    private IEnumerator rankSize(int i)
    {
        ranks[i].transform.localScale = smallScale;
        ranks[i].GetComponent<Image>().sprite = rankImg[1];
        particles[i].SetActive(true);
        while(elapsedTime < 0.2f)
        {
            ranks[i].transform.localScale = Vector3.Lerp(smallScale, originScale, elapsedTime / 0.2f);
            elapsedTime += Time.fixedDeltaTime;
            yield return null;
        }
        elapsedTime = 0f;
        ranks[i].transform.localScale = originScale;
        yield break;
    }


}
