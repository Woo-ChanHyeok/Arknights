using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum OperatorLineUp
{
    Amiya_002 = 2,
}

public class OperatorManager : MonoBehaviour
{
    public static OperatorManager instance = null;
    public GameObject operatorPerfab;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }    
    public void CreateLineUp(Transform parent)
    {
        DestroyLineUp(parent);
        for (int i = 0; i < PlayerInfoManager.instance.playerInfo.haveOperList.Count; i++)
        {
            InstantiateOperator(PlayerInfoManager.instance.playerInfo.haveOperList[i], parent);
        }
    }
    public void DestroyDuplicateLineUp(Transform parent)
    {
        DestroyLineUp(parent);
        for (int i = 0; i < PlayerInfoManager.instance.playerInfo.haveOperList.Count; i++)
        {
            InstantiateOperatorButDestroyDuplelicate(PlayerInfoManager.instance.playerInfo.haveOperList[i], parent);
        }
    }
    public void DestroyDuplicateLineUpNotDestroyMe (Transform parent, OperatorLineUp DontDestroy)
    {
        DestroyLineUp(parent);
        OperStatus copy = transform.Find(DontDestroy.ToString()).GetComponent<OperStatus>();
        GameObject oper = Instantiate(operatorPerfab, parent);
        oper.GetComponent<OperStatus>().CopyStatus(copy);
        oper.GetComponent<Squad_Slot>().UpdateInfo();
        SquadManager.Selected = oper;
        SquadManager.instance.removeObject = oper;

        for (int i = 0; i < PlayerInfoManager.instance.playerInfo.haveOperList.Count; i++)
        {
            InstantiateOperatorButDestroyDuplelicate(PlayerInfoManager.instance.playerInfo.haveOperList[i], parent);
        }
    }
    private void InstantiateOperator(OperatorLineUp haveOper, Transform parent)
    {
        OperStatus copy = transform.Find(haveOper.ToString()).GetComponent<OperStatus>();
        GameObject oper = Instantiate(operatorPerfab, parent);
        oper.GetComponent<OperStatus>().CopyStatus(copy);
        oper.GetComponent<Squad_Slot>().UpdateInfo();
    }
    public void InstantiateOperatorButDestroyDuplelicate(OperatorLineUp haveOper, Transform parent)
    {
        if (SquadManager.instance.squads.Contains(haveOper))
        {
            return;
        }
        OperStatus copy = transform.Find(haveOper.ToString()).GetComponent<OperStatus>();
        GameObject oper = Instantiate(operatorPerfab, parent);
        oper.GetComponent<OperStatus>().CopyStatus(copy);
        oper.GetComponent<Squad_Slot>().UpdateInfo();
    }

    private void DestroyLineUp(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }
    public OperStatus GetOperStatus(string name)
    {
        OperStatus oper = transform.Find(name).GetComponent<OperStatus>();
        return oper;
    }
}
