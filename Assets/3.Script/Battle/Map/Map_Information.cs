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
    public bool checkEnd = false;
    public int char_Limit;
    [SerializeField] private Text char_Limit_Text;

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
        char_Limit_Text = GameObject.FindGameObjectWithTag("char_Limit_Text").GetComponent<Text>();

        TryGetComponent(out mapStatus);
        totalEnemy = mapStatus.mapInfo.totalEnemy;
        deathEnemy = 0;
        defenseHP = mapStatus.mapInfo.DefenseHP;
        char_Limit = mapStatus.mapInfo.Char_Limit;

        mapStatus = StageManager.instance.mapStatus;
    }
    private void Start()
    {
        UpdateInfo();
    }
    private void Update()
    {
        if (!checkEnd)
            return;
        GameEndCheck();
    }


    public void UpdateInfo()
    {
        totalEnemy_Text.text = $"{deathEnemy}/{totalEnemy}";
        defenseHP_Text.text = defenseHP.ToString();
        char_Limit_Text.text = char_Limit.ToString();
    }
    public void CheckEnd()
    {
        checkEnd = true;

    }
    private void GameEndCheck()
    {
        if (checkEnd)
        {
            if (deathEnemy == totalEnemy)
            {
                Debug.Log("³¡");
                checkEnd = false;
                StartCoroutine(WaitEnd());
                //MissionComplete.instance.gameObject.SetActive(true);               
            }
        }
    }
    private IEnumerator WaitEnd()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        MissionComplete.instance.gameObject.SetActive(true);
        yield break;
    }
}
