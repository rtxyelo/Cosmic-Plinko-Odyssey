using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    public static int[] winScore = { 10, 20, 30, 40, 50, 120, 140, 150, 130, 150, 200, 140, 170, 190, 170, 240, 240, 220, 260, 140, 350, 230, 240, 280, 510, 190, 230, 150, 190, 220, 190, 270, 260, 240, 290, 190, 230, 150, 190, 220, 190, 270, 260, 240, 150, 360, 250, 250, 310, 540};

    [HideInInspector]
    public static bool isGameStart = false;
	[HideInInspector]
	public static bool isGamePaused = false;

	[SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject winPanel;
    [SerializeField]
    private GameObject losePanel;

    private Animator winPanelAnim;
    private Animator losePanelAnim;

    private TMP_Text winPanelScoreText;
    private TMP_Text losePanelScoreText;

	private GameObject ball;
	private BallBehaviour ballScript;

	private string currentLevelKey = "CurrentLevel";
    private string maxLevelKey = "MaxLevel";

    private GameObject pauseButton;

    void Start()
    {
        isGameStart = false;

		ball = GameObject.FindGameObjectWithTag("Ball");
		ballScript = ball.GetComponent<BallBehaviour>();

        winPanel.SetActive(false);
		losePanel.SetActive(false);

		winPanelAnim = winPanel.GetComponent<Animator>();
		losePanelAnim = losePanel.GetComponent<Animator>();

		winPanelScoreText = winPanel.transform.Find("ScoreText").GetComponent<TMP_Text>();
		losePanelScoreText = losePanel.transform.Find("ScoreText").GetComponent<TMP_Text>();
        pauseButton = GameObject.Find("PauseButton");
	}

    void Update()
    {
        GameOverCheckUpdate();
	}

    void GameOverCheckUpdate()
    {
        if (ballScript.isStart && !ball.activeSelf)
        {
            int curLvlValue = PlayerPrefs.GetInt(currentLevelKey, 1);
            if (ScoreBehaviour.playerScore >= winScore[curLvlValue - 1])
            {
                GameWin();
            }
            else
            {
                GameLose();
            }
        }
        else
        {
            winPanel.SetActive(false);
            losePanel.SetActive(false);
        }
    }

    void GameWin()
    {
        if (!winPanel.activeSelf)
        {
            if (PlayerPrefs.GetInt(currentLevelKey, 1) + 1 > PlayerPrefs.GetInt(maxLevelKey, 1))
            {
                PlayerPrefs.SetInt(maxLevelKey, PlayerPrefs.GetInt(maxLevelKey, 1) + 1);
                Debug.Log("Max Level Key " + PlayerPrefs.GetInt(maxLevelKey, 0));
            }
            pauseButton.SetActive(false);
            winPanelScoreText.text = "Score: " + ScoreBehaviour.playerScore.ToString();
			winPanel.SetActive(true);
            winPanelAnim.Play("GameOverPanelOnAnim");
        }
	}

    void GameLose()
    {
        if (!losePanel.activeSelf)
        {
            pauseButton.SetActive(false);
            losePanelScoreText.text = "Score: " + ScoreBehaviour.playerScore.ToString();
			losePanel.SetActive(true);
            losePanelAnim.Play("GameOverPanelOnAnim");
        }
	}

	public void GameStart()
    {
        isGameStart = true;
    }

    public void PlayNextLevel()
    {
        if (PlayerPrefs.GetInt(currentLevelKey, 1) != 50)
            PlayerPrefs.SetInt(currentLevelKey, PlayerPrefs.GetInt(currentLevelKey, 1) + 1);
        else
            PlayerPrefs.SetInt(currentLevelKey, 1);
    }
}
