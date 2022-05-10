using UnityEngine;
using UnityEngine.UI;
using ProceduralModeling;

public class PlantSelection : MonoBehaviour
{
    public Image IvySprite;
    public Image TreeSprite;

    PTGarden[] treeTool;
    ProceduralIvy ivyTool;

    private void Start()
    {
        treeTool = FindObjectsOfType<PTGarden>();
        ivyTool = FindObjectOfType<ProceduralIvy>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            IvySprite.enabled = ivyTool .enabled = true;

            foreach(PTGarden surfaces in treeTool)
            {
                TreeSprite.enabled = surfaces.enabled = false;
            }
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            IvySprite.enabled = ivyTool.enabled = false;

            foreach (PTGarden surfaces in treeTool)
            {
                TreeSprite.enabled = surfaces.enabled = true;
            }
        }
    }


}
