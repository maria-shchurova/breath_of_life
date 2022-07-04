using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRemove : MonoBehaviour
{
    [SerializeField] float deleteInterval = 2;
    float timeAlive;
    // Update is called once per frame
    void Update()
    {
        if (timeAlive >= deleteInterval)
            GameObject.Destroy(gameObject);
        timeAlive += Time.deltaTime;
    }
}
