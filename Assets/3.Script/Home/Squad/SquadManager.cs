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
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);
    }
    public void TakeOperBtn()
    {
        btlOperCanvas.SetActive(true);
    }

    public void BackBtn()
    {
        btlOperCanvas.SetActive(false);
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
    }
    public void RemoveSquadList(OperatorLineUp LineUp)
    {
        if (squads.Contains(LineUp))
            squads.Remove(LineUp);

        SquadCavasManager.instance.SquadLineUp();
    }


}
