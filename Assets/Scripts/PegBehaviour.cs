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
	private GameObject outerBallGameObject;
	private CircleCollider2D outerBall;
	private CircleCollider2D pegCollider;
	private MousePointerBehaviour mousePointer;
	private int gameobjectLayer;
	private int outerBallLayer;

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

		gameobjectLayer = LayerMask.NameToLayer("UI");
		outerBallLayer = LayerMask.NameToLayer("OuterZone");

		ball = GameObject.FindGameObjectWithTag("Ball");
		if (ball != null)
			ballScript = ball.GetComponent<BallBehaviour>();

		bottomOuterZone = GameObject.Find("BottomWall").GetComponent<BoxCollider2D>();
		outerBallGameObject = transform.GetChild(0).gameObject;
		outerBall = outerBallGameObject.GetComponent<CircleCollider2D>();

		mousePointer = FindObjectOfType<MousePointerBehaviour>();
		pegsArrangement = FindObjectOfType<PegsArrangementBehaviour>();
	}

	// Update is called once per frame
	protected virtual void Update()
	{
		PegMoveController();
		CheckCollision();
		HideUnusedPegs();
		TeleportPegToMouse();
		StayedPegController();
		LayerChanger();
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
		Debug.Log("isGameStart " + GameBehaviour.isGameStart);
		Debug.Log("isGamePaused " + GameBehaviour.isGamePaused);
		if (GameBehaviour.isGameStart)
			return;
		if (GameBehaviour.isGamePaused)
			return;
		if (rigidBody.bodyType == RigidbodyType2D.Static || rigidBody.bodyType == RigidbodyType2D.Kinematic)
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
		if(Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(mousePos.x, mousePos.y)) > 1.5f)
		{
			if (isMouseDrug && mousePointer.isPegCanPlaced || isMouseDrug && mousePointer.isMouseInStartZone)
			{
				rigidBody.velocity = Vector3.zero;
				gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
			}
		}
	}

	private void StayedPegController()
	{
		if(isMouseDrug && rigidBody.bodyType != RigidbodyType2D.Dynamic)
		{
			rigidBody.bodyType = RigidbodyType2D.Dynamic;
		}
		else if (!isPegClicked)
		{
			rigidBody.bodyType = RigidbodyType2D.Kinematic;
		}
		else if(!isMouseDrug && rigidBody.bodyType != RigidbodyType2D.Static)
		{
			rigidBody.bodyType = RigidbodyType2D.Static;
		}
	}

	private void LayerChanger()
	{
		if (isPegClicked && gameObject.layer != gameobjectLayer)
		{
			gameObject.layer = gameobjectLayer;
			outerBallGameObject.layer = outerBallLayer;
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
					Debug.Log("OnTriggerExit");
					isPegBeenPlaced = true;
				}
			}
		}
	}
}
