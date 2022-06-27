using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    [SerializeField] GameObject Cutout;
    [SerializeField] GameObject Hint;

    bool one_wasPressed;
    bool two_wasPressed;
    bool three_wasPressed;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { one_wasPressed = true; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { two_wasPressed = true; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { three_wasPressed = true; }


            if (one_wasPressed == true && two_wasPressed == true && three_wasPressed == true)
        {
            Cutout.SetActive(false);
            Hint.SetActive(false);

            Destroy(this);
        }
    }
}
