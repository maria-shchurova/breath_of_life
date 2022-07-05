using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreesMeshCombiner : MonoBehaviour
{
    List<GameObject> GrownTrees = new List<GameObject>();

    private void Update()
    {
        if(GrownTrees.Count > 2)
        {
            combineAndClear();
        }
    }
    public void AddTreeToList(GameObject tree)
    {
        GrownTrees.Add(tree);
    }
    void combineAndClear()
    {
        MeshManager.instance.combineAllTrees();
        foreach(GameObject tree  in GrownTrees.ToArray())
        {
            Destroy(tree);
        }
        GrownTrees.Clear();
    }
}
