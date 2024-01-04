using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostSlider : MonoBehaviour
{
    private Slider slider;
    private void Start()
    {
        TryGetComponent(out slider);
    }
    private void FixedUpdate()
    {
        slider.value = CardCostManager.instance.recoveryCost;

    }
}
