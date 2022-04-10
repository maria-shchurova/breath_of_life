using UnityEngine;
using UnityEngine.UI;

public class IvySelection : MonoBehaviour
{
    Button redIvy;
    Button greenIvy;
    Button violetFlowers;
    Button whiteFlowers;

    ProceduralIvy IvyCreator;

    public Material red;
    public Material green;
    public Material flowerViolet;
    public Material flowerWhite;

    public CameraControl camControl;
    void Start()
    {
        redIvy = GameObject.Find("SelectRedIvy").GetComponent<Button>();
        greenIvy = GameObject.Find("SelectGreenIvy").GetComponent<Button>();
        violetFlowers = GameObject.Find("SelectVioletFlowers").GetComponent<Button>();
        whiteFlowers = GameObject.Find("SelectWhiteFlowers").GetComponent<Button>();

        IvyCreator = GameObject.Find("ProceduralIvy").GetComponent<ProceduralIvy>();


        redIvy.onClick.AddListener(SelectRedIvy);
        greenIvy.onClick.AddListener(SelectGreenIvy);
        violetFlowers.onClick.AddListener(SelectVioletFlowers);
        whiteFlowers.onClick.AddListener(SelectWhiteFlowers);
    }

    void SelectRedIvy()
    {
        IvyCreator.leafMaterial = red;
        camControl.enabled = true;
    }
    void SelectGreenIvy()
    {
        IvyCreator.leafMaterial = green;
        camControl.enabled = true;
    }    
    
    void SelectVioletFlowers()
    {
        IvyCreator.flowerMaterial = flowerViolet;
        camControl.enabled = true;
    }

    void SelectWhiteFlowers()
    {
        IvyCreator.flowerMaterial = flowerWhite;
        camControl.enabled = true;
    }
}
