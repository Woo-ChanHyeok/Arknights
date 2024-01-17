using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquadManager : MonoBehaviour
{
    public static SquadManager instance = null;
    public static GameObject Selected;
    public bool dontDestroyOper = false;
    public GameObject removeObject = null;
    public List<OperatorLineUp> squads = new List<OperatorLineUp>();
    public GameObject btlOperCanvas;
    public GameObject mainBtlOperCanvas;
    public GameObject btlOperContent;
    public GameObject mainBtlOperContent;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);

        btlOperCanvas.SetActive(false);
    }
    public void TakeOperBtn()
    {
        btlOperCanvas.SetActive(true);
        StartCoroutine(FadeManager.instance.CanvasFadeIn(btlOperCanvas.GetComponent<CanvasGroup>()));
    }

    public void BackBtn()
    {
        //btlOperCanvas.SetActive(false);
        StartCoroutine(FadeManager.instance.CanvasFadeOut(btlOperCanvas.GetComponent<CanvasGroup>(), btlOperCanvas));
    }
    public void TurnOn_Main()
    {
        mainBtlOperCanvas.SetActive(true);
        StartCoroutine(FadeManager.instance.CanvasFadeIn(mainBtlOperCanvas.GetComponent<CanvasGroup>()));
    }
    public void BackBtn_Main()
    {
        //btlOperCanvas.SetActive(false);
        StartCoroutine(FadeManager.instance.CanvasFadeOut(mainBtlOperCanvas.GetComponent<CanvasGroup>(), mainBtlOperCanvas));
    }
    public void ConfirmBtn()
    {
        if (Selected == null)
        {
            if(removeObject != null)
            {
                RemoveSquadList(removeObject.GetComponent<OperStatus>().operInfo.ItsMe);
                removeObject = null;
            }
            BackBtn();
            return;
        }
        else if (Selected.GetComponent<OperStatus>() != null)
        {
            AddSquadList(Selected.GetComponent<OperStatus>().operInfo.ItsMe);
            BackBtn();
        }
    }

    public void AddSquadList(OperatorLineUp LineUp)
    {
        if (!squads.Contains(LineUp))
            squads.Add(LineUp);

        SquadCavasManager.instance.SquadLineUp();
        MainSquadCanvasManager.instance.SquadLineUp();
    }
    public void RemoveSquadList(OperatorLineUp LineUp)
    {
        if (squads.Contains(LineUp))
            squads.Remove(LineUp);

        SquadCavasManager.instance.SquadLineUp();
        MainSquadCanvasManager.instance.SquadLineUp();
    }

    public void UIUpdate()
    {
        for(int i =0; i < btlOperContent.transform.childCount; i++)
        {
            btlOperContent.transform.GetChild(i).GetComponent<Squad_Slot>().UpdateInfo();
        }
        for (int i = 0; i < mainBtlOperContent.transform.childCount; i++)
        {
            mainBtlOperContent.transform.GetChild(i).GetComponent<Squad_Slot>().UpdateInfo();
        }
        
    }
}
