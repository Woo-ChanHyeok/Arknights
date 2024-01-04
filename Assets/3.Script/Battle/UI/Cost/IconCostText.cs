using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconCostText : MonoBehaviour
{
    private OperStatus operStatus;
    private Text costText;
    void Start()
    {
        operStatus = transform.parent.parent.parent.GetComponent<OperStatus>();
        TryGetComponent(out costText);
        costText.text = operStatus.operInfo.CardCost.ToString();
    }

    
}
