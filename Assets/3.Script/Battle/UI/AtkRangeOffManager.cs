using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkRangeOffManager : MonoBehaviour
{
    public static AtkRangeOffManager instance = null;
    private GameObject[] AtkRange;

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
    }
    void Start()
    {
        AtkRange = GameObject.FindGameObjectsWithTag("AtkRange");
    }

    public void MeshRendererOff()
    {
        for (int i = 0; i < AtkRange.Length; i++)
        {
            AtkRange[i].GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
