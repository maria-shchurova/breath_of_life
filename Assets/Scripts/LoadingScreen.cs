using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] GameObject MainCamera;
    void Start()
    {
        Messenger.AddListener("Done Fracturing", Fade);
    }

    public void Fade()
    {
        GetComponent<Animator>().SetTrigger("Fade");
        MainCamera.SetActive(true);
        Destroy(gameObject, 2);
    }
}
