using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BG_Movement : MonoBehaviour
{
    private RectTransform rect;
    public ScrollRect scrollRect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        scrollRect.onValueChanged.AddListener(MoveBG);
    }
    private void MoveBG(Vector2 normal)
    {
        float targetY = rect.anchoredPosition.y;

        float leftBoundX = 115f;

        float rightBoundX = -115f;

        float targetX = Mathf.Lerp(leftBoundX, rightBoundX, normal.x);

        rect.anchoredPosition = new Vector2(targetX, targetY);
    }
}
