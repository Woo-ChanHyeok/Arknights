using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatOffsetChanger : MonoBehaviour
{
    public Material AttackRange;

    float y = 3f;
    float Offset_y;
    private void Update()
    {
        Offset_y -= (Time.fixedDeltaTime * y) / 10f;
        AttackRange.mainTextureOffset = new Vector2(0, Offset_y);
    }
}
