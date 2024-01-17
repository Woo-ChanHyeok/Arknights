using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager instance = null;
    public static bool isLoad = false;
    public GameObject CombatCanvas;
    public Image FadeImg;
    public Image LoadingImg;
    public Image LoadedImg;
    public Image CornerImg;
    public GameObject LoadingText;
    public LoadingText loadingText;
    public GameObject TextSpace;
    public CanvasGroup TextSpaceCG;
    public Text MapNum_Text;
    public Text MapName_Text;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        UIOff();
        FadeIn();
    }

    public void UILoading()
    {
        LoadingImg.gameObject.SetActive(true);
        LoadedImg.gameObject.SetActive(true);
        CornerImg.gameObject.SetActive(true);
        TextSpace.SetActive(true);
        LoadingText.SetActive(true);
        LoadingImg.color = Color.white;
        LoadedImg.color = Color.white;
        CornerImg.color = Color.white;
        TextSpaceCG.alpha = 1f;
        MapNum_Text.text = StageManager.instance.mapStatus.mapInfo.MapNum;
        MapName_Text.text = StageManager.instance.mapStatus.mapInfo.MapName;
    }
    public void UIOff()
    {
        FadeImg.gameObject.SetActive(false);
        LoadingImg.gameObject.SetActive(false);
        LoadedImg.gameObject.SetActive(false);
        CornerImg.gameObject.SetActive(false);
        TextSpace.SetActive(false);
        LoadingText.SetActive(false);
    }
    public IEnumerator ImageFadeOut(Image img)
    {
        img.color = Color.white;
        float elapsedTime = 0f;
        while (elapsedTime < 0.3f)
        {
            img.color = Color.Lerp(Color.white, Color.clear, elapsedTime / 0.3f);
            elapsedTime += Time.fixedDeltaTime;
            yield return null;
        }
        img.color = Color.clear;

        yield break;
    }



    public IEnumerator CanvasFadeIn(CanvasGroup alpha)
    {
        float elapsedTime = 0f;
        alpha.alpha = 0;
        while (elapsedTime < 0.5f)
        {
            alpha.alpha = Mathf.Lerp(0, 1, elapsedTime / 0.5f);
            elapsedTime += Time.fixedDeltaTime;
            yield return null;
        }
        alpha.alpha = 1f;
        yield break;
    }
    public IEnumerator CanvasFadeOut(CanvasGroup alpha, GameObject SetActive)
    {
        float elapsedTime = 0f;
        alpha.alpha = 1;
        while (elapsedTime < 0.5f)
        {
            alpha.alpha = Mathf.Lerp(1, 0, elapsedTime / 0.5f);
            elapsedTime += Time.fixedDeltaTime;
            yield return null;
        }
        alpha.alpha = 0f;
        if (SetActive != null)
        {
            SetActive.SetActive(false);
        }
        yield break;
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInOut(true));
    }
    public void FadeOut()
    {
        StartCoroutine(FadeInOut(false));
    }
    private IEnumerator FadeInOut(bool In)
    {
        float elapsedTime = 0f;
        FadeImg.gameObject.SetActive(true);
        if (In)
        {
            FadeImg.color = Color.black;
            while (elapsedTime < 1f)
            {
                FadeImg.color = Color.Lerp(Color.black, Color.clear, elapsedTime / 1f);
                elapsedTime += Time.fixedDeltaTime;
                yield return null;
            }
            FadeImg.color = Color.clear;
            FadeImg.gameObject.SetActive(false);
        }
        else
        {
            FadeImg.color = Color.clear;
            while (elapsedTime < 0.5f)
            {

                FadeImg.color = Color.Lerp(Color.clear, Color.black, elapsedTime / 0.5f);
                elapsedTime += Time.fixedDeltaTime;
                yield return null;
            }
            FadeImg.color = Color.black;
        }
        elapsedTime = 0f;
        yield break;
    }




    public void TurnOnCombatCanvas()
    {
        CombatCanvas.SetActive(true);
        StartCoroutine(CanvasFadeIn(CombatCanvas.GetComponent<CanvasGroup>()));
    }
    public void TurnOffCombatCanvas()
    {
        StartCoroutine(CanvasFadeOut(CombatCanvas.GetComponent<CanvasGroup>(), CombatCanvas));
    }
}
