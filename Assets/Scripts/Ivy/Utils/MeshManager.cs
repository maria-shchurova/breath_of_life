using System.Collections.Generic;
using UnityEngine;

public class MeshGroup {
    public string materialName;
    public Color color;
    //public Color colorEnd;
    public List<Mesh> meshes;
    public List<Transform> transforms;

    public MeshGroup(string materialName, Color materialColor) {
        this.materialName = materialName;
        this.color = materialColor;
        //this.colorEnd = materialColorEnd;
        this.meshes = new List<Mesh>();
        this.transforms = new List<Transform>();
    }
}

// public class MeshChunk {
//     public Mesh mesh;
//     public Transform transform;

//     public MeshChunk(Mesh mesh, Transform transform) {
//         this.mesh = mesh;
//         this.transform = transform;
//     }
// }

public class MeshManager : Singleton<MeshManager> {
    Dictionary<string, MeshGroupRenderer> ivyMeshGroupRenderers;
    Dictionary<string, MeshGroupRenderer> treeMeshGroupRenderers;
    Dictionary<string, MeshGroupRenderer> fractureMeshGroupRenderers;
    GameObject ivyMeshParent;
    GameObject fractureMeshParent;
    GameObject TreeMeshParent;

    public void addMesh(Transform t, Mesh mesh, Material material) {
        if (ivyMeshParent == null) {
            ivyMeshParent = new GameObject("meshParent");
        }

        if (ivyMeshGroupRenderers == null) {
            ivyMeshGroupRenderers = new Dictionary<string, MeshGroupRenderer>();
        }

        if (ivyMeshGroupRenderers.ContainsKey(material.name)) {
            ivyMeshGroupRenderers[material.name].add(t, mesh, material);
        } else {
            GameObject render = new GameObject("meshGroup - " + material.name);
            print("new object:" + material.name);
            render.transform.SetParent(ivyMeshParent.transform);

            MeshFilter mFilter = render.AddComponent<MeshFilter>();
            MeshRenderer mRenderer = render.AddComponent<MeshRenderer>();

            MeshGroupRenderer groupRenderer = render.AddComponent<MeshGroupRenderer>();
            groupRenderer.meshFilter = mFilter;
            groupRenderer.meshRenderer = mRenderer;
            groupRenderer.add(t, mesh, material);
            ivyMeshGroupRenderers.Add(material.name, groupRenderer);
        }

    }

    public void addFractureMesh(Transform t, Mesh mesh, Material material)
    {
        if (fractureMeshParent == null)
        {
            fractureMeshParent = new GameObject("FractureMeshParent");
        }

        if (fractureMeshGroupRenderers == null)
        {
            fractureMeshGroupRenderers = new Dictionary<string, MeshGroupRenderer>();
        }

        if (fractureMeshGroupRenderers.ContainsKey(material.name))
        {
            fractureMeshGroupRenderers[material.name].add(t, mesh, material);
        }
        else
        {
            GameObject render = new GameObject("meshGroup - " + material.name);
            print("new object:" + material.name);
            render.transform.SetParent(fractureMeshParent.transform);

            MeshFilter mFilter = render.AddComponent<MeshFilter>();
            MeshRenderer mRenderer = render.AddComponent<MeshRenderer>();

            MeshGroupRenderer groupRenderer = render.AddComponent<MeshGroupRenderer>();
            groupRenderer.meshFilter = mFilter;
            groupRenderer.meshRenderer = mRenderer;
            groupRenderer.add(t, mesh, material);
            fractureMeshGroupRenderers.Add(material.name, groupRenderer);
        }

    }    
    public void addTreeMesh(Transform t, Mesh mesh, Material material, Transform[] VFX)
    {
        if (TreeMeshParent == null)
        {
            TreeMeshParent = new GameObject("TreeMeshParent");
        }

        if (treeMeshGroupRenderers == null)
        {
            treeMeshGroupRenderers = new Dictionary<string, MeshGroupRenderer>();
        }

        if (treeMeshGroupRenderers.ContainsKey(material.name))
        {
            treeMeshGroupRenderers[material.name].add(t, mesh, material);
        }
        else
        {
            GameObject render = new GameObject("meshGroup - " + material.name);
            print("new object:" + material.name);
            render.transform.SetParent(TreeMeshParent.transform);

            MeshFilter mFilter = render.AddComponent<MeshFilter>();
            MeshRenderer mRenderer = render.AddComponent<MeshRenderer>();

            MeshGroupRenderer groupRenderer = render.AddComponent<MeshGroupRenderer>();
            groupRenderer.meshFilter = mFilter;
            groupRenderer.meshRenderer = mRenderer;
            groupRenderer.add(t, mesh, material);
            treeMeshGroupRenderers.Add(material.name, groupRenderer);
        }

        foreach(Transform vfx  in VFX)
        {
            vfx.transform.parent = TreeMeshParent.transform;
        }

    }

    public void combineAll() {
        if (ivyMeshGroupRenderers != null) {
            foreach (var group in ivyMeshGroupRenderers) {
                group.Value.combineAndRender();
            }
            ivyMeshGroupRenderers.Clear();
            Resources.UnloadUnusedAssets();
        }
    }

    public void  combineAllFractured()
    {
        if (fractureMeshGroupRenderers != null)
        {
            foreach (var group in fractureMeshGroupRenderers)
            {
                group.Value.combineAndRender();
            }
            fractureMeshGroupRenderers.Clear();
            Resources.UnloadUnusedAssets();
        }
    }

    public void combineAllTrees()
    {
        if (treeMeshGroupRenderers != null)
        {
            foreach (var group in treeMeshGroupRenderers)
            {
                group.Value.combineAndRender();
            }
            treeMeshGroupRenderers.Clear();
            Resources.UnloadUnusedAssets();
        }
    }

}