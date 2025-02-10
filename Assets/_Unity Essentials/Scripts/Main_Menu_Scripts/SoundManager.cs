using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider volumeSlider;
    public Image icon;
    public Sprite volumeHigh;
    public Sprite volumeMid;
    public Sprite volumeLow;
    public Sprite volumeNull;

    void Start()
    {
        Load();

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
        if (volume == 0)
        {
            icon.sprite = volumeNull; // no volume
        }
        else if (volume > 0.74f)
        {
            icon.sprite = volumeHigh; // high volume
        }
        else if (volume > 0.25f)
        {
            icon.sprite = volumeMid; // mid volume
        }
        else
        {
            icon.sprite = volumeLow; // low volume

        }
    }

    private void Load()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }       
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);       
    }

    public void ResetDefaultVolume()
    {
        PlayerPrefs.DeleteAll();
        Load();
    }
}
