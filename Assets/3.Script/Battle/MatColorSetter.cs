using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class MatColorSetter : MonoBehaviour
{
    public static MatColorSetter instance= null;
    public Material Floor;
    public Material UpperFloor;
    float normalizedAlpha = 100f / 255f;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            SetFloorZero();
            SetUpperFloorZero();
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void SetFloorGreen()
    {
        Floor.color = new Color(Floor.color.r, Floor.color.g, Floor.color.b, normalizedAlpha);
    }
    public void SetFloorZero()
    {
        Floor.color = new Color(Floor.color.r, Floor.color.g, Floor.color.b, 0);

    }
    public void SetUpperFloorGreen()
    {
        UpperFloor.color = new Color(UpperFloor.color.r, UpperFloor.color.g, UpperFloor.color.b, normalizedAlpha);
    }
    public void SetUpperFloorZero()
    {
        UpperFloor.color = new Color(UpperFloor.color.r, UpperFloor.color.g, UpperFloor.color.b, 0);
    }
}
