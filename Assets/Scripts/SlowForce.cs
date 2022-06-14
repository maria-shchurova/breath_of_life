using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowForce : MonoBehaviour
{
    GameObject breaker;
    float mass;
    float raduis;
    float force;
    float maxForce;

    float timeInterval = 5;
    float currentTime;

    Vector3 direction;
    public void Init(float _mass, float _radius, float _force, float _maxforce, float _timeInterval,Vector3 _direction)
    {
        mass = _mass;
        raduis = _radius;
        force = _force;
        maxForce = _maxforce;
        timeInterval = _timeInterval;
        direction = _direction;

        currentTime = timeInterval;

        Instantiate();
    }
    void Instantiate()
    {
        breaker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        breaker.GetComponent<MeshRenderer>().enabled = false;
        breaker.transform.position = transform.position;
        breaker.transform.rotation = transform.rotation;
        breaker.GetComponent<MeshRenderer>().forceRenderingOff = true;
        breaker.layer = 2;
        breaker.transform.localScale = new Vector3(raduis, raduis, raduis);
        var rb = breaker.AddComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
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
                if (force < maxForce)
                    force += 0.5f;

                breaker.transform.position = transform.position;
                breaker.transform.rotation = transform.rotation;
                breaker.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);
            }
            currentTime = timeInterval;
        }
    }
}
