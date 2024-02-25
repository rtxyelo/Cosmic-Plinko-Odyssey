using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PegBehaviour : MonoBehaviour
{
	[HideInInspector] public bool isMouseDrug = false;
	[HideInInspector] public bool isCanPlace = false;
	[HideInInspector] public bool isPegClicked = false;
	[HideInInspector] public bool isPegBeenPlaced = false;

	private BoxCollider2D bottomOuterZone;
	private CircleCollider2D outerBall;
	private MousePointerBehaviour mousePointer;

	protected Image pegImage;
	protected GameObject ball;
	protected BallBehaviour ballScript;
	protected Rigidbody2D rigidBody;

    protected virtual void Start()
	{
        rigidBody = GetComponent<Rigidbody2D>();
		pegImage = GetComponent<Image>();

		ball = GameObject.FindGameObjectWithTag("Ball");
		if (ball != null)
			ballScript = ball.GetComponent<BallBehaviour>();

		bottomOuterZone = GameObject.Find("BottomWall").GetComponent<BoxCollider2D>();
		outerBall = transform.GetChild(0).GetComponent<CircleCollider2D>();

		mousePointer = FindAnyObjectByType<MousePointerBehaviour>();
	}

	// Update is called once per frame
	protected virtual void Update()
	{
		PegMoveController();
		CheckCollision();
		HideUnusedPegs();
		//RespawnUnusedPegs();
	}

	private void CheckCollision()
	{
		//if (isMouseDrug && !isCanPlace || mousePointer.isMouseInStartZone && isMouseDrug && isCanPlace)
		if (isMouseDrug && !isCanPlace)
        {
            Physics2D.IgnoreCollision(bottomOuterZone, this.gameObject.GetComponent<CircleCollider2D>());
			Physics2D.IgnoreCollision(bottomOuterZone, outerBall);
		}
		else
		{
			Physics2D.IgnoreCollision(bottomOuterZone, this.gameObject.GetComponent<CircleCollider2D>(), false);
			Physics2D.IgnoreCollision(bottomOuterZone, outerBall, false);
		}
	}

	private void PegMoveController()
	{
		if (GameBehaviour.isGameStart)
			return;
		if (GameBehaviour.isGamePaused)
			return;

		Vector3 cursor = Input.mousePosition;

		cursor = Camera.main.ScreenToWorldPoint(cursor);

		cursor.z = 0;

		if (isMouseDrug)
		{
			rigidBody.velocity = (cursor - transform.position).normalized * Vector3.Distance(cursor, transform.position) * 20f;
			//transform.position = cursor;
			rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
		}
		else
		{
			rigidBody.velocity = Vector3.zero;
			rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
		}

	}

	private void RespawnUnusedPegs()
	{
		if (!isMouseDrug && !isCanPlace && isPegBeenPlaced)
		{
			Destroy(gameObject);
		}
	}

	private void HideUnusedPegs()
	{
		if (GameBehaviour.isGameStart && !isCanPlace)
		{
			gameObject.SetActive(false);
		}
	}

	private void OnDestroy()
	{
		Debug.Log("Destroy peg");
	}

	private void OnMouseDown()
	{
		if (!isPegClicked)
			isPegClicked = true;

		if (!ballScript.isStart)
			isMouseDrug = true;

		if (!ballScript.isStart && !GameBehaviour.isGamePaused)
		{
			pegImage.enabled = true;
		}
	}

	protected virtual void OnMouseUp()
	{
		isMouseDrug = false;
	}


	protected virtual void OnTriggerStay2D(Collider2D collision)
	{
		if (collision != null)
		{
			if (collision.CompareTag("OuterZone"))
			{
				isCanPlace = false;
			}
		}
	}

	protected virtual void OnTriggerExit2D(Collider2D collision)
	{
		if (collision != null)
		{
			if (collision.CompareTag("OuterZone"))
			{
				isCanPlace = true;

				if (!isPegBeenPlaced)
				{
					Debug.Log("isPegBeenPlaced " + isPegBeenPlaced);
					isPegBeenPlaced = true;
				}
			}
		}
	}
}
