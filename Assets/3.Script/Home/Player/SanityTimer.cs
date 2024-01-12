using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityTimer : MonoBehaviour
{
    public static SanityTimer instance = null;
    [SerializeField] private float restoreSanityTime = 0f;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);
    }
    private void Update()
    {
        if(PlayerInfoManager.instance.playerInfo.Sanity < PlayerInfoManager.instance.playerInfo.MaxSanity)
        {
            restoreSanityTime += Time.unscaledDeltaTime;
            if(restoreSanityTime > 360f)
            {
                PlayerInfoManager.instance.playerInfo.Sanity++;
                restoreSanityTime = 0f;
            }
        }
    }
}
