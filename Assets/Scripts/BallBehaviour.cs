using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public float bounciness = 0.7f;
    public bool isStart = false;

	public int touchCount = 0;

    private Rigidbody2D rigidBody;
    private PhysicsMaterial2D ballMaterial;
	private GameBehaviour gameBehaviour;

	private Vector2 ballSpeed = Vector2.zero;
	private float ballGravity = 0;
	private bool isGameBeenPaused = false;

    // Start is called before the first frame update
    void Start()
    {
		gameBehaviour = FindObjectOfType<GameBehaviour>();
		rigidBody = GetComponent<Rigidbody2D>();
		ballMaterial = rigidBody.sharedMaterial;

		ballMaterial.bounciness = bounciness;

		touchCount = 0;
	}

    // Update is called once per frame
    void Update()
    {
		GameController();
	}

    private void GameController()
    {
		if (Input.GetKey(KeyCode.Space)) // Debug tool
		{
			isStart = true;
		}

		if (isStart && rigidBody.isKinematic)
		{
			rigidBody.isKinematic = false;
		}

		if (gameBehaviour.isGamePaused)
		{
			rigidBody.velocity = Vector2.zero;
			rigidBody.gravityScale = 0;
			isGameBeenPaused = true;
		}
		else
		{
			if (isGameBeenPaused)
			{
				rigidBody.velocity = ballSpeed;
				rigidBody.gravityScale = ballGravity;
				isGameBeenPaused = false;
			}

			ballSpeed = rigidBody.velocity;
			ballGravity = rigidBody.gravityScale;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
	    if(collision != null)
        {
            if (collision.gameObject.CompareTag("Exit"))
            {
				FinishGame();
            }

			if (collision.gameObject.CompareTag("Peg"))
			{
				touchCount++;
			}
        }
	}

	public void FinishGame()
	{
		Destroy(this.gameObject);
	}

	public void StartGame()
	{
		isStart = true;
	}
}
