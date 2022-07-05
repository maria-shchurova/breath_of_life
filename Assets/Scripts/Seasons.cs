using System.Linq;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine;
using ProceduralModeling;

public class Seasons : MonoBehaviour
{
    [Header("Ground Materials")]
    [SerializeField] Material SpringGround;
    [SerializeField] Material SummerGround;
    [SerializeField] Material AutumnGround;
    [SerializeField] MeshRenderer Ground;
    [Space]
    [Header("Volumes")]
    [SerializeField] Volume ActiveVolume;
    [SerializeField] VolumeProfile Spring;
    [SerializeField] VolumeProfile Summer;
    [SerializeField] VolumeProfile Autumn;
    [Space]
    [Header("Lights")]
    [SerializeField] GameObject SpringSun;
    [SerializeField] GameObject SummerSun;
    [SerializeField] GameObject AutumnSun;


    [Space]
    [Header("Grass spawner prefabs")]
    [SerializeField] GameObject grassSpring;
    [SerializeField] GameObject grassSummer;
    [SerializeField] GameObject grassAutumn;

    [Space]
    [Header("Ivy materials")]
    [SerializeField] Material[] ivySpringMaterials;
    [SerializeField] Material[] ivySummerMaterials;
    [SerializeField] Material[] ivyAutumnMaterials;

    [Space]
    [Header("Tree prefabs")]
    [SerializeField] List<GameObject> treeSpring;
    [SerializeField] List<GameObject> treeSummer;
    [SerializeField] List<GameObject> treeAutumn;
    public int season;

    PTGarden treeTool;
    ProceduralIvy ivyTool;
    GrassPlant grassTool;

    private void Start()
    {
        treeTool = FindObjectOfType<PTGarden>();
        ivyTool = FindObjectOfType<ProceduralIvy>();
        grassTool = FindObjectOfType<GrassPlant>();

        Messenger.AddListener<int>("SwitchSeason", SwitchSeason);
    }

    void Update()
    {

        //DEBUG TOOL
        //if(Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    if (season != 2)
        //        season++;
        //    else
        //        season = 0;

        //    SwitchSeason(season);
        //}
    }

    // Update is called once per frame
    void SwitchSeason(int season)
    {
        switch(season)
        {
            case 0: //Spring
                ApplyGroundMaterial(SpringGround);
                ActiveVolume.profile = Spring;
                SpringSun.SetActive(true);
                SummerSun.SetActive(false);
                AutumnSun.SetActive(false);

                ivyTool.leafMaterials = ivySpringMaterials;
                treeTool.prefabs = treeSpring;
                grassTool.grassSpawner = grassSpring;
                break;
            case 1: //Summer
                ApplyGroundMaterial(SummerGround);
                ActiveVolume.profile = Summer;
                SummerSun.SetActive(true);
                SpringSun.SetActive(false);
                AutumnSun.SetActive(false);

                ivyTool.leafMaterials = ivySummerMaterials;
                treeTool.prefabs = treeSummer;
                grassTool.grassSpawner = grassSummer;
                break;
            case 2: //Autumn
                ApplyGroundMaterial(AutumnGround);
                ActiveVolume.profile = Autumn;
                AutumnSun.SetActive(true);
                SpringSun.SetActive(false);
                SummerSun.SetActive(false);


                ivyTool.leafMaterials = ivyAutumnMaterials;
                treeTool.prefabs = treeAutumn;
                grassTool.grassSpawner = grassAutumn;
                break;
        }
    }

    void ApplyGroundMaterial(Material material)
    {

        var materials = Ground.materials;

        for (var i = 0; i < materials.Length; i++)
        {
            materials[i] = material;
        }

    }
}
