using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCheater : MonoBehaviour
{
    public float acceleration;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            if((Time.timeScale + acceleration) < 100)
                Time.timeScale += acceleration;
        }
        else
            Time.timeScale = 1;
    }
}
