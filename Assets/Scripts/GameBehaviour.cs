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
    [SerializeField]
    private GameObject pegsSpawnPoints;

    [SerializeField] List<GameObject> bonusesList = new List<GameObject>();

    private Animator winPanelAnim;
    private Animator losePanelAnim;

    private TMP_Text winPanelScoreText;
    private TMP_Text losePanelScoreText;

    private ScoreBehaviour scoreScript;
	private GameObject ball;
	private BallBehaviour ballScript;

	private string currentLevelKey = "CurrentLevel";
    private string maxLevelKey = "MaxLevel";

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
            if (PlayerPrefs.GetInt(currentLevelKey, 1) + 1 > PlayerPrefs.GetInt(maxLevelKey, 1))
            {
                PlayerPrefs.SetInt(maxLevelKey, PlayerPrefs.GetInt(maxLevelKey, 1) + 1);
                Debug.Log("Max Level Key " + PlayerPrefs.GetInt(maxLevelKey, 0));
            }
            HideBonuses();
            winPanelScoreText.text = "Score: " + scoreScript.playerScore.ToString();
			winPanel.SetActive(true);
            pegsSpawnPoints.SetActive(false);
            winPanelAnim.Play("GameOverPanelOnAnim");
        }
	}

    void GameLose()
    {
        if (!losePanel.activeSelf)
        {
            HideBonuses();
            losePanelScoreText.text = "Score: " + scoreScript.playerScore.ToString();
			losePanel.SetActive(true);
            pegsSpawnPoints.SetActive(false);
            losePanelAnim.Play("GameOverPanelOnAnim");
        }
	}

    private void HideBonuses()
    {
        for (int i = 0; i < bonusesList.Count; i++)
        {
            if (bonusesList[i])
                bonusesList[i].SetActive(false);
        }
    }

	public void GameStart()
    {
        isGameStart = true;
	}

    public void PlayNextLevel()
    {
        PlayerPrefs.SetInt(currentLevelKey, PlayerPrefs.GetInt(currentLevelKey, 1) + 1);
    }
}
