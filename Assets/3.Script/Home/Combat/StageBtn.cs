using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageBtn : MonoBehaviour
{
    private MapStatus mapStatus;
    private Button Btn;
    private Color originColor;
    private Color SelectColor;

    private void Awake()
    {
        TryGetComponent(out mapStatus);
        TryGetComponent(out Btn);
        originColor = Color.white;
        SelectColor = new Color(150f / 255f, 150f / 255f, 150f / 255f, 1);
    }
    private void Update()
    {
        if (StageManager.instance.mapStatus == mapStatus && StageManager.instance.InformationCanvas.activeSelf)
        {
            SetBtnColor(true);
        }
        else
        {
            SetBtnColor(false);
            return;
        }
    }
    private void SetBtnColor(bool select)
    {
        if (select)
        {
            Btn.image.color = SelectColor;
        }
        else
        {
            Btn.image.color = originColor;
        }
    }
    public void MapBtn()
    {
        StageManager.instance.mapStatus = mapStatus;
        StageManager.instance.TurnOnCanvas();
        StageManager.instance.SyncInformationUI();
    }
}
