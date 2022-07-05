using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassGrow : MonoBehaviour
{
    const string AMOUNT = "_Amount";
    public float currentAmount = -1;
    public float growthSpeed = 0.5f;
    float maxSize = 1;
    float grassSize;

    Material material;
    bool addedToCombiner =  false;
    // Start is called before the first frame update
    void Start()
    {
        grassSize = Random.Range(0.8f, 1.2f);
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

        material = GetComponent<MeshRenderer>().material;
        material.SetFloat(AMOUNT, currentAmount);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.x <= maxSize)
        {
            transform.localScale += new Vector3(0.01f * Time.deltaTime, 0.01f * Time.deltaTime, 0.01f * Time.deltaTime);
        }
        else
        {
            gameObject.GetComponent<GrassGrow>().enabled = false;
        }

        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.up, out hit) && hit.point.y - transform.position.y < grassSize)
        {
            maxSize = hit.point.y - transform.position.y;
        }
        else
        {
            maxSize = grassSize;
        }

        if(currentAmount < 1)
        {
            currentAmount += Time.deltaTime * growthSpeed;
            material.SetFloat(AMOUNT, currentAmount);
        }

        if(currentAmount >= 1 && addedToCombiner == false)
        {
            addedToCombiner = true;
            MeshManager.instance.addGrassMesh(transform, GetComponent<MeshFilter>().mesh, GetComponent<MeshRenderer>().sharedMaterial);
        }
    }
}
