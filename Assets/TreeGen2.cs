using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class TreeGen2 : MonoBehaviour
{
    Vector3[] vertices;
    int[] triangles;
    Mesh mesh;

    [SerializeField] float radius;
    [SerializeField] int subdivisions; // min 3 todo adda range from 3 to ...?

    public GameObject debug;
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
    }

    void CreateShape()
    {
        vertices = new Vector3[subdivisions];

        Vector3 center = transform.position;

        float angle = 0;
        print(angle);

        for (int i = 0; i < subdivisions; i++)
        {
            vertices[i] = new Vector3(
                Mathf.Cos(Mathf.Deg2Rad * angle) * radius, //x
                Mathf.Sin(Mathf.Deg2Rad * angle) * radius,//y
                0); //z

            angle += (360f / subdivisions);
        }


        foreach (Vector3 pos in vertices)
        {
            Instantiate(debug, pos, Quaternion.identity);
        }
    }


}
