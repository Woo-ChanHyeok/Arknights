using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkRange : MonoBehaviour
{ 
    public Collider[] colls1;
    public Collider[] colls2;
    public Vector3 center1 = new Vector3(0, 0, -0.5f);
    public Vector3 boxSize1 = new Vector3(3, 3, 1);    
    public Vector3 center2 = new Vector3(0, 0, -0.5f);
    public Vector3 boxSize2 = new Vector3(3, 3, 1);

    private int floorLayer;

    private void Start()
    {
        floorLayer = 1 << LayerMask.NameToLayer("AtkRange");
    }
    void Update()
    {
        DisableRange();
        colls1 = Physics.OverlapBox(transform.TransformPoint(center1), boxSize1 * 0.5f, transform.rotation, floorLayer);
        colls2 = Physics.OverlapBox(transform.position + center2, boxSize2 * 0.5f, transform.rotation, floorLayer);
        for(int i = 0; i < colls1.Length; i++)
        {
            colls1[i].GetComponent<MeshRenderer>().enabled = true;
        }
        for (int i = 0; i < colls2.Length; i++)
        {
            colls2[i].GetComponent<MeshRenderer>().enabled = true;
        }
    }
    private void DisableRange()
    {
        for (int i = 0; i < colls1.Length; i++)
        {
            colls1[i].GetComponent<MeshRenderer>().enabled = false;
        }
        for (int i = 0; i < colls2.Length; i++)
        {
            colls2[i].GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        //Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, Quaternion.Euler(-90, 0, 0), Vector3.one);
        //Gizmos.matrix = rotationMatrix;
        Gizmos.DrawWireCube(transform.position + center1, boxSize1);
        Gizmos.DrawWireCube(transform.position + center2, boxSize2);
    }
}
