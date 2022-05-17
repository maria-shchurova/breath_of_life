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

        IvySprite.color = new Color(1, 1, 1, 1f);
        TreeSprite.color = new Color(1, 1, 1, 0.5f);
        ivyTool.enabled = true;

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            ivyTool.enabled = true;

            IvySprite.color = new Color(1, 1, 1, 1f);
            TreeSprite.color = new Color(1, 1, 1, 0.5f);

            foreach(PTGarden surfaces in treeTool)
            {
                surfaces.enabled = false;
            }
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            ivyTool.enabled = false;

            IvySprite.color = new Color(1, 1, 1, 0.5f);
            TreeSprite.color = new Color(1, 1, 1, 1f);

            foreach (PTGarden surfaces in treeTool)
            {
                surfaces.enabled = true;
            }
        }
    }


}
