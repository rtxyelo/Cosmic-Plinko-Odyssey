using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomPanelBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject pegPanel;
    [SerializeField] private GameObject scorePanel;

	private GameObject ball;
	private BallBehaviour ballScript;

	private GameBehaviour gameBehaviour;

	// Start is called before the first frame update
	void Start()
    {
		ball = GameObject.FindGameObjectWithTag("Ball");
		ballScript = ball.GetComponent<BallBehaviour>();
		pegPanel.SetActive(true);
		scorePanel.SetActive(true);

		gameBehaviour = FindObjectOfType<GameBehaviour>();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangePanel()
    {
		pegPanel.SetActive(false);
		scorePanel.SetActive(true);
	}

	public void RemoveUnusedPegs()
	{
		gameBehaviour.GameStart();
	}

	public void StartGame()
	{
		ballScript.StartGame();
	}
}
