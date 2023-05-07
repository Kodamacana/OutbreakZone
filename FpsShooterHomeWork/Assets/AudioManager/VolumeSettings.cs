using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer myMixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SoundSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("SoundVolume")) LoadVolume();
        else SetSFXVolume();

        if (PlayerPrefs.HasKey("MusicVolume")) LoadMusic();
        else SetMusicVolume();
    }

    public void SetSFXVolume()
    {
        float volume = SoundSlider.value;
        //myMixer.SetFloat("SoundVolume", Mathf.Log10(volume)*20); //sesler ekleyince bunu koy
        myMixer.SetFloat("MasterVolume", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("SoundVolume", volume);
    }


    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }


    void LoadMusic()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SetMusicVolume();
    }

    void LoadVolume()
    {        
        SoundSlider.value = PlayerPrefs.GetFloat("SoundVolume");
        SetSFXVolume();        
    }
}
