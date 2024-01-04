using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassIcon : MonoBehaviour
{
    [SerializeField] private Image classIcon;
    [SerializeField] private OperStatus operStatus;

    private void Start()
    {
        TryGetComponent(out classIcon);
        operStatus = transform.parent.parent.GetComponent<OperStatus>();
        IconDecide(operStatus.operInfo.ClassType);
    }

    private void IconDecide(OperClass operClass)
    {
        if (operClass.Equals(OperClass.Vanguard))
            classIcon.sprite = Oper_InfoUpdater.instance.ClassIcon[0];
        
        else if (operClass.Equals(OperClass.Guard))
            classIcon.sprite = Oper_InfoUpdater.instance.ClassIcon[1];

        else if (operClass.Equals(OperClass.Defender))
            classIcon.sprite = Oper_InfoUpdater.instance.ClassIcon[2];

        else if (operClass.Equals(OperClass.Sniper))
            classIcon.sprite = Oper_InfoUpdater.instance.ClassIcon[3];

        else if (operClass.Equals(OperClass.Caster))
            classIcon.sprite = Oper_InfoUpdater.instance.ClassIcon[4];

        else if (operClass.Equals(OperClass.Medic))
            classIcon.sprite = Oper_InfoUpdater.instance.ClassIcon[5];

        else if (operClass.Equals(OperClass.Supporter))
            classIcon.sprite = Oper_InfoUpdater.instance.ClassIcon[6];

        else if (operClass.Equals(OperClass.Specialist))
            classIcon.sprite = Oper_InfoUpdater.instance.ClassIcon[7];

    }
}
