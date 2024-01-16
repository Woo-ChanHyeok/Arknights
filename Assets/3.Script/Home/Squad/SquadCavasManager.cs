using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SquadCavasManager : MonoBehaviour
{
    public static SquadCavasManager instance = null;
    public Transform content;
    public GameObject[] Slots;
    public Image[] images;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        gameObject.SetActive(false);
    }
    private void Start()
    {

    }
    private void Update()
    {
        
    }
    public void SquadLineUp()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].enabled = true;
            images[i].transform.GetChild(0).gameObject.SetActive(false);
        }

        for (int i = 0; i < SquadManager.instance.squads.Count; i++)
        {
            images[i].enabled = false;
            images[i].transform.GetChild(0).GetComponent<OperStatus>().CopyStatus(OperatorManager.instance.GetOperStatus(SquadManager.instance.squads[i].ToString()));
            images[i].transform.GetChild(0).gameObject.SetActive(true);
            images[i].transform.GetChild(0).GetComponent<Squad_Slot>().UpdateInfo();
        }
    }
    public void SquadBtn()
    {
        OperatorManager.instance.DestroyDuplicateLineUp(content);
    }
    public void OperBtn()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        OperatorManager.instance.DestroyDuplicateLineUpNotDestroyMe(content, clickObject.GetComponent<OperStatus>().operInfo.ItsMe);
    }
    public void BackToMainBtn()
    {
        StartCoroutine(FadeManager.instance.CanvasFadeOut(GetComponent<CanvasGroup>(), gameObject));
    }
    public void TurnOnMe()
    {
        gameObject.SetActive(true);
        StartCoroutine(FadeManager.instance.CanvasFadeIn(GetComponent<CanvasGroup>()));
    }
}
