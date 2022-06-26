using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressTracker : MonoBehaviour
{
    Project.Scripts.Fractures.RuntimeFracture fractureprogress;
    Color spring;
    Color summer;
    Color autumn;
    Seasons seasonManager;

    // Start is called before the first frame update
    void Start()
    {
        fractureprogress = FindObjectOfType<Project.Scripts.Fractures.RuntimeFracture>();
        seasonManager = FindObjectOfType<Seasons>();

        spring = new Color(0.5882353f, 0.8823529f, 0.2745098f);
        summer = new Color(0.3137255f, 0.8627451f, 0.1176471f);
        autumn = new Color(0.8039216f, 0.8823529f, 0.2352941f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(fractureprogress.brokenPercentage / 100, 1);

        if(fractureprogress.brokenPercentage < 33)
        {
            transform.GetComponent<Image>().color = spring;
            seasonManager.season = 0;
        }
        else if(fractureprogress.brokenPercentage >= 33 && fractureprogress.brokenObjects < 66)
        {
            transform.GetComponent<Image>().color = summer;
            seasonManager.season = 1;
        }
        else if(fractureprogress.brokenPercentage >= 66)
        {
            transform.GetComponent<Image>().color = autumn;
            seasonManager.season = 2;
        }

        if(fractureprogress.objectsBroken)
        {
            transform.GetComponentInChildren<ParticleSystem>().Play();
            fractureprogress.objectsBroken = false;
        }
    }
}
