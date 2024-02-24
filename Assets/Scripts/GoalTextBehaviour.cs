using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalTextBehaviour : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text scoreTextShadow;

    private string currentLevelKey = "CurrentLevel";

    private int currentLevel;

    private void Start()
    {
        if (!PlayerPrefs.HasKey(currentLevelKey))
            PlayerPrefs.SetInt(currentLevelKey, 1);
    }

    private void Update()
    {
        currentLevel = PlayerPrefs.GetInt(currentLevelKey, 1);
        scoreText.text = "GOAL " + GameBehaviour.winScore[currentLevel - 1].ToString();
        scoreTextShadow.text = "GOAL " + GameBehaviour.winScore[currentLevel - 1].ToString();
    }
}
