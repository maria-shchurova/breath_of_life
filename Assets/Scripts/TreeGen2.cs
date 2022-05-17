using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TreeGen2 : MonoBehaviour
{
    List<Vector3> vertices = new List<Vector3>();
    //Vector3[] vertices;
    int[] triangles;
    Mesh mesh;

    [SerializeField] float radius;
    [SerializeField] int subdivisions; // min 3 todo adda range from 3 to ...?

    [SerializeField] float cylynderHeight;
    public GameObject debug;
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateCircle();
        DisplayPoints();
        CalculateIndices();
        UpdateMesh();
    }

    void CreateCircle()
    {
        Vector3 center = transform.position; //todo use this somehow??

        float angle = 0;

        //forming the circle
        for (int i = 0; i < subdivisions; i++)
        {
            vertices.Add(new Vector3(
                Mathf.Cos(Mathf.Deg2Rad * angle) * radius, //x
                0,//y
                Mathf.Sin(Mathf.Deg2Rad * angle) * radius)); //z

            angle += (360f / subdivisions);
        }
        //adding height aka @second floor
        foreach (Vector3 pos in vertices.ToArray())
        {
            vertices.Add(pos + Vector3.up * cylynderHeight);
        }
    }

    void CalculateIndices()
    {
        triangles = new int[subdivisions * 6];
        for (int t = 0, v = 0, x = 0; x < subdivisions - 1; x++, t += 6, v++)
        {
            //    //if (i == subdivisions) // last in a cycle
            //    //{
            //    //    triangles[i] = i;
            //    //    triangles[i + 1] = 0;
            //    //    triangles[i + 2] = i + subdivisions;
            //    //}
            //    //else
            //    //{
            triangles[t] = v;
            triangles[t + 1] = v + subdivisions + 1;
            triangles[t + 2] = v + 1;
            print("Triangle built: " + triangles[t] + triangles[t + 1] + triangles[t + 2]);
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    void DisplayPoints()
    {
        for (int i = 0; i < vertices.Count; i++)
        {
            GameObject point =  Instantiate(debug, vertices[i], Quaternion.identity);
            point.name = "point " + i;
        }
    }
}
