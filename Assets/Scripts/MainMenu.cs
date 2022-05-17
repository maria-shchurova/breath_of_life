using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private GameObject PlayButton1;
    private GameObject PlayButton2;
    private GameObject CreditsButton;
    private GameObject QuitButton;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        PlayButton1 = GameObject.Find("Play Level 1");
        PlayButton2 = GameObject.Find("Play Level 2");
        CreditsButton = GameObject.Find("Credits");
        QuitButton = GameObject.Find("Quit");

        PlayButton1.GetComponent<Button>().onClick.AddListener(Play_1);
        PlayButton2.GetComponent<Button>().onClick.AddListener(Play_2);
        CreditsButton.GetComponent<Button>().onClick.AddListener(Credits);
        QuitButton.GetComponent<Button>().onClick.AddListener(Quit);
    }

    void Play_1()
    {
        SceneManager.LoadScene(1);
    }    
    void Play_2()
    {
        SceneManager.LoadScene(2);
    }

    void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    void Quit()
    {
        Application.Quit();
    }
}