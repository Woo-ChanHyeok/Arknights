using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkRange : MonoBehaviour
{
    private bool isOn = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("AtkRange") && isOn)
        {
            other.GetComponent<MeshRenderer>().enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("AtkRange"))
        {
            other.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void OnEnable()
    {
        isOn = true;
    }
    private void OnDisable()
    {
        isOn = false;
        AtkRangeOffManager.instance.MeshRendererOff();
    }

    public void DefaultDirection()
    {
        gameObject.transform.localRotation = Quaternion.Euler(36.5f, 0, 0);
    }
    public void UpDirection()
    {
        gameObject.transform.localRotation = Quaternion.Euler(36.5f, 0, 90);
    }
    public void DownDirection()
    {
        gameObject.transform.localRotation = Quaternion.Euler(36.5f, 0, -90);
    }
}
