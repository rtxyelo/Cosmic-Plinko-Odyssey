using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBehaviour : MonoBehaviour
{
    [SerializeField] private bool isReset = false;
    [SerializeField] private bool isSetKeys = false;

    [SerializeField] private int currentLevel = 1;
    [SerializeField] private int maxLevel = 1;
    [SerializeField] private float musicVolume = 0.2f;

    private string maxLevelKey = "MaxLevel";
    private string currentLevelKey = "CurrentLevel";
    private string musicVolumeKey = "MusicVolumeKey";


    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey(maxLevelKey))
        {
            PlayerPrefs.SetInt(maxLevelKey, 1);
        }
        if (!PlayerPrefs.HasKey(currentLevelKey))
        {
            PlayerPrefs.SetInt(currentLevelKey, 1);
        }
        if (!PlayerPrefs.HasKey(musicVolumeKey))
        {
            PlayerPrefs.SetFloat(musicVolumeKey, 0.2f);
        }
        if (isReset)
        {
            PlayerPrefs.SetInt(maxLevelKey, 1);
            PlayerPrefs.SetInt(currentLevelKey, 1);
            PlayerPrefs.SetFloat(musicVolumeKey, 0.2f);
        }
        if (isSetKeys)
        {
            PlayerPrefs.SetInt(maxLevelKey, maxLevel);
            PlayerPrefs.SetInt(currentLevelKey, currentLevel);
            PlayerPrefs.SetFloat(musicVolumeKey, musicVolume);
        }

        Debug.Log("Max Level Key " + PlayerPrefs.GetInt(maxLevelKey, 0));
        Debug.Log("Current Level Key " + PlayerPrefs.GetInt(currentLevelKey, 0));
        Debug.Log("Music Volume Key " + PlayerPrefs.GetFloat(musicVolumeKey, -1.0f));

    }

    // Update is called once per frame
    void Update()
    {

    }
}