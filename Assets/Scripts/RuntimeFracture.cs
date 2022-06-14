using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Fractures
{
    public class RuntimeFracture : MonoBehaviour
    {
        [SerializeField] int setChunks = 10;
        [SerializeField] float setDensity = 50;
        [SerializeField] float setInternalStrength = 100;
        public float brokenPercentage;
        public float brokenObjects;
        float totalObjects;

        GameObject[] objectsToFracture; //size of this is number of breakable objects


        private void Start()
        {
            objectsToFracture = GameObject.FindGameObjectsWithTag("Fracture");
            for (int i = 0; i < objectsToFracture.Length; i++)
            {
                FractureThis fracturing = objectsToFracture[i].AddComponent<FractureThis>();
                fracturing.chunks = setChunks;
                fracturing.density = setDensity;
                fracturing.internalStrength = setInternalStrength;

                MeshCollider mesh = objectsToFracture[i].AddComponent<MeshCollider>();
                mesh.convex = true;
                Rigidbody rigidbody = objectsToFracture[i].AddComponent<Rigidbody>();
                rigidbody.isKinematic = true;
            }
            totalObjects = objectsToFracture.Length;
            Debug.LogWarning("number of objects: " + objectsToFracture.Length);
        }
        private void Update()
        {
            brokenPercentage = (brokenObjects / totalObjects) * 100;
        }
    }
}
