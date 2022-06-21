using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;


public class SizeBinder : MonoBehaviour
{
    public float effectSize;
    public Texture2D leafTexture;
    public Gradient leafGradient;

    private void Start()
    {
        if (GetComponent<VisualEffect>() != null)
        {
            GetComponent<VisualEffect>().SetTexture("LeafTexture", leafTexture);
            GetComponent<VisualEffect>().SetGradient("LeafGradient", leafGradient);

        }
    }
    private void Update()
    {
        if(GetComponent<VisualEffect>() != null)
        {
            GetComponent<VisualEffect>().SetFloat("Size", effectSize);
            GetComponent<VisualEffect>().SetTexture("LeafTexture", leafTexture);
            GetComponent<VisualEffect>().SetGradient("LeafGradient", leafGradient);

        }
    }
}