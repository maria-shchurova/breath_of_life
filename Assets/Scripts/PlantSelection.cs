using UnityEngine;
using UnityEngine.UI;
using ProceduralModeling;

public class PlantSelection : MonoBehaviour
{
    public Image IvySprite;
    public Image TreeSprite;
    public Image GrassSprite;

    PTGarden[] treeTool;
    ProceduralIvy ivyTool;
    GrassPlant grassTool;

    private void Start()
    {
        treeTool = FindObjectsOfType<PTGarden>();
        ivyTool = FindObjectOfType<ProceduralIvy>();
        grassTool = FindObjectOfType<GrassPlant>();


        IvySprite.color = new Color(1, 1, 1, 1f);
        TreeSprite.color = new Color(1, 1, 1, 0.5f);
        GrassSprite.color = new Color(1, 1, 1, 0.5f);
        ivyTool.enabled = true;
        grassTool.enabled = false;

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            ivyTool.enabled = true;
            grassTool.enabled = false;

            IvySprite.color = new Color(1, 1, 1, 1f);
            TreeSprite.color = new Color(1, 1, 1, 0.5f);
            GrassSprite.color = new Color(1, 1, 1, 0.5f);

            foreach (PTGarden surfaces in treeTool)
            {
                surfaces.enabled = false;
            }
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            grassTool.enabled = true;
            ivyTool.enabled = false;

            IvySprite.color = new Color(1, 1, 1, 0.5f);
            TreeSprite.color = new Color(1, 1, 1, 0.5f);
            GrassSprite.color = new Color(1, 1, 1, 1f);

            foreach (PTGarden surfaces in treeTool)
            {
                surfaces.enabled = false;
            }
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            ivyTool.enabled = false;
            grassTool.enabled = false;

            IvySprite.color = new Color(1, 1, 1, 0.5f);
            TreeSprite.color = new Color(1, 1, 1, 1f);
            GrassSprite.color = new Color(1, 1, 1, 0.5f);

            foreach (PTGarden surfaces in treeTool)
            {
                surfaces.enabled = true;
            }
        }

      
    }


}
