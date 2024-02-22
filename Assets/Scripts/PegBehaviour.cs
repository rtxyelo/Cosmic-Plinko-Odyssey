using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PegBehaviour : MonoBehaviour
{
    private bool isMouseDrug = false;
    private bool isCanPlace = true;

    private Vector3 prevPoint = Vector3.zero;
	private Image pegImage;
	protected Rigidbody2D rigidBody;
	private BoxCollider2D bottomOuterZone;
	private CircleCollider2D outerBall;


    protected virtual void Start()
    {
		prevPoint = transform.position;
		rigidBody = GetComponent<Rigidbody2D>();
		Debug.Log("rigidBody name " + rigidBody.gameObject.name);
        pegImage = GetComponent<Image>();

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
        pegImage.enabled = true;
		Debug.Log("Peg is clamp " + gameObject.name);
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
