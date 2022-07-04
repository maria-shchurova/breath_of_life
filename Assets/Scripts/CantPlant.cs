using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantPlant : MonoBehaviour
{
    [SerializeField] GameObject cantPlantSign;
    // Start is called before the first frame update
    void Start()
    {
        Messenger.AddListener("CantPlant", CantPlantShow);
    }

    void CantPlantShow()
    {
        Instantiate(cantPlantSign,transform);
    }
}
