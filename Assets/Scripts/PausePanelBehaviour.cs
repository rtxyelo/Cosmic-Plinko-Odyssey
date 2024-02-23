using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanelBehaviour : MonoBehaviour
{
	private GameBehaviour gameBehaviour;

	void Start()
	{
		gameBehaviour = FindObjectOfType<GameBehaviour>();
	}

	public void PauseGame() 
	{
		gameBehaviour.isGamePaused = true;
	}

	public void ContinueGame()
	{
		gameBehaviour.isGamePaused = false;
	}
}
