using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider volumeSlider;
    public Image icon;
    public Sprite volumeHigh;
    public Sprite volumeLow;
    public Sprite volumeNull;

    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }

        // Update the icon based on the initial volume
        ChangeVolumeIcon(volumeSlider.value);
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();

        // Update the icon based on the new volume
        ChangeVolumeIcon(volumeSlider.value);
    }

    private void ChangeVolumeIcon(float volume)
    {
        if (volume < 0.75f && volume > 0)
        {
            icon.sprite = volumeLow; // mid volume
        }
        else if (volume == 0)
        {
            icon.sprite = volumeNull; // no volume
        }
        else
        {
            icon.sprite = volumeHigh; // full volume
        }
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
