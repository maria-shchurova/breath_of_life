using UnityEngine;
using UnityEngine.UI;

public class PlantSelection : MonoBehaviour
{
    public Image IvySprite;
    public Image TreeSprite;

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            IvySprite.enabled = true;
            TreeSprite.enabled = false;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            IvySprite.enabled = false;
            TreeSprite.enabled = true;
        }
    }


}
