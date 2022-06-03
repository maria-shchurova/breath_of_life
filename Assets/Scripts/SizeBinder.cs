using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;


public class SizeBinder : MonoBehaviour
{
    public float effectSize;

    private void Update()
    {
        if(GetComponent<VisualEffect>() != null)
            GetComponent<VisualEffect>().SetFloat("Size", effectSize);
    }
}