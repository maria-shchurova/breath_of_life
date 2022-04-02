using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    LineRenderer line;
    Mesh mesh;
    public  Vector3[] vertices;
    int[] triangles;
    void Start()
    {
        mesh = new Mesh();
        line = GetComponent<LineRenderer>();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[] //if line.points = 2
        {
            new Vector3 (line.GetPosition(0).x -  line.startWidth / 2, line.GetPosition(0).y, line.GetPosition(0).z),
            new Vector3 (line.GetPosition(0).x +  line.startWidth / 2, line.GetPosition(0).y, line.GetPosition(0).z),
            new Vector3 (line.GetPosition(1).x +  line.endWidth / 2, line.GetPosition(0).y, line.GetPosition(0).z),
            new Vector3 (line.GetPosition(1).x -  line.endWidth / 2, line.GetPosition(0).y, line.GetPosition(0).z),
        };

        triangles = new int[]
        {
            0, 1, 2,
            1, 3, 2 
        };
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
