using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostText : MonoBehaviour
{
    private Text costText;
    void Start()
    {
        TryGetComponent(out costText);
    }
    private void FixedUpdate()
    {
        costText.text = CardCostManager.instance.Cost.ToString();
    }

}
