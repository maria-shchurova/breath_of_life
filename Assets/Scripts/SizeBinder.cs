using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;


public class SizeBinder : MonoBehaviour
{
    [SerializeField] VisualEffect visualEffect;
    public float effectSize;

    private void Start()
    {
        visualEffect = GetComponent<VisualEffect>();
    }
    private void Update()
    {
        visualEffect.SetFloat("Size", effectSize);
    }
}