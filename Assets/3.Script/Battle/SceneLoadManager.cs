using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public static SceneLoadManager instance = null;
    public GameObject[] OffObjInBtl;
    public GameObject[] OnObjOutBtl;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            AsyncLoad("Level0-1");
        }
    }
    public void StartBtlBtn()
    {
        string SceneName = "Level" + StageManager.instance.mapStatus.mapInfo.MapNum;
        AsyncLoad(SceneName);
    }
    public void BackToMainBtn(Scene scene)
    {
        StartCoroutine(BackToMain_co(scene));
        
    }
    private IEnumerator BackToMain_co(Scene scene)
    {
        FadeManager.instance.FadeOut();
        yield return new WaitForSecondsRealtime(1f);
        BGMManager.instance.StopAllBGM();
        BGMManager.instance.isIntro = false;
        BGMManager.currentSceneName = "Home";
        BGMManager.instance.index = 0;
        AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(scene);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        for (int i = 0; i < OnObjOutBtl.Length; i++)
        {
            OnObjOutBtl[i].SetActive(true);
        }
        yield return new WaitForSecondsRealtime(3f);
        BGMManager.instance.PlayBGMintro(0);

        FadeManager.instance.FadeIn();
    }

    public void AsyncLoad(string SceneName)
    {
        Debug.Log("SceneLoadÀü");
        StartCoroutine(SceneLoad_co(SceneName));
    }
    private IEnumerator SceneLoad_co(string SceneName)
    {
        Time.timeScale = 1f;
        FadeManager.isLoad = false;
        FadeManager.instance.FadeOut();
        yield return new WaitForSecondsRealtime(1f);
        BGMManager.instance.StopAllBGM();
        BGMManager.instance.isIntro = false;
        BGMManager.currentSceneName = "0-1";
        BGMManager.instance.index = 1;
        for (int i = 0; i < OffObjInBtl.Length; i++)
        {
            OffObjInBtl[i].SetActive(false);
        }
        yield return new WaitForSecondsRealtime(1f);
        FadeManager.instance.UILoading();
        FadeManager.instance.loadingText.Startco();
        FadeManager.instance.FadeIn();
        yield return new WaitForSecondsRealtime(1f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
        Debug.Log("SceneLoadÁß");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        yield return new WaitForSecondsRealtime(3f);
        FadeManager.instance.loadingText.isCoroutine = false;
        FadeManager.instance.LoadingText.SetActive(false);
        StartCoroutine(FadeManager.instance.ImageFadeOut(FadeManager.instance.LoadingImg));
        yield return new WaitForSecondsRealtime(2f);
        StartCoroutine(FadeManager.instance.ImageFadeOut(FadeManager.instance.LoadedImg));
        StartCoroutine(FadeManager.instance.ImageFadeOut(FadeManager.instance.CornerImg));
        BGMManager.instance.PlayBGMintro(1);
        yield return new WaitForSecondsRealtime(1f);
        StartCoroutine(FadeManager.instance.CanvasFadeOut(FadeManager.instance.TextSpaceCG, null));
        yield return new WaitForSecondsRealtime(1f);
        FadeManager.instance.UIOff();
        FadeManager.isLoad = true;

        yield break;
    }

}
