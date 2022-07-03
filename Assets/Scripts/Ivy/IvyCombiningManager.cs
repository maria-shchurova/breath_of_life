using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvyCombiningManager : MonoBehaviour
{
    public List<Branch> thisIvyBranches = new List<Branch>();
    public List<Blossom>  thisIvyBlossoms = new List<Blossom>();

    bool BranchesReady = false;
    bool BlossomsReady = false;

    public void AddBranch(Branch  newBranch)
    {
        thisIvyBranches.Add(newBranch);
    }

    public void AddBlossom(Blossom newBlossom)
    {
        thisIvyBlossoms.Add(newBlossom);
    }

    // Update is called once per frame
    void Update()
    {
        if(GrowingFinished() == true)
        {
            //Messenger.Broadcast("CombineIvy");
        }
    }

    bool GrowingFinished()
    {
        foreach(Branch branch in thisIvyBranches)
        {
            BranchesReady = !branch.Animate();  //if all are  animated, it will stary true from the beginning of loop till the end
        }

        foreach (Blossom blossom in thisIvyBlossoms)
        {
            BlossomsReady = !blossom.Animate();  //if all are  animated, it will stary true from the beginning of loop till the end
        }

        if (BranchesReady == true && BlossomsReady == true)
        {
            return true;
        }
        else
            return false;
    }
}
