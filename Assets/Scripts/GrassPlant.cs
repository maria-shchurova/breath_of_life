using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassPlant : MonoBehaviour
{
    public GameObject grassSpawner;
    [SerializeField] GameObject grassParent;
    [SerializeField] GameObject impactEffect;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Instantiate(impactEffect, hit.point, Quaternion.identity);
                SpawnGrass(hit);
            }
        }
    }

    void SpawnGrass(RaycastHit place)
    {
        Instantiate(grassSpawner, place.point, new Quaternion(), grassParent.transform);
    }
}
