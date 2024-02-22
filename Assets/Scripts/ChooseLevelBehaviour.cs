using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseLevelBehaviour : MonoBehaviour
{
    [SerializeField] private AudioSource asseptSound;
    [SerializeField] private AudioSource declineSound;

    private string maxLevelKey = "MaxLevel";
    private string currentLevelKey = "CurrentLevel";
    private string musicVolumeKey = "MusicVolumeKey";

    /// <summary>
    /// Check level availability by level number.
    /// </summary>
    /// <param name="lvl">Level number.</param>
    public void CheckLevelAvailability(int lvl)
    {
        if (!PlayerPrefs.HasKey(musicVolumeKey))
        {
            PlayerPrefs.SetFloat(musicVolumeKey, 0.2f);
        }

        if (!PlayerPrefs.HasKey(currentLevelKey))
        {
            PlayerPrefs.SetInt(currentLevelKey, 1);
        }

        if (!PlayerPrefs.HasKey(maxLevelKey))
        {
            PlayerPrefs.SetInt(maxLevelKey, 1);
        }
        else if (lvl <= PlayerPrefs.GetInt(maxLevelKey, 1000))
        {
            PlayerPrefs.SetInt(currentLevelKey, lvl);
            if (PlayerPrefs.GetFloat(musicVolumeKey, 0.2f) != 0f)
            {
                asseptSound.volume = PlayerPrefs.GetFloat(musicVolumeKey, 0.2f) + 0.2f;
                asseptSound.Play();
            }
        }
        else
        {
            if (PlayerPrefs.GetFloat(musicVolumeKey, 0.2f) != 0f)
            {
                declineSound.volume = PlayerPrefs.GetFloat(musicVolumeKey, 0.2f) + 0.2f;
                declineSound.Play();
            }
        }
    }
}
