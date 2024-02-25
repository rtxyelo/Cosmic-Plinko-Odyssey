using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLevelBehaviour : MonoBehaviour
{
    [SerializeField] private AudioSource asseptSound;
    [SerializeField] private AudioSource declineSound;
    [SerializeField] private List<Image> listOfButtonImages = new List<Image>();

    [SerializeField] private Sprite selectLvlSprite;
    [SerializeField] private Sprite closedLvlSprite;
    [SerializeField] private Sprite openedLvlSprite;

    private string maxLevelKey = "MaxLevel";
    private string currentLevelKey = "CurrentLevel";
    private string musicVolumeKey = "MusicVolumeKey";

    private void Start()
    {
        if (!PlayerPrefs.HasKey(maxLevelKey))
        {
            PlayerPrefs.SetInt(maxLevelKey, 1);
        }
        int btnInd = 1;
        foreach (Image buttonImg in listOfButtonImages)
        {
			if (btnInd > PlayerPrefs.GetInt(maxLevelKey, 1000))
				buttonImg.sprite = closedLvlSprite;
			else if (btnInd != PlayerPrefs.GetInt(currentLevelKey, 0))
				buttonImg.sprite = openedLvlSprite;
			else
				buttonImg.sprite = selectLvlSprite;
			btnInd++;
        }
    }

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
            Debug.Log("currentLevelKey " + PlayerPrefs.GetInt(currentLevelKey, 0));

            int btnInd = 1;
            foreach (Image buttonImg in listOfButtonImages)
            {
                if (btnInd > PlayerPrefs.GetInt(maxLevelKey, 1000))
                    buttonImg.sprite = closedLvlSprite;
                else if(btnInd != lvl)
                    buttonImg.sprite = openedLvlSprite;
                else 
                    buttonImg.sprite = selectLvlSprite;
                btnInd++;
            }

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
