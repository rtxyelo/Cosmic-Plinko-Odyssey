using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PegBehaviour : MonoBehaviour
{
	/*[HideInInspector]*/ public bool isMouseDrug = false;
	/*[HideInInspector]*/ public bool isCanPlace = false;
	/*[HideInInspector]*/ public bool isPegClicked = false;
	/*[HideInInspector]*/ public bool isPegBeenPlaced = false;

	private BoxCollider2D bottomOuterZone;
	private CircleCollider2D outerBall;
	private CircleCollider2D pegCollider;
	private MousePointerBehaviour mousePointer;

	protected Image pegImage;
	protected GameObject ball;
	protected BallBehaviour ballScript;
	protected PegsArrangementBehaviour pegsArrangement;
	protected Rigidbody2D rigidBody;

    protected virtual void Start()
	{
        rigidBody = GetComponent<Rigidbody2D>();
		pegImage = GetComponent<Image>();
		pegCollider = GetComponent<CircleCollider2D>();

		ball = GameObject.FindGameObjectWithTag("Ball");
		if (ball != null)
			ballScript = ball.GetComponent<BallBehaviour>();

		bottomOuterZone = GameObject.Find("BottomWall").GetComponent<BoxCollider2D>();
		outerBall = transform.GetChild(0).GetComponent<CircleCollider2D>();

		mousePointer = FindObjectOfType<MousePointerBehaviour>();
		pegsArrangement = FindObjectOfType<PegsArrangementBehaviour>();
	}

	// Update is called once per frame
	protected virtual void Update()
	{
		PegMoveController();
		CheckCollision();
		HideUnusedPegs();
		//RespawnUnusedPegs();
		TeleportPegToMouse();
	}

	private void CheckCollision()
	{
		if (isMouseDrug && !isCanPlace || mousePointer.isMouseInStartZone && isMouseDrug && isCanPlace)
		//if (isMouseDrug && !isCanPlace)
		{
			Physics2D.IgnoreCollision(bottomOuterZone, pegCollider);
			Physics2D.IgnoreCollision(bottomOuterZone, outerBall);
		}
		else
		{
			Physics2D.IgnoreCollision(bottomOuterZone, pegCollider, false);
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


		if(isCanPlace && isMouseDrug || isMouseDrug && isPegBeenPlaced)
		{
			DropDownBehaviour.isShowDropDown = true;
		}
	}

	protected virtual void RespawnUnusedPegs()
	{
		if (!isMouseDrug && isPegClicked && !isCanPlace)
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

	private void TeleportPegToMouse()
	{
		var mousePos = mousePointer.gameObject.transform.position;
		if(Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(mousePos.x, mousePos.y)) > 2f)
		{
			if (isMouseDrug && mousePointer.isPegCanPlaced || isMouseDrug && mousePointer.isMouseInStartZone)
			{
				gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
			}
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

				if (!isPegBeenPlaced && !GameBehaviour.isGameStart)
				{
					Debug.Log("isPegBeenPlaced " + isPegBeenPlaced);
					isPegBeenPlaced = true;
				}
			}
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision != null)
		{
			if (collision.gameObject.CompareTag("Peg"))
			{
				PegBehaviour collidePeg = collision.gameObject.GetComponent<PegBehaviour>();
				Collider2D colliderCollidePeg = collidePeg.GetComponent<CircleCollider2D>();
				Collider2D ballCollidePeg = collidePeg.transform.GetChild(0).GetComponent<CircleCollider2D>();

				Debug.Log("Peg collision Enter " + collidePeg.gameObject.name);

				if (isMouseDrug && !collidePeg.isPegClicked)
				{
					Physics2D.IgnoreCollision(colliderCollidePeg, pegCollider);
					Physics2D.IgnoreCollision(colliderCollidePeg, outerBall);
					Physics2D.IgnoreCollision(ballCollidePeg, pegCollider);
					Physics2D.IgnoreCollision(ballCollidePeg, outerBall);
				}
			}
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision != null)
		{
			if (collision.gameObject.CompareTag("Peg"))
			{
				PegBehaviour collidePeg = collision.gameObject.GetComponent<PegBehaviour>();
				Collider2D colliderCollidePeg = collidePeg.GetComponent<CircleCollider2D>();
				Collider2D ballCollidePeg = collidePeg.transform.GetChild(0).GetComponent<CircleCollider2D>();

				Debug.Log("Peg collision Exit " + collidePeg.gameObject.name);

				if (isMouseDrug && !collidePeg.isPegClicked)
				{
					Physics2D.IgnoreCollision(colliderCollidePeg, pegCollider, false);
					Physics2D.IgnoreCollision(colliderCollidePeg, outerBall, false);
					Physics2D.IgnoreCollision(ballCollidePeg, pegCollider, false);
					Physics2D.IgnoreCollision(ballCollidePeg, outerBall, false);
				}
			}
		}
	}
}
