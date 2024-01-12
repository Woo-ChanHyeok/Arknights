using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Respawn_UI : MonoBehaviour
{
    private OperStatus operStatus;
    [SerializeField] private GameObject Respawn_BG;
    public float RespawnTime = 0f;
    public Slider slider;
    public Text text;
    private void Awake()
    {
        TryGetComponent(out operStatus);
    }
    void Start()
    {
        slider.minValue = 0f;
        slider.maxValue = operStatus.operInfo.RestoreTime;
        
    }

    void Update()
    {
        SetUp_UI();
        if (RespawnTime > 0f)
        {
            slider.value = operStatus.operInfo.RestoreTime - RespawnTime;
            RespawnTime -= Time.deltaTime;
            text.text = RespawnTime.ToString("F1");
            if (RespawnTime < 0f)
            {
                RespawnTime = 0f;
            }
        }
    }
    public void SetUp_UI()
    {
        if (RespawnTime > 0f && !Respawn_BG.activeSelf)
        {
            Respawn_BG.SetActive(true);
        }
        else if(RespawnTime <= 0f && Respawn_BG.activeSelf)
        {
            Respawn_BG.SetActive(false);
        }
    }
}
