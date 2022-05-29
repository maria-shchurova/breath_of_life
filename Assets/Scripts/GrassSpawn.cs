using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSpawn: MonoBehaviour
{
    float timer;
    int grassCount;
    [SerializeField] float radius;
    [SerializeField] int grassPerSpot;
    [SerializeField] float spawnInterval;
    [SerializeField] GameObject grass;

    // Update is called once per frame
    void Update()
    {
        if(timer >= spawnInterval && grassCount < grassPerSpot)
        {
            float spawnX = Random.Range(transform.position.x - radius, transform.position.x + radius);
            float spawnZ = Random.Range(transform.position.z - radius, transform.position.z + radius);
            Debug.Log("checking spot " + spawnX + " , " + spawnZ);

            if (!(Physics.Raycast(new Vector3(spawnX, 0, spawnZ), Vector3.up, 2f)))
            {
                GameObject.Instantiate(grass, new Vector3(spawnX, 0, spawnZ), Quaternion.identity, transform);
                grassCount++;
            }

            Debug.Log("Planting grass");
            timer = 0;            
        }

        timer += Time.deltaTime;
    }
}
