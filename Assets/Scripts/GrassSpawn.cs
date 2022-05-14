using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSpawn: MonoBehaviour
{
    Vector3 corner1;
    Vector3 corner2;

    float timer;
    [SerializeField] float spawnInterval;
    [SerializeField] GameObject grass;
    [SerializeField] GameObject grassParent;

    // Start is called before the first frame update
    void Start()
    {
        corner1 = transform.GetChild(0).position;
        corner2 = transform.GetChild(1).position;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= spawnInterval)
        {
            float spawnX = Random.Range(corner1.x, corner2.x);
            float spawnZ = Random.Range(corner1.z, corner2.z);
            Debug.Log("checking spot " + spawnX + " , " + spawnZ);

            if (!(Physics.Raycast(new Vector3(spawnX, -1, spawnZ), Vector3.up, 2f)))
            {
                GameObject.Instantiate(grass, new Vector3(spawnX, -1, spawnZ), Quaternion.identity, grassParent.transform);
            }

            Debug.Log("Planting grass");
            timer = 0;
        }

        timer += Time.deltaTime;
    }
}
