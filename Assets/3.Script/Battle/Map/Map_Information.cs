using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map_Information : MonoBehaviour
{
    public static Map_Information instance = null;
    public MapStatus mapStatus;
    //private string MapName;
    [SerializeField] private Text totalEnemy_Text;
    public int totalEnemy;
    public int deathEnemy;
    [SerializeField] private Text defenseHP_Text;
    public int defenseHP;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        totalEnemy_Text = GameObject.FindGameObjectWithTag("totalEnemy_Text").GetComponent<Text>();
        defenseHP_Text = GameObject.FindGameObjectWithTag("defenseHP_Text").GetComponent<Text>();
    }
    private void Start()
    {
        TryGetComponent(out mapStatus);
        totalEnemy = mapStatus.mapInfo.totalEnemy;
        deathEnemy = 0;
        defenseHP = mapStatus.mapInfo.DefenseHP;

        UpdateInfo();
    }


    public void UpdateInfo()
    {
        totalEnemy_Text.text = $"{deathEnemy}/{totalEnemy}";
        defenseHP_Text.text = defenseHP.ToString();
    }
}
