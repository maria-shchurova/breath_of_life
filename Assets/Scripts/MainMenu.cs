using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private GameObject PlayButton1;
    private GameObject PlayButton2;
    private GameObject CreditsButton;
    private GameObject QuitButton;
    private GameObject LoadingText;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        PlayButton1 = GameObject.Find("Play Level 1");
        PlayButton2 = GameObject.Find("Play Level 2");
        CreditsButton = GameObject.Find("Credits");
        QuitButton = GameObject.Find("Quit");
        LoadingText = GameObject.Find("LoadingText");

        LoadingText.SetActive(false);
        PlayButton1.GetComponent<Button>().onClick.AddListener(Play_1);
        PlayButton2.GetComponent<Button>().onClick.AddListener(Play_2);
        CreditsButton.GetComponent<Button>().onClick.AddListener(Credits);
        QuitButton.GetComponent<Button>().onClick.AddListener(Quit);
    }

    void Play_1()
    {
        SceneManager.LoadScene(1);
        LoadingText.SetActive(true);
    }    
    void Play_2()
    {
        SceneManager.LoadScene(2);
        LoadingText.SetActive(true);
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