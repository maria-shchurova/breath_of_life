using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowForce : MonoBehaviour
{
    [SerializeField] GameObject breaker;
    [SerializeField] float mass  = 1.5f;
    [SerializeField] float raduis  = 0.05f;
    [SerializeField] float force = 0;
    [SerializeField] float maxForce = 5.5f;

    [SerializeField] float timeInterval = 5;
    float currentTime;

    private void Start()
    {
        currentTime = timeInterval;
    }
    void Instantiate()
    {
        breaker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        breaker.GetComponent<MeshRenderer>().enabled = false;
        breaker.transform.position = transform.position;
        breaker.transform.rotation = transform.rotation;
        breaker.transform.localScale = new Vector3(raduis, raduis, raduis);
        var  rb  = breaker.AddComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        breaker.AddComponent<SphereCollider>().radius = raduis;
        rb.mass = mass;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            if (breaker == null)
                Instantiate();
            else
            {
                if(force< maxForce)
                    force += 0.25f;

                breaker.transform.position = transform.position;
                breaker.transform.rotation = transform.rotation;
                breaker.GetComponent<Rigidbody>().AddForce(breaker.transform.up * -1 * force, ForceMode.Impulse);
            }
            currentTime = timeInterval;
        }
    }
}
