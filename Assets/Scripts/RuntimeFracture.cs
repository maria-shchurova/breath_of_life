using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MeshProcess;

public class RuntimeFracture : MonoBehaviour
{
    public int seed = 0;
    public GameObject source;
    private FractureTypes fractureType;
    public uint VHACDResolution = 100000;
    public float jointBreakForce = 1000;

    private enum FractureTypes
    {
        Voronoi
    }

    private enum GenerateType
    {
        Preview,
        Generate,
        CreateAsset
    }

    void Start()
    {
        seed = Random.Range(0, 100);
        _createObject(GenerateType.Generate);
    }


    void Update()
    {
        
    }

    private void _createObject(GenerateType generateType)
    {
        NvBlastExtUnity.setSeed(seed);

        GameObject cs = generateType == GenerateType.Preview ? new GameObject(generateType == GenerateType.Generate ? $"{source.name}{" Chunk"}Preview" : $"{source.name}") : Instantiate(source);
        if (generateType != GenerateType.Preview)
            cs.name = $"{source.name}{" Chunk"}";

        cs.SetActive(true);
        cs.transform.position = Vector3.zero;
        cs.transform.rotation = Quaternion.identity;
        cs.transform.localScale = Vector3.one;

        Mesh ms = null;

        Material[] mats = new Material[2];

        //mats[1] = insideMaterial ? insideMaterial : source.GetComponent<MeshRenderer>().sharedMaterial;

        MeshFilter mf = source.GetComponent<MeshFilter>();
        SkinnedMeshRenderer smr = source.GetComponent<SkinnedMeshRenderer>();

        if (mf != null)
        {
            mats[0] = source.GetComponent<MeshRenderer>().sharedMaterial;
            ms = source.GetComponent<MeshFilter>().sharedMesh;
        }
        if (smr != null)
        {
            mats[0] = smr.sharedMaterial;
            smr.gameObject.transform.position = Vector3.zero;
            smr.gameObject.transform.rotation = Quaternion.identity;
            smr.gameObject.transform.localScale = Vector3.one;
            ms = new Mesh();
            smr.BakeMesh(ms);
            //ms = smr.sharedMesh;
        }

        if (ms == null)
            return;

        NvMesh mymesh = new NvMesh(ms.vertices, ms.normals, ms.uv, ms.vertexCount, ms.GetIndices(0), (int)ms.GetIndexCount(0));

        // cleaner = new NvMeshCleaner();
        //cleaner.cleanMesh(mymesh);

        NvFractureTool fractureTool = new NvFractureTool();
        fractureTool.setSourceMesh(mymesh);

        fractureType = FractureTypes.Voronoi;


        fractureTool.finalizeFracturing();

        NvLogger.Log("Chunk Count: " + fractureTool.getChunkCount());

        if (generateType == GenerateType.CreateAsset)
        {
            if (!AssetDatabase.IsValidFolder("Assets/NvBlast Prefabs")) AssetDatabase.CreateFolder("Assets", "NvBlast Prefabs");
            if (!AssetDatabase.IsValidFolder("Assets/NvBlast Prefabs/Meshes")) AssetDatabase.CreateFolder("Assets/NvBlast Prefabs", "Meshes");
            if (!AssetDatabase.IsValidFolder("Assets/NvBlast Prefabs/Fractured")) AssetDatabase.CreateFolder("Assets/NvBlast Prefabs", "Fractured");

            FileUtil.DeleteFileOrDirectory("Assets/NvBlast Prefabs/Meshes/" + source.name);
            AssetDatabase.Refresh();
            AssetDatabase.CreateFolder("Assets/NvBlast Prefabs/Meshes", source.name);
        }

        for (int i = 1; i < fractureTool.getChunkCount(); i++)
        {
            GameObject chunk = new GameObject("Chunk" + i);
            chunk.transform.parent = cs.transform;

            MeshFilter chunkmf = chunk.AddComponent<MeshFilter>();
            MeshRenderer chunkmr = chunk.AddComponent<MeshRenderer>();

            chunkmr.sharedMaterials = mats;

            NvMesh outside = fractureTool.getChunkMesh(i, false);
            NvMesh inside = fractureTool.getChunkMesh(i, true);

            Mesh m = outside.toUnityMesh();
            m.subMeshCount = 2;
            m.SetIndices(inside.getIndexes(), MeshTopology.Triangles, 1);
            chunkmf.sharedMesh = m;

            if (generateType == GenerateType.CreateAsset)
            {
                AssetDatabase.CreateAsset(m, "Assets/NvBlast Prefabs/Meshes/" + source.name + "/Chunk" + i + ".asset");
            }

            if (generateType == GenerateType.Preview) chunk.AddComponent<ChunkInfo>();

            if (generateType == GenerateType.Generate || generateType == GenerateType.CreateAsset)
            {
                VHACD vhacd = chunk.AddComponent<VHACD>();
                vhacd.m_parameters.m_resolution = VHACDResolution;
                List<Mesh> meshes = vhacd.GenerateConvexMeshes();

                foreach (Mesh mesh in meshes)
                {
                    MeshCollider collider = chunk.AddComponent<MeshCollider>();
                    collider.sharedMesh = mesh;
                    collider.convex = true;
                }
                DestroyImmediate(vhacd);
            }

            chunk.transform.position = Vector3.zero;
            chunk.transform.rotation = Quaternion.identity;
        }

        if (generateType == GenerateType.Generate || generateType == GenerateType.CreateAsset)
        {
            cs.AddComponent<FractureObject>().jointBreakForce = jointBreakForce;

            for (int i = 0; i < cs.transform.childCount; i++)
            {
                Transform chunk = cs.transform.GetChild(i);
                chunk.gameObject.layer = LayerMask.NameToLayer("Fracture");
                Bounds chunkBounds = new Bounds(chunk.GetComponent<MeshRenderer>().bounds.center, chunk.GetComponent<MeshRenderer>().bounds.size * 1.5f);

                chunk.GetComponent<MeshRenderer>().enabled = false;

                ChunkRuntimeInfo chunkInfo = chunk.gameObject.AddComponent<ChunkRuntimeInfo>();
            }
        }

        if (generateType == GenerateType.CreateAsset)
        {
            GameObject p = PrefabUtility.SaveAsPrefabAsset(cs, "Assets/NvBlast Prefabs/Fractured/" + source.name + "_fractured.prefab");

            GameObject fo;

            bool skinnedMesh = false;
            if (source.GetComponent<SkinnedMeshRenderer>() != null) skinnedMesh = true;

            if (skinnedMesh)
                fo = Instantiate(source.transform.root.gameObject);
            else
                fo = Instantiate(source);

            Destructible d = fo.AddComponent<Destructible>();
            d.fracturedPrefab = p;

            bool hasCollider = false;
            if (fo.GetComponent<MeshCollider>() != null) hasCollider = true;
            if (fo.GetComponent<BoxCollider>() != null) hasCollider = true;
            if (fo.GetComponent<SphereCollider>() != null) hasCollider = true;
            if (fo.GetComponent<CapsuleCollider>() != null) hasCollider = true;

            if (!hasCollider)
            {
                BoxCollider bc = fo.AddComponent<BoxCollider>();
                if (skinnedMesh)
                {
                    Bounds b = source.GetComponent<SkinnedMeshRenderer>().bounds;
                    bc.center = new Vector3(0, .5f, 0);
                    bc.size = b.size;
                }
            }

            PrefabUtility.SaveAsPrefabAsset(fo, "Assets/NvBlast Prefabs/" + source.name + ".prefab");
            DestroyImmediate(fo);
        }

        cs.transform.position = source.transform.position;
        cs.transform.rotation = source.transform.rotation;
        cs.transform.localScale = source.transform.localScale;
    }

}
