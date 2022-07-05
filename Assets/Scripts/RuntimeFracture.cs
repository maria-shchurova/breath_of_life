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
        float oldPercentage;
        int objectCounter;
        bool doneFracturing;
        public bool objectsBroken;

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
                    MeshCollider mesh = objectsToFracture[objectCounter].AddComponent<MeshCollider>();
                    mesh.convex = true;
                    Rigidbody rigidbody = objectsToFracture[objectCounter].AddComponent<Rigidbody>();
                    rigidbody.isKinematic = true;
                    Debug.Log("added rigidbody to " + objectsToFracture[objectCounter].name);

                    FractureThis fracturing = objectsToFracture[objectCounter].AddComponent<FractureThis>();
                    fracturing.chunks = setChunks;
                    fracturing.density = setDensity;
                    fracturing.internalStrength = setInternalStrength;

                    fracturingText.text = "Fracturing Objects: " + objectCounter + " / " + totalObjects;
                    objectCounter++;
                    objPerFrame++;
                    Debug.LogWarning("Fractured " + objectCounter + " objects");
                }

                

                if (objectCounter >= totalObjects)
                {
                    fracturingText.transform.gameObject.SetActive(false);
                    doneFracturing = true;
                    Messenger.Broadcast("Done Fracturing");                    
                }
                    
            }

            brokenPercentage = (brokenObjects / totalObjects) * 100;

            if (brokenPercentage > oldPercentage)
                objectsBroken = true;

            oldPercentage = brokenPercentage;

            if(Input.GetKeyUp(KeyCode.Space))
            {
                combineAndClear();
            }
        }

        void combineAndClear()
        {
            MeshManager.instance.combineAllChunks();
            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
            }
        }
    }
}
