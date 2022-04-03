using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBranch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MeshManager.instance.addMesh(transform, GetComponent<MeshFilter>().mesh, GetComponent<MeshRenderer>().sharedMaterial);
    }
}
