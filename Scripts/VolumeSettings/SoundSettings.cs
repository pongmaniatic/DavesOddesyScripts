using UnityEngine;
using UnityEngine.Audio;

using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [Tooltip("The \"Exposed Parameters\" name of the master volume in the audio Mixer")]
    public string masterVolume = "VolumeMaster";
    [Tooltip("The \"Exposed Parameters\" name of the SFX volume in the audio Mixer")]
    public string SFXVolume = "VolumeSFX";
    [Tooltip("The \"Exposed Parameters\" name of the Music volume in the audio Mixer")]
    public string MusicVolume1 = "VolumeMusic1";
    public string MusicVolume2 = "VolumeMusic1";
    [Tooltip("Put the MainMix in this slot")]
    public AudioMixer audioMixer;

    public Slider mainVolumeSlider;
    public Slider sfxVolumeSlider;

    public void SubmitMainVolumeSliderSettings()
    {
        audioMixer.SetFloat(masterVolume, Mathf.Log10(mainVolumeSlider.value) * 20);
        audioMixer.SetFloat(SFXVolume, Mathf.Log10(sfxVolumeSlider.value) * 20);
    }


    private void Update()
    {
        SubmitMainVolumeSliderSettings();
    }


    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat(masterVolume, volume);
    }


    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat(SFXVolume, volume);
    }


    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat(MusicVolume1, volume);
        audioMixer.SetFloat(MusicVolume2, volume);
    }


    public void SetLevel(float volume)
    {
        audioMixer.SetFloat(masterVolume, Mathf.Log10(volume) * 20);
    }
}
