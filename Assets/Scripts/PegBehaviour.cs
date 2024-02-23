using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PegBehaviour : MonoBehaviour
{
    private bool isMouseDrug = false;
    private bool isCanPlace = true;

	private Image pegImage;
	private BoxCollider2D bottomOuterZone;
	private CircleCollider2D outerBall;

	protected GameObject ball;
	protected BallBehaviour ballScript;
	protected Rigidbody2D rigidBody;

	protected virtual void Start()
    {
		rigidBody = GetComponent<Rigidbody2D>();
        pegImage = GetComponent<Image>();

		ball = GameObject.FindGameObjectWithTag("Ball");
		ballScript = ball.GetComponent<BallBehaviour>();

		bottomOuterZone = GameObject.Find("BottomWall").GetComponent<BoxCollider2D>();
		outerBall = transform.GetChild(0).GetComponent<CircleCollider2D>();
	}

    // Update is called once per frame
    protected virtual void Update()
    {
        PegMoveController();
		CheckCollision();

	}

	private void CheckCollision()
	{
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
		if(ballScript.isStart)
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

	private void OnMouseDown()
	{

        isMouseDrug = true;

		if (!ballScript.isStart)
		{
			pegImage.enabled = true;
		}
	}

    private void OnMouseUp()
    {
        isMouseDrug = false;
    }


    protected virtual void OnTriggerStay2D(Collider2D collision)
	{
		if(collision != null)
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
			}
        }
	}
}
