using System.Linq;
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
    [SerializeField] Material grassMaterial;
    // Update is called once per frame
    void Update()
    {
        if(timer >= spawnInterval && grassCount < grassPerSpot)
        {
            float spawnX = Random.Range(transform.position.x - radius, transform.position.x + radius);
            float spawnZ = Random.Range(transform.position.z - radius, transform.position.z + radius);
            Debug.Log("checking spot " + spawnX + " , " + spawnZ);

            RaycastHit hit;
            if(Physics.Raycast(new Vector3(spawnX, transform.position.y + 1, spawnZ), Vector3.down, out hit))
            {
                if(hit.transform.CompareTag("Ground"))
                {
                    if(grass != null)
                    {
                        Instantiate(grass, new Vector3(spawnX, hit.point.y, spawnZ), Quaternion.identity, transform);
                        grass.GetComponent<MeshRenderer>().material = grassMaterial;
                        grassCount++;
                    }
                }               
            }
            

            Debug.Log("Planting grass");
            timer = 0;            
        }

        if(grassCount >= grassPerSpot)
        {
            combineAndClear();
        }

        timer += Time.deltaTime;
    }

    void combineAndClear()
    {
        MeshManager.instance.combineAllGrass();
        Destroy(gameObject);
    }
}
