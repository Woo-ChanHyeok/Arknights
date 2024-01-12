using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeOperator : MonoBehaviour
{
    public GameObject Oper_UI;

    private void OnDestroy()
    {
        Oper_UI.SetActive(true);
        float respawnTime = Oper_UI.GetComponent<OperStatus>().operInfo.RestoreTime;
        Oper_UI.GetComponent<Respawn_UI>().RespawnTime = respawnTime;
        Map_Information.instance.char_Limit++;
        Map_Information.instance.UpdateInfo();

    }
}
