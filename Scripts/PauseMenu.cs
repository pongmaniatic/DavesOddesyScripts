using UnityEngine;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
	public GameObject pauseMenu;
	public bool paused = false;

	private bool soundSFXOn = true;
	public AudioMixer sfxAudioMixer;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!paused){showPaused();}
			else if (paused){hidePaused(); }
		}

	}

	public void showPaused()
	{
		paused = true;
		Time.timeScale = 0;
		pauseMenu.SetActive(true);
		ToggleSFXSound();
	}


	public void hidePaused()
	{
		paused = false;
		Time.timeScale = 1;
		pauseMenu.SetActive(false);
		ToggleSFXSound();
	}


	public void ToggleSFXSound()
	{

		if (paused)
		{
			sfxAudioMixer.SetFloat("VolumeSFX", Mathf.Log10(-1) * 20);
			soundSFXOn = false;
		}
		else
		{
			sfxAudioMixer.SetFloat("VolumeSFX", Mathf.Log10(0) * 20);
			soundSFXOn = true;
		}

	}

}
