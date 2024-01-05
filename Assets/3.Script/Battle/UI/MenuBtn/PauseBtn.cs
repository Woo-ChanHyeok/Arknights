using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseBtn : MonoBehaviour
{
    private Button button;
    [SerializeField] private GameObject PauseCanvas;
    [SerializeField] Sprite[] sprites;
    private Image pauseImage;
    private void Start()
    {
        TryGetComponent(out button);
        pauseImage = transform.GetChild(0).GetComponent<Image>();
        button.onClick.AddListener(ToggleBtn);
    }

    public void ToggleBtn()
    {
        if (TimeScaleManager.instance.isPause)
        {
            TimeScaleManager.instance.TimeScaleCurrent();
            TimeScaleManager.instance.isPause = false;
            pauseImage.sprite = sprites[0];
            PauseCanvas.SetActive(false);
        }
        else
        {
            TimeScaleManager.instance.TimeScaleZero();
            TimeScaleManager.instance.isPause = true;
            pauseImage.sprite = sprites[1];
            PauseCanvas.SetActive(true);
        }
    }
}
