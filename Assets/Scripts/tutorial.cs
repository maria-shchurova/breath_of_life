using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using  ProceduralModeling;
using System;

public class tutorial : MonoBehaviour
{
    [SerializeField] GameObject Cutout;
    [SerializeField] GameObject Hint;

    bool one_wasPressed;
    bool two_wasPressed;
    bool three_wasPressed;

    [SerializeField] ProceduralIvy IvyMaster;
    [SerializeField] PTGarden TreeMaster;

    [SerializeField] GameObject ContinueScreenUI;
    [SerializeField] CameraControl cameraControl;
    [SerializeField] PauseMenu pause;

    Button ContinueButton;
    private void Start()
    {
        Messenger.AddListener("IntroCompleted", ContinueScreen);
        ContinueButton = GameObject.Find("PlayGame").GetComponent<Button>();
        ContinueButton.onClick.AddListener(PlayGame);
        ContinueScreenUI.SetActive(false);

    }

    private void ContinueScreen()
    {
        cameraControl.enabled = false;
        pause.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        ContinueScreenUI.SetActive(true);
    }

    void PlayGame()
    {
        SceneManager.LoadScene(2);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { one_wasPressed = true; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { two_wasPressed = true; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { three_wasPressed = true; }


            if (one_wasPressed == true && two_wasPressed == true && three_wasPressed == true)
        {
            Cutout.SetActive(false);
            Hint.SetActive(false);
        }

    }
}
