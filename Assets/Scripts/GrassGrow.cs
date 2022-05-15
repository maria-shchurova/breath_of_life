using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassGrow : MonoBehaviour
{
    const string AMOUNT = "_Amount";
    public float currentAmount = -1;
    public float growthSpeed = 0.5f;

    Material material;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

        material = GetComponent<MeshRenderer>().material;
        material.SetFloat(AMOUNT, currentAmount);

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.x <= 1)
        {
            transform.localScale += new Vector3(0.01f * Time.deltaTime, 0.01f * Time.deltaTime, 0.01f * Time.deltaTime);
        }

        if(currentAmount < 1)
        {
            currentAmount += Time.deltaTime * growthSpeed;
            material.SetFloat(AMOUNT, currentAmount);
        }
    }
}
