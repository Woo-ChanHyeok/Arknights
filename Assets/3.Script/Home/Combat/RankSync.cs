using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankSync : MonoBehaviour
{
    public Sprite[] Ranks;
    public Image image;
    private void Start()
    {
        TryGetComponent(out image);
    }
    public void SyncRank(MapStatus mapStatus)
    {
        if(mapStatus.mapInfo.ClearRank == 0)
        {
            image.sprite = Ranks[0];
        }
        else if (mapStatus.mapInfo.ClearRank == 1)
        {
            image.sprite = Ranks[1];
        }
        else if (mapStatus.mapInfo.ClearRank == 2)
        {
            image.sprite = Ranks[2];
        }
        else if (mapStatus.mapInfo.ClearRank == 3)
        {
            image.sprite = Ranks[3];
        }
    }
}
