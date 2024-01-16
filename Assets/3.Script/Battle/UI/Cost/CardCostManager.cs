using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCostManager : MonoBehaviour
{
    public static CardCostManager instance = null;

    public int Cost = 0;
    public float recoveryCost = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Cost = Map_Information.instance.mapStatus.mapInfo.FirstCost;
    }

    private void FixedUpdate()
    {
        if (!FadeManager.isLoad)
        {
            return;
        }
        if (Cost < 99)
        {
            recoveryCost += 50 * Time.deltaTime;
            if (recoveryCost > 100f)
            {
                Cost++;
                recoveryCost = 0f;
            }
        }
    }


}
