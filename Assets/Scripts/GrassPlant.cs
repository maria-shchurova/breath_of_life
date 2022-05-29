using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassPlant : MonoBehaviour
{
    [SerializeField] GameObject grassSpawner;
    [SerializeField] GameObject grassParent;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                SpawnGrass(hit);
            }
        }
    }

    void SpawnGrass(RaycastHit place)
    {
        GameObject.Instantiate(grassSpawner, place.point, new Quaternion(), grassParent.transform);
    }
}
