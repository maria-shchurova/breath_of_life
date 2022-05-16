using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBranch : MonoBehaviour
{
    TubeRenderer body;

    public float thicknessMultiplier;
    void Start()
    {
        MeshManager.instance.addMesh(transform, GetComponent<MeshFilter>().mesh, GetComponent<MeshRenderer>().sharedMaterial);
        body = GetComponent<TubeRenderer>();


    }
}
