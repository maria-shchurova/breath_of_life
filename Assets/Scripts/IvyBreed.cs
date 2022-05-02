using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvyBreed : MonoBehaviour
{
    public Material mat1;
    public Material mat2;
    public MeshRenderer mesh;
    void Start()
    {
        MixMaterial(mesh);
    }

    // Update is called once per frame
    void MixMaterial(MeshRenderer nmesh)
    {
        CombineColors(mat1.color, mat2.color);
        mesh.material.color = CombineColors(mat1.color, mat2.color);
    }

    public static Color CombineColors(params Color[] aColors)
    {
        Color result = new Color(0, 0, 0, 0);
        foreach (Color c in aColors)
        {
            result += c;
        }
        result /= aColors.Length;
        return result;
    }
}
