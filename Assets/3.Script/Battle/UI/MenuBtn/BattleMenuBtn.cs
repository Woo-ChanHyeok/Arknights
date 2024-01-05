using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class BattleMenuBtn : MonoBehaviour
{
    private Button button;
    [SerializeField] private GameObject battleMenuCanvas;
    private CanvasGroup alpha;
    private PostProcessVolume volume;
    private void Start()
    {
        TryGetComponent(out button);
        volume = Camera.main.GetComponent<PostProcessVolume>();
        alpha = battleMenuCanvas.GetComponent<CanvasGroup>();
        button.onClick.AddListener(ToggleBtn);
    }

    public void ToggleBtn()
    {
        if (!battleMenuCanvas.activeSelf)
        {
            TimeScaleManager.instance.TimeScaleZero();
            battleMenuCanvas.SetActive(true);
            StartCoroutine(SlowChange(alpha, volume));
        }
        else
        {
            TimeScaleManager.instance.TimeScaleCurrent();
            volume.weight = 0;
            battleMenuCanvas.SetActive(false);
        }
    }

    private IEnumerator SlowChange(CanvasGroup alpha, PostProcessVolume volume)
    {
        float elapsedTime = 0f;
        float duration = 0.1f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            alpha.alpha = Mathf.Lerp(0, 1, t);
            volume.weight = Mathf.Lerp(0, 1, t);
            elapsedTime += Time.fixedDeltaTime;
            yield return null;
        }
        alpha.alpha = 1;
        volume.weight = 1;
        yield break;
    }
}
