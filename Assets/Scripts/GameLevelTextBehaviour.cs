using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameLevelTextBehaviour : MonoBehaviour
{
	private string currentLevelKey = "CurrentLevel";

    private TMP_Text levelText;

	// Start is called before the first frame update
	void Awake()
    {
		int curLvlValue = PlayerPrefs.GetInt(currentLevelKey, 1);
        levelText = GetComponent<TMP_Text>();

		levelText.text = "Level " + curLvlValue.ToString();
		Debug.Log("Current Level " + curLvlValue.ToString());
	}
}
