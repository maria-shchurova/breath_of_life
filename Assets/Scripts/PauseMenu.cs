using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenuObject;

    private GameObject MenuButton;
    private GameObject QuitButton;

    CameraControl cameraControl;
    void Start()
    {
        MenuButton = GameObject.Find("MenuButton");
        QuitButton = GameObject.Find("QuitButton");

        cameraControl = FindObjectOfType<CameraControl>();

        MenuButton.GetComponent<Button>().onClick.AddListener(ToMenu);
        QuitButton.GetComponent<Button>().onClick.AddListener(Quit);

        pauseMenuObject.SetActive(false); //this GO should be active initially to set references to buttons.
    }

    void ToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    void Quit()
    {
        Time.timeScale = 1;
        Application.Quit();
    }

    private void Pause()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuObject.SetActive(true);
        cameraControl.enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Reset()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuObject.SetActive(false);
        cameraControl.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuObject.activeInHierarchy == false)
                Pause();
            else
                Reset();
        }
    }
}