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
        triangles = new int[subdivisions * 2 * 3];

        //for (int i = 0; i < subdivisions; i++)
        //{
        //    if (i == subdivisions) // last in a cycle
        //    {
        //        triangles[i] = i;
        //        triangles[i + 1] = 0;
        //        triangles[i + 2] = i + subdivisions;
        //    }
        //    else
        //    {
        //        triangles[i] = i;
        //        triangles[i + 1] = i + 1;
        //        triangles[i + 2] = i + subdivisions + 1;
        //    }

        //    print("Triangle built: " + triangles[i] + " " + triangles[i + 1] + " " + triangles[i + 2]);
        //}

        triangles = new int[]
        {
            0, 1, 5,
            1, 2, 6,
            2, 3, 7
        };
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
