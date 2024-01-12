using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGroupController : MonoBehaviour
{
    public Graphic[] graphics;
    private OperStatus operStatus;
    private bool isChange = false;
    private void Start()
    {
        TryGetComponent(out operStatus);
    }
    private void FixedUpdate()
    {
        CheckCost();
    }
    public void CheckCost()
    {
        if (operStatus.operInfo.CardCost <= CardCostManager.instance.Cost && Map_Information.instance.char_Limit > 0)
        {
            SetColorOrigin();
        }
        else
        {
            SetColorDark();
        }
    }
    public void SetColorDark()
    {
        StopAllCoroutines();
        foreach (Graphic graphic in graphics)
        {
            Debug.Log("setcolorDark");
            if (graphic.color != new Color(0.7f, 0.7f, 0.7f))
            {
                graphic.color = new Color(0.7f, 0.7f, 0.7f);
            }
        }
    }

    public void SetColorOrigin()
    {
        if (isChange)
            return;
        foreach (Graphic graphic in graphics)
        {
            Debug.Log("setcolorOrigin");
            Color startColor = graphic.color;
            Color targetColor = Color.white;
            if (graphic.color != Color.white)
            {
                StartCoroutine(RestoreColor(graphic, startColor, targetColor));
            }
        }
    }
    private IEnumerator RestoreColor(Graphic graphic, Color startColor, Color targetColor)
    {
        isChange = true;
        float elapsedTime = 0f;
        float duration = 0.1f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            graphic.color = Color.Lerp(startColor, targetColor, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        graphic.color = targetColor;
        isChange = false;
        yield break;
    }
}
