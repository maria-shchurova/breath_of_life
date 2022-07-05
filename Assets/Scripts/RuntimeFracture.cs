using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        TMP_Text fracturingText;


        private void Start()
        {
            objectsToFracture = GameObject.FindGameObjectsWithTag("Fracture");           
            totalObjects = objectsToFracture.Length;
            fracturingText = GameObject.Find("FracturingText").GetComponent<TMP_Text>();
            Debug.LogWarning("number of objects: " + objectsToFracture.Length);

            Messenger.AddListener("CombineFractured", combineAndClear);
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

                    if(fracturingText)
                        fracturingText.text = Mathf.RoundToInt((objectCounter / totalObjects) * 100) + "%";

                    objectCounter++;
                    objPerFrame++;
                    Debug.LogWarning("Fractured " + objectCounter + " objects");
                }

                

                if (objectCounter >= totalObjects)
                {
                    if (fracturingText)
                        fracturingText.transform.gameObject.SetActive(false);
                    doneFracturing = true;
                    Messenger.Broadcast("Done Fracturing");                    
                }
                    
            }

            brokenPercentage = (brokenObjects / totalObjects) * 100;

            if (brokenPercentage > oldPercentage)
                objectsBroken = true;

            oldPercentage = brokenPercentage;
        }

        void combineAndClear()
        {
            MeshManager.instance.combineAllFractured();
            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
            }
        }
    }
}
