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
    [SerializeField] private int iterations  = 4;
    [SerializeField] private int StartThickness;
    [SerializeField] private float RadiusDelta;
    [SerializeField] private bool applyRadiuses;
    
    [SerializeField] private GameObject Branch;
    [SerializeField] private float length =  10;
    [SerializeField] private float angle =  30;

    private const string axiom = "X";

    private Stack<TransformInfo> transformStack;
    private Dictionary<char, string> rules;

    private string currentString = string.Empty;

    public GameObject debugObj;
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
        //Messenger.AddListener<Mesh>("MeshCombined", GenerateUV);
    }

    //string  RandomString()
    //{
    //    System.Random random = new System.Random();
    //    string entry = string.Empty;

    //    string[] array = new string[]
    //    {
    //        "X", "F", "-", "+", "[", "A", "D"
    //    };

    //    for (int i = 0; i < 27; ++i)
    //    {
    //        int index = random.Next(array.Length);
    //        entry += array[index];            
    //    }
    //    Debug.Log("string generated:  " + entry);
    //    return entry;
    //}

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
        bool branchingStarted = false;

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

        int notClosedBracesCount = 0;
        List<TubeRenderer> branchSequence = new List<TubeRenderer>();
        int branchIndex = 0;
        print(currentString);
        foreach (char ch in currentString)
        {

            switch (ch)
            {
                case 'F':
                    Vector3 initialPosition = transform.position;
                    transform.Translate(Vector3.up * length);
                    GameObject treeSegment = Instantiate(Branch);
                    treeSegment.name = "branch " + branchIndex;
                    branchIndex++;

                    Vector3[] positions = new Vector3[]
                    {
                        initialPosition,
                        transform.position
                    };

                    treeSegment.GetComponent<TubeRenderer>().SetPositions(positions);
                   if(notClosedBracesCount == 0)
                    {
                        if (branchSequence.Count > 0)
                        {
                            ShapeBranchSequence(branchSequence);
                            branchSequence.Clear();
                        }
                    }
                   else
                    {
                        branchSequence.Add(treeSegment.GetComponent<TubeRenderer>());
                    }

                    if (branchingStarted == true)
                        GrowSprouts(initialPosition, transform.position);
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
                    notClosedBracesCount++;

                    transformStack.Push(new TransformInfo()
                    {
                        position = transform.position,
                        rotation = transform.rotation
                    });

                    //Instantiate(debugObj, transform.position, new Quaternion(Normal.x, Normal.y, Normal.z, 0));

                    break;
                case ']':
                    notClosedBracesCount--;

                    if (branchingStarted == false)
                        branchingStarted = true;

                    TransformInfo ti = transformStack.Pop();
                    transform.position = ti.position;
                    transform.rotation = ti.rotation;
                    Instantiate(debugObj, transform.position, transform.rotation);
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
    void ShapeBranchSequence(List<TubeRenderer> sequence)
    {

            print(sequence);

        if (applyRadiuses)
        {
            for (int i = 0; i < sequence.Count; i++)
            {
                if (i == 0)
                {
                    sequence[i]._radiusOne = StartThickness;
                }
                else
                {
                    sequence[i]._radiusOne = sequence[i - 1]._radiusTwo;
                }

                if (sequence[i]._radiusOne - RadiusDelta > 0)
                    sequence[i]._radiusTwo = sequence[i]._radiusOne - RadiusDelta;
                else
                    sequence[i]._radiusTwo = sequence[i]._radiusOne;
            }
        }
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

    //void GenerateUV(Mesh  mesh)
    //{
    //    Vector3[] vertices = mesh.vertices;
    //    Vector2[] uvs = new Vector2[vertices.Length];

    //    for (int i = 0; i < uvs.Length; i++)
    //    {
    //        uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
    //    }
    //    mesh.uv = uvs;
    //}

    void GrowSprouts(Vector3 a, Vector3 b)
    {
        var rnd = UnityEngine.Random.Range(0, 100);
        var c = new Vector3(rnd, rnd, rnd);

        var side1 = b - a;
        var side2 = c - a;

        var Normal = Vector3.Cross(side1, side2);

        var sprout = Instantiate(debugObj, transform.position, Quaternion.identity);
        sprout.transform.LookAt(Normal);
    }

}
