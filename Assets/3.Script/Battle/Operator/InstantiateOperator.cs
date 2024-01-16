using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOperator : MonoBehaviour
{
    public GameObject[] prefabs;

    private void Start()
    {
        InstantiateOper();
    }

    private void InstantiateOper()
    {
        
        for(int i = 0; i< prefabs.Length; i++)
        {
            for (int a = 0; a < SquadManager.instance.squads.Count; a++)
            {
                if(prefabs[i].GetComponent<OperStatus>().operInfo.ItsMe == SquadManager.instance.squads[a])
                {
                    Instantiate(prefabs[i], transform);
                }
            }
        }
    }
}
