using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressTracker : MonoBehaviour
{
    Project.Scripts.Fractures.RuntimeFracture fractureprogress;
    Color[] seasonColors;
    Seasons seasonManager;
    Image blackout;
    string season = "spring";

    // Start is called before the first frame update
    void Start()
    {
        fractureprogress = FindObjectOfType<Project.Scripts.Fractures.RuntimeFracture>();
        seasonManager = FindObjectOfType<Seasons>();
        blackout = GameObject.Find("Blackout").GetComponent<Image>();
        blackout.CrossFadeAlpha(0, 0, false);

        seasonColors = new Color[] { new Color(0.5882353f, 0.8823529f, 0.2745098f), 
                                     new Color(0.3137255f, 0.8627451f, 0.1176471f), 
                                     new Color(0.8039216f, 0.8823529f, 0.2352941f) };
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(fractureprogress.brokenPercentage / 100, 1);

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Intro")
        {
            if(season != "spring")
            {
                SeasonChange(0);
            }

            season = "spring";
        }
        else if(fractureprogress.brokenPercentage >= 33 && fractureprogress.brokenObjects < 66)
        {
            if (season != "summer")
            {
                SeasonChange(1);
            }

            season = "summer";
        }
        else if(fractureprogress.brokenPercentage >= 66)
        {
            if (season != "autumn")
            {
                SeasonChange(2);
            }

            season = "autumn";
        }
        
        

        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Intro")
        {
            if (fractureprogress.brokenPercentage >= 80)
            {
                Messenger.Broadcast("IntroCompleted");
            }
        }
    }

    async void SeasonChange(int seasonInt)
    {       
        blackout.CrossFadeAlpha(1, 1, false);
        Debug.Log("Blackout");

        transform.GetComponent<Image>().color = seasonColors[seasonInt];
        seasonManager.season = seasonInt;

        await System.Threading.Tasks.Task.Delay(1500);
        BlackFadeOut();
    }

    void BlackFadeOut()
    {        
        blackout.CrossFadeAlpha(0, 1, false);
        Debug.Log("BlackoutOff " + blackout.color.a);
    }
}
