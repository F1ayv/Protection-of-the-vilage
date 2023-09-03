using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    public GameObject plane;
    public Transform folderPlanes;
    public Material[] materials;
    public float sizePlaneX, sizePlaneZ;
    public int sizeX = 9, sizeZ = 5;
    private void Start()
    {
        for (int i = 0; i < sizeX; i++)
        {
            for (int n = 0; n < sizeZ; n++)
            {
               GameObject obj = Instantiate(plane, folderPlanes) as GameObject;
               obj.transform.position = new Vector3(i * sizePlaneX + sizePlaneX/2, 0,  n * sizePlaneZ + sizePlaneZ/2);
               obj.transform.localScale = new Vector3(sizePlaneX/10, 1,  sizePlaneZ/10);
               obj.GetComponent<MeshRenderer>().material = materials[(i+n)%2];
            }
        }
    }
}
