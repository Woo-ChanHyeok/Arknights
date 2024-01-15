using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCreate : MonoBehaviour
{
    private void Start()
    {
        OperatorManager.instance.CreateLineUp(transform);
    }
}
