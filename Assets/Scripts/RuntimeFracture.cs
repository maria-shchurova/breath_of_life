using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        int objectCounter;
        bool doneFracturing;

        GameObject[] objectsToFracture; //size of this is number of breakable objects
        Text fracturingText;


        private void Start()
        {
            objectsToFracture = GameObject.FindGameObjectsWithTag("Fracture");           
            totalObjects = objectsToFracture.Length;
            fracturingText = GameObject.Find("FracturingText").GetComponent<Text>();
            Debug.LogWarning("number of objects: " + objectsToFracture.Length);
        }
        private void Update()
        {
            int objPerFrame = 0;
            if(doneFracturing == false)
            {
                while (objectCounter < totalObjects && objPerFrame < 3)
                {
                    FractureThis fracturing = objectsToFracture[objectCounter].AddComponent<FractureThis>();
                    fracturing.chunks = setChunks;
                    fracturing.density = setDensity;
                    fracturing.internalStrength = setInternalStrength;

                    MeshCollider mesh = objectsToFracture[objectCounter].AddComponent<MeshCollider>();
                    mesh.convex = true;
                    Rigidbody rigidbody = objectsToFracture[objectCounter].AddComponent<Rigidbody>();
                    rigidbody.isKinematic = true;

                    fracturingText.text = "Fracturing Objects: " + objectCounter + " / " + totalObjects;
                    objectCounter++;
                    objPerFrame++;
                    Debug.LogWarning("Fractured " + objectCounter + " objects");
                }

                

                if (objectCounter >= totalObjects)
                {
                    fracturingText.transform.gameObject.SetActive(false);
                    doneFracturing = true;
                    Debug.LogWarning("Done Fracturing");
                }
                    
            }

            brokenPercentage = (brokenObjects / totalObjects) * 100;
        }
    }
}
