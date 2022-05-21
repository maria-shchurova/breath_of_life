using UnityEngine;
using UnityEngine.VFX;

public class TreeVFXMeneager : MonoBehaviour
{
    public float growthSpeed = 0.5f;
    public VisualEffect VF;
    void Start()
    {
        transform.localScale = new Vector3(0, 0, 0);
        VF.playRate = 0;
    }

    void Update()
    {
        if (transform.localScale.x <= 1)
        {
            transform.localScale += new Vector3(growthSpeed * Time.deltaTime, growthSpeed * Time.deltaTime, growthSpeed * Time.deltaTime);
            VF.playRate += Time.deltaTime * growthSpeed;
        }
    }
}
