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
    }
    void SelectGreenIvy()
    {
        IvyCreator.leafMaterial = green;
    }    
    
    void SelectVioletFlowers()
    {
        IvyCreator.flowerMaterial = flowerViolet;
    }

    void SelectWhiteFlowers()
    {
        IvyCreator.flowerMaterial = flowerWhite;
    }
}
