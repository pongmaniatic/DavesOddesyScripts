using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class ButtonBehaviour : MonoBehaviour
{
    private bool soundOn = true;
    public PauseMenu pauseMenuScript;
    public AudioMixer mainAudioMixer;
    public GameObject pauseMenu;
    public bool paused = false;
    public bool isUsePauseMenuFunctionality = true;

    public void PauseGame(){Time.timeScale = 0; pauseMenuScript.showPaused(); }
    public void UnPauseGame(){Time.timeScale = 1; pauseMenuScript.hidePaused(); }
    public void playGame() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1); 
    }
    public void BackToMenu() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void quitGame() { Application.Quit(); }
    public void ToggleSound() 
    {

        if (soundOn)
        {
            mainAudioMixer.SetFloat("VolumeMaster", Mathf.Log10(-1) * 20);
            soundOn = false;
        }
        else
        {
            mainAudioMixer.SetFloat("VolumeMaster", Mathf.Log10(0) * 20);
            soundOn = true;
        }

    }


    void Update()
    {
        if (isUsePauseMenuFunctionality)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Time.timeScale == 1) { Time.timeScale = 0; showPaused(); }
                else if (Time.timeScale == 0) { Time.timeScale = 1; hidePaused(); paused = false; }
            }
        }
    }

    public void showPaused()
    {
        pauseMenu.SetActive(true);
        paused = true;
    }
    public void hidePaused()
    {
        pauseMenu.SetActive(false);
        paused = false;
    }
}
