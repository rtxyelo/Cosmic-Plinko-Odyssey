using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonBehaviour : MonoBehaviour
{
	protected GameObject ball;

	// Start is called before the first frame update
	void Start()
    {
		ball = GameObject.FindGameObjectWithTag("Ball");
	}

	// Update is called once per frame
	void Update()
    {
		if (!ball)
		{
			gameObject.SetActive(false);
		} 
    }
}
