using UnityEngine;
using UnityEngine.UI;
using ProceduralModeling;

public class PlantSelection : MonoBehaviour
{
    GameObject[] tools;

    PTGarden[] treeTool;
    ProceduralIvy ivyTool;
    GrassPlant grassTool;

    private void Start()
    {
        tools = new GameObject[3] { GameObject.Find("Ivy"), GameObject.Find("Tree"), GameObject.Find("Grass") };

        treeTool = FindObjectsOfType<PTGarden>();
        ivyTool = FindObjectOfType<ProceduralIvy>();
        grassTool = FindObjectOfType<GrassPlant>();

        Select(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Select(0);
            

            ivyTool.enabled = true;
            grassTool.enabled = false;

            foreach (PTGarden surfaces in treeTool)
            {
                surfaces.enabled = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Select(1);
            

            ivyTool.enabled = false;
            grassTool.enabled = false;

            foreach (PTGarden surfaces in treeTool)
            {
                surfaces.enabled = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Select(2);
            

            grassTool.enabled = true;
            ivyTool.enabled = false;

            foreach (PTGarden surfaces in treeTool)
            {
                surfaces.enabled = false;
            }
        }
    }

    void Select(int tool)
    {
        for(int i = 0; i < tools.Length; i ++)
        {
            if(i > tool)
            {
                tools[i].transform.localPosition = new Vector3(i*50 + 50, 0);
            }
            else if(i <= tool)
            {
                tools[i].transform.localPosition = new Vector3(i * 50, 0);
            }

            ResetTriggers(tools[i]);
            tools[i].GetComponentInChildren<Animator>().SetTrigger(tools[tool].name);
        }        
    }

    void ResetTriggers(GameObject toolReset)
    {
        toolReset.GetComponentInChildren<Animator>().ResetTrigger("Ivy");
        toolReset.GetComponentInChildren<Animator>().ResetTrigger("Grass");
        toolReset.GetComponentInChildren<Animator>().ResetTrigger("Tree");
    }
}
