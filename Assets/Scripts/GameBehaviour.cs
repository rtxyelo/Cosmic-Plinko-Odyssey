using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    public int[] winScore = {40, 100, 200, 300, 500, 1000, 2000, 3000, 5000, 10000 };

    [HideInInspector]
    public bool isGameStart = false;
	[HideInInspector]
	public bool isGamePaused = false;

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

    private ScoreBehaviour scoreScript;
	private GameObject ball;
	private BallBehaviour ballScript;

	private string currentLevelKey = "CurrentLevel";

	// Start is called before the first frame update
	void Start()
    {
        isGameStart = false;
		scoreScript = FindObjectOfType<ScoreBehaviour>();
        if(scoreScript == null)
        {
            Debug.Log("Score script not found");
        }

		ball = GameObject.FindGameObjectWithTag("Ball");
		ballScript = ball.GetComponent<BallBehaviour>();

        winPanel.SetActive(false);
		losePanel.SetActive(false);

		winPanelAnim = winPanel.GetComponent<Animator>();
		losePanelAnim = losePanel.GetComponent<Animator>();

		winPanelScoreText = winPanel.transform.Find("ScoreText").GetComponent<TMP_Text>();
		losePanelScoreText = losePanel.transform.Find("ScoreText").GetComponent<TMP_Text>();
	}

    // Update is called once per frame
    void Update()
    {
        GameOverCheckUpdate();
	}

    void GameOverCheckUpdate()
    {
        if (scoreScript != null)
        {
            if (ballScript.isStart && !ball)
            {
                int curLvlValue = PlayerPrefs.GetInt(currentLevelKey, 1);
                if (scoreScript.playerScore >= winScore[curLvlValue - 1])
                {
                    GameWin();
                }
                else
                {
                    GameLose();
                }
            }
        }
    }

    void GameWin()
    {
        if (!winPanel.activeSelf)
        {
			winPanelScoreText.text = "Score: " + scoreScript.playerScore.ToString();
			winPanel.SetActive(true);
            winPanelAnim.Play("GameOverPanelOnAnim");
        }
	}

    void GameLose()
    {
        if (!losePanel.activeSelf)
        {
			losePanelScoreText.text = "Score: " + scoreScript.playerScore.ToString();
			losePanel.SetActive(true);
            losePanelAnim.Play("GameOverPanelOnAnim");
        }
	}

	public void GameStart()
    {
        isGameStart = true;
	}
}
