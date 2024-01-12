using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScaleBtn : MonoBehaviour
{
    public static TimeScaleBtn instance = null;
    private Button button;
    [SerializeField] Sprite[] sprites;
    private Image timeScaleImage;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            return;
    }
    private void Start()
    {
        TryGetComponent(out button);
        timeScaleImage = transform.GetChild(0).GetComponent<Image>();
        button.onClick.AddListener(ToggleBtn);
    }
    public void scaleBtn1x()
    {
        timeScaleImage.sprite = sprites[0];
    }
    public void ToggleBtn()
    {
        if (TimeScaleManager.instance.currentTimeScale == 1f)
        {
            TimeScaleManager.instance.TimeScale2x();
            timeScaleImage.sprite = sprites[1];
        }
        else if (TimeScaleManager.instance.currentTimeScale == 1.8f)
        {
            TimeScaleManager.instance.TimeScale1x();
            timeScaleImage.sprite = sprites[0];
        }
    }
}
