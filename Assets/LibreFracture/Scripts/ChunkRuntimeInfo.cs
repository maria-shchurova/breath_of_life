using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ChunkRuntimeInfo : MonoBehaviour
{
    public List<Rigidbody> topConnections = new List<Rigidbody>();
    public List<Rigidbody> bottomConnections = new List<Rigidbody>();
    public List<Rigidbody> forwardConnections = new List<Rigidbody>();
    public List<Rigidbody> backwardConnections = new List<Rigidbody>();
    public List<Rigidbody> leftConnections = new List<Rigidbody>();
    public List<Rigidbody> rightConnections = new List<Rigidbody>();

    bool collidersEnabled = true;
    float passedTime;

    new Rigidbody rigidbody;
    FractureObject fractureObject;

    public bool isBrokenOff { get; set; }

    public UnityAction<ChunkRuntimeInfo> onFracture;

    public void Init()
    {
        SwitchColliders(collidersEnabled);
        rigidbody = GetComponent<Rigidbody>();
        fractureObject = transform.parent.GetComponent<FractureObject>();
    }

    private void Update()
    {
        if(rigidbody)
        {
            if (
            topConnections.Count == 0 ||
            bottomConnections.Count == 0 ||
            forwardConnections.Count == 0 ||
            backwardConnections.Count == 0 ||
            leftConnections.Count == 0 ||
            rightConnections.Count == 0)
            {
                rigidbody.isKinematic = false;
                SwitchColliders(true);
            }
            else // happens if surrounded from all sides
            {
                rigidbody.isKinematic = true;
                SwitchColliders(false);
            }

            if (GetComponents<FixedJoint>().Length == 0 && !isBrokenOff)
            {
                rigidbody.isKinematic = false;
                gameObject.layer = 0;
                onFracture.Invoke(this);
                isBrokenOff = true;
            }

            if (collidersEnabled)
            {
                passedTime += Time.deltaTime;
            }

            if (passedTime >= 10)
            {
                rigidbody.isKinematic = true;
            }

        }

    }

    void SwitchColliders(bool enabled)
    {
        if (collidersEnabled != enabled)
        {
            foreach (Collider collider in GetComponents<Collider>())
            {
                collider.enabled = enabled;
            }
        }
    }

    public void OnJointRemove(Rigidbody connectedBody)
    {
        topConnections.Remove(connectedBody);
        bottomConnections.Remove(connectedBody);
        forwardConnections.Remove(connectedBody);
        backwardConnections.Remove(connectedBody);
        leftConnections.Remove(connectedBody);
        rightConnections.Remove(connectedBody);

        rigidbody.ResetCenterOfMass();

        MeshManager.instance.addFractureMesh(transform, GetComponent<MeshFilter>().mesh, GetComponent<MeshRenderer>().sharedMaterial);

    }

    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.relativeVelocity.magnitude >= fractureObject.chunkCollisionVelocityToCreateVFX)
        //{
        //    if(fractureObject.onChunkCollideVFX)
        //        Instantiate(fractureObject.onChunkCollideVFX);
        //}

        if(collision.transform.CompareTag("Armature") && passedTime <= 1)
        {
            if (Random.Range(0, 2) == 0)
                rigidbody.isKinematic = true;
        }

    }

}
