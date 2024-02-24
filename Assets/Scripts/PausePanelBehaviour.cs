using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanelBehaviour : MonoBehaviour
{
	public void PauseGame() 
	{
		GameBehaviour.isGamePaused = true;
	}

	public void ContinueGame()
	{
		GameBehaviour.isGamePaused = false;
	}
}
