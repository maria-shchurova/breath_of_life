using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TransformInfo
{
    public Vector3 position;
    public Quaternion rotation;
}
public class LSystemScript : MonoBehaviour
{
    [SerializeField]private int iterations  = 4;
    [SerializeField] private GameObject Branch;
    [SerializeField] private float length =  10;
    [SerializeField] private float angle =  30;

    private const string axiom = "X";

    private Stack<TransformInfo> transformStack;
    private Dictionary<char, string> rules;

    private string currentString = string.Empty;
    void Start()
    {
        transformStack = new Stack<TransformInfo>();
        rules = new Dictionary<char, string>
        {
            {'X', "[F-[[X]A+XA]D+F[+FAX]DA-X]D" },
            //{'X', RandomString() },
            {'F', "FF" }
        };
        Generate();

    }

    string  RandomString()
    {
        System.Random random = new System.Random();
        string entry = string.Empty;

        string[] array = new string[]
        {
            "X", "F", "-", "+", "[", "A", "D"
        };

        for (int i = 0; i < 27; ++i)
        {
            int index = random.Next(array.Length);
            entry += array[index];            
        }
        Debug.Log("string generated:  " + entry);
        return entry;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // call this method when you are ready to group your meshes
            combineAndClear();
        }
    }
    void combineAndClear()
    {
        MeshManager.instance.combineAll();
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
    }

    void Generate()
    {
        currentString = axiom;
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < iterations; i++)
        {
            foreach (char c in currentString)
            {
                sb.Append(rules.ContainsKey(c) ? rules[c] : c.ToString());
            }

            currentString = sb.ToString();
            sb = new StringBuilder();
        }


        foreach (char c in currentString)
        {
            switch (c)
            {
                case 'F':
                    Vector3 initialPosition = transform.position;
                    transform.Translate(Vector3.up * length);
                    GameObject treeSegment = Instantiate(Branch);

                    Vector3[] positions = new Vector3[]
                    {
                        initialPosition,
                        transform.position
                    };
                    treeSegment.GetComponent<TubeRenderer>().SetPositions(positions);
                   // treeSegment.transform.SetParent(transform);
                    break;
                case 'X':
                    break;
                case '+':
                    transform.Rotate(Vector3.back * angle);
                    break;
                case '-':
                    transform.Rotate(Vector3.forward * angle);
                    break;
                case '[':
                    transformStack.Push(new TransformInfo()
                    {
                        position = transform.position,
                        rotation = transform.rotation
                    });
                    break;
                case ']':
                    TransformInfo ti = transformStack.Pop();
                    transform.position = ti.position;
                    transform.rotation = ti.rotation;
                    break;
                case 'A':
                    transform.Rotate(Vector3.left * angle);
                    break;                
                case 'D':
                    transform.Rotate(Vector3.right * angle);
                    break;
                default:
                    throw new InvalidOperationException("invalid L operation");
            }
        }
        combineAndClear();
    }

    void CombineMesh()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);

            i++;
        }
        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        transform.gameObject.SetActive(true);
    }

    
}
