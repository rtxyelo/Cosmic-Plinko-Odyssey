using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public float bounciness = 0.7f;
    public bool isStart = false;

	public int touchCount = 0;

	[SerializeField] private GameObject reboundWall;

    private Rigidbody2D rigidBody;
    private PhysicsMaterial2D ballMaterial;
    private ScoreBehaviour scoreBehaviour;

	private Vector2 ballSpeed = Vector2.zero;
	private float ballGravity = 0;
	private bool isGameBeenPaused = false;
	private Vector3 initialPosition;
	private bool isHealthBonusCollect = false;
	private int reboundBonusCollectCount = 0;
    private PegsArrangementBehaviour pegsArrangementBehaviour;
    void Start()
    {
		initialPosition = transform.position;

		scoreBehaviour = FindObjectOfType<ScoreBehaviour>();
        pegsArrangementBehaviour = FindObjectOfType<PegsArrangementBehaviour>();

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

		if (GameBehaviour.isGamePaused)
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
			if (collision.gameObject.CompareTag("Exit") && isHealthBonusCollect)
			{
				PlaceBallOnInitialPosition();
                isHealthBonusCollect = false;
            }
            else if (collision.gameObject.CompareTag("Exit"))
            {
				FinishGame();
            }

			if (collision.gameObject.CompareTag("Peg"))
			{
				touchCount++;
			}

            if (collision.gameObject.CompareTag("ReboundWall"))
            {
                reboundBonusCollectCount--;
                
                if (reboundBonusCollectCount <= 0)
                    reboundWall.SetActive(false);
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // PointsBonus collect
        if (collision.gameObject.CompareTag("PointsBonus"))
        {
            scoreBehaviour.PointsBonusCollect();

            PegsArrangementBehaviour.listOfCollectBonuses.Add(collision.gameObject);
            collision.gameObject.SetActive(false);

			Debug.Log("Points bonus collect");
        }

        // SpeedBonus collect
        if (collision.gameObject.CompareTag("SpeedBonus"))
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x + 0.001f, rigidBody.velocity.y + 0.001f);
            rigidBody.velocity *= 3;

            PegsArrangementBehaviour.listOfCollectBonuses.Add(collision.gameObject);
            collision.gameObject.SetActive(false);

            Debug.Log("Speed bonus collect");
        }

        // ReboundBonus collect
        if (collision.gameObject.CompareTag("ReboundBonus"))
        {
            reboundWall.SetActive(true);
            reboundBonusCollectCount++;

            PegsArrangementBehaviour.listOfCollectBonuses.Add(collision.gameObject);
            collision.gameObject.SetActive(false);

            Debug.Log("Rebound bonus collect");
        }

        // HealthBonus collect
        if (collision.gameObject.CompareTag("HealthBonus"))
        {
            isHealthBonusCollect = true;

            PegsArrangementBehaviour.listOfCollectBonuses.Add(collision.gameObject);
            collision.gameObject.SetActive(false);

            Debug.Log("Health bonus collect");
        }
    }

    public void FinishGame()
	{
		rigidBody.isKinematic = true;
		gameObject.SetActive(false);
    }

	public void StartGame()
	{
		isStart = true;
        pegsArrangementBehaviour.isRemoveJumpPegMaterial = false;
    }

	public void StartGameFlagOff()
	{
		isStart = false;
        GameBehaviour.isGameStart = false;
    }

	public void PlaceBallOnInitialPosition()
	{
		transform.position = initialPosition;
	}
}
