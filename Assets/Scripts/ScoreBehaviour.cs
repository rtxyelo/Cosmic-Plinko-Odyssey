using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBehaviour : MonoBehaviour
{
    public static int playerScore;
    [SerializeField] private int scoreScaler = 10;
    [SerializeField] public int pointsBonusQuantity = 100;
    public static int pointsBonusesCollectCount = 0;

    private GameObject ball;
    private BallBehaviour ballScript;


    [SerializeField]
    private TMP_Text _textMeshPro;

    private void Start()
    {
		ball = GameObject.FindGameObjectWithTag("Ball");
		ballScript = ball.GetComponent<BallBehaviour>();

		playerScore = 0;
	}

	private void Update()
    {
        SetScore();
        ScoreUpdate();
    }

    private void SetScore()
    {
        if (ballScript.isStart)
        {
            _textMeshPro.text = playerScore.ToString();
        }
    }

    private void ScoreUpdate()
    {
        playerScore = ballScript.touchCount * scoreScaler + pointsBonusesCollectCount * pointsBonusQuantity;
    }

    public void PointsBonusCollect()
    {
        pointsBonusesCollectCount++;
        Debug.Log("Points bonus collect!");
    }
}
