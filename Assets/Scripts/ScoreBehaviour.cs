using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBehaviour : MonoBehaviour
{
    public int playerScore;
    public int scoreScaler = 10;
    public int pointsBonusQuantity = 100;
    [HideInInspector] public int pointsBonusesCollectCount = 0;

    private GameObject ball;
    private BallBehaviour ballScript;


    [SerializeField]
    private TMP_Text _textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
		ball = GameObject.FindGameObjectWithTag("Ball");
		ballScript = ball.GetComponent<BallBehaviour>();

		playerScore = 0;
	}

	// Update is called once per frame
	void Update()
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
