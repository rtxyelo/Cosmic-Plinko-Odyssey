using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBehaviour : MonoBehaviour
{
    private GameObject ball;
    private BallBehaviour ballScript;

    [SerializeField]
    private TMP_Text _textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
		ball = GameObject.FindGameObjectWithTag("Ball");
		ballScript = ball.GetComponent<BallBehaviour>();
	}

    // Update is called once per frame
    void Update()
    {
        SetScore();

	}

    private void SetScore()
    {
        if (ballScript.isStart)
        {
            _textMeshPro.text = (ballScript.touchCount * 10).ToString();
        }
    }
}
