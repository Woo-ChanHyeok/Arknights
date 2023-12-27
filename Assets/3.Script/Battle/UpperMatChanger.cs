using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperMatChanger : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    [SerializeField] private Material originalMat;
    private GameObject childObject;
    private void Start()
    {
        TryGetComponent(out meshRenderer);
        childObject = transform.GetChild(0).gameObject;
    }

    private void FixedUpdate()
    {
        FindMyChild();
    }

    private void Material_Null()
    {
        meshRenderer.material = null;
    }
    private void Material_Original()
    {
        meshRenderer.material = originalMat;
    }
    private void FindMyChild()
    {
        if(childObject.transform.childCount > 0)
        {
            Material_Null();
            childObject.tag = "Untagged";
        }
        else
        {
            Material_Original();
            childObject.tag = "UpFloor";

        }

    }
}
