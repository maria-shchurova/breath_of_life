using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
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
    }

    void CreateCircle()
    {
        Vector3 center = transform.position; //todo use this somehow??

        float angle = 0;
        print(angle);

        //forming the circle
        for (int i = 0; i < subdivisions; i++) 
        {
            vertices.Add(new Vector3(
                Mathf.Cos(Mathf.Deg2Rad * angle) * radius, //x
                0,//y
                Mathf.Sin(Mathf.Deg2Rad * angle) * radius)); //z

            angle += (360f / subdivisions);
        }
        //adding height
        foreach (Vector3 pos in vertices.ToArray())
        {
            vertices.Add(pos + Vector3.up * cylynderHeight);
        }
    }



    void DisplayPoints()
    {
        foreach (Vector3 pos in vertices)
        {
            Instantiate(debug, pos, Quaternion.identity);
        }
    }
}
