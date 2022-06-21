using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine;

public class Seasons : MonoBehaviour
{
    [SerializeField] Material SpringGround;
    [SerializeField] Material SummerGround;
    [SerializeField] Material AutumnGround;

    [SerializeField] MeshRenderer Ground;
    [SerializeField] Volume ActiveVolume;

    [SerializeField] VolumeProfile Spring;
    [SerializeField] VolumeProfile Summer;
    [SerializeField] VolumeProfile Autumn;

    [SerializeField] GameObject SpringSun;
    [SerializeField] GameObject SummerSun;
    [SerializeField] GameObject AutumnSun;

    public int debugInt;

    //TODO plants materials
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            SwitchSeason(debugInt);
        }
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
                break;
            case 1:
                ApplyGroundMaterial(SummerGround);
                ActiveVolume.profile = Summer;
                SummerSun.SetActive(true);
                SpringSun.SetActive(false);
                AutumnSun.SetActive(false);
                break;
            case 2:
                ApplyGroundMaterial(AutumnGround);
                ActiveVolume.profile = Autumn;
                AutumnSun.SetActive(true);
                SpringSun.SetActive(false);
                SummerSun.SetActive(false);
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
