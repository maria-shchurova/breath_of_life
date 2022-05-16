using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class KillAfterPlay : MonoBehaviour
{

    private void OnDisable()
    {
        Destroy(gameObject);
    }
}


