using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionComplete : MonoBehaviour
{
    public static MissionComplete instance = null;
    private Transform BG;
    private Transform BG_Shadow;
    private GameObject blockRayCast;
    private Image img;
    private Color originColor;
    private Color disableBtn_Color;
    private float elapsedTime = 0f;
    private float fadeOutTime = 0f;
    private float moveTime = 0.5f;
    private bool isAwake = false;
    private Vector3 BG_TargetPos;
    private Vector3 BG_Shadow_TargetPos;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        TryGetComponent(out img);
        blockRayCast = GameObject.FindWithTag("BlockRay");
        originColor = img.color;
        disableBtn_Color = new Color(200f / 255f, 200f / 255f, 200f / 255f, 200f / 255f);   
        BG = transform.GetChild(1);
        BG_Shadow = transform.GetChild(0);
        BG_TargetPos = new Vector3(1800, BG.localPosition.y, 0);
        BG_Shadow_TargetPos = new Vector3(1750, BG_Shadow.localPosition.y, 0);
        gameObject.SetActive(false);
        blockRayCast.SetActive(false);
        isAwake = true;
    }
    private void OnEnable()
    {
        if (!isAwake)
            return;
        blockRayCast.SetActive(true);
        TimeScaleManager.instance.TimeScale1x();
        TimeScaleBtn.instance.scaleBtn1x();
        TimeScaleBtn.instance.transform.GetChild(0).GetComponentInChildren<Image>().color = disableBtn_Color;
        PauseBtn.instance.transform.GetChild(0).GetComponentInChildren<Image>().color = disableBtn_Color;
        StopAllCoroutines();
        img.color = originColor;
        BG.localPosition = new Vector3(-1800, BG.localPosition.y, 0);    
        BG_Shadow.localPosition = new Vector3(-1750, BG_Shadow.localPosition.y, 0);
        StartCoroutine(MoveBG());
    }

    private IEnumerator MoveBG()
    {
        yield return new WaitForSeconds(0.1f);
        while (elapsedTime < moveTime)
        {
            elapsedTime += Time.deltaTime;
            BG.localPosition = Vector3.Lerp(BG.localPosition, new Vector3(0, BG.localPosition.y, 0), elapsedTime / moveTime);
            BG_Shadow.localPosition = Vector3.Lerp(BG_Shadow.localPosition, new Vector3(0, BG_Shadow.localPosition.y, 0), elapsedTime / moveTime);
            yield return null;
        }
        elapsedTime = 0f;
        yield return new WaitForSeconds(moveTime);
        while (elapsedTime < moveTime)
        {
            elapsedTime += Time.deltaTime;
            BG.localPosition = Vector3.Lerp(BG.localPosition, BG_TargetPos, elapsedTime / moveTime);
            BG_Shadow.localPosition = Vector3.Lerp(BG_Shadow.localPosition, BG_Shadow_TargetPos, elapsedTime / moveTime);
            if(elapsedTime > 0.2f)
            {
                StartCoroutine(FadeOut());
            }
            yield return null;
        }
        elapsedTime = 0f;
        yield break;
    }
    private IEnumerator FadeOut()
    {
        while(fadeOutTime < 0.3f)
        {
            fadeOutTime += Time.deltaTime;
            float alpha = Mathf.Lerp(originColor.a, 0, fadeOutTime / 0.3f);
            img.color = new Color(originColor.r, originColor.g, originColor.b, alpha);
            yield return null;
        }
        fadeOutTime = 0f;
        gameObject.SetActive(false);
        TimeScaleManager.instance.TimeScaleZero();
        ResultManager.instance.Result();
        yield break;
    }
}
