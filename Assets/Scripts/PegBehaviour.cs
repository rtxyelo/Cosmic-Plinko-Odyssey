using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PegBehaviour : MonoBehaviour
{
    private bool isMouseDrug = false;
    private bool isCanPlace = true;

    private Vector3 prevPoint = Vector3.zero;
	private Image pegImage;
	private Rigidbody2D rigidBody;
	private BoxCollider2D bottomOuterZone;
	private PegsArrangementBehaviour pegsArrangementBehaviour;


    void Start()
    {
		prevPoint = transform.position;
		rigidBody = GetComponent<Rigidbody2D>();
        pegImage = GetComponent<Image>();

        pegsArrangementBehaviour = GameObject.Find("PegsArrangementBehaviour").GetComponent<PegsArrangementBehaviour>();
        bottomOuterZone = GameObject.Find("Bottom").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PegMoveController();
		//PegPlaceController();

		Debug.Log(isCanPlace);

		if(isMouseDrug && !isCanPlace)
		{
			bottomOuterZone.enabled = false;
		}
		else
		{
			bottomOuterZone.enabled = true;
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

    private void PegPlaceController()
    {
        if(isCanPlace && !isMouseDrug)
        {
            prevPoint = transform.position;
        } 
        else if(!isCanPlace && !isMouseDrug)
        {
            transform.position = prevPoint;
        }
	}

	private void OnMouseDown()
	{
        isMouseDrug = true;
        pegImage.enabled = true;
    }

    private void OnMouseUp()
    {
        isMouseDrug = false;
    }


	// Rewrite for each type of peg !!!
	private void OnTriggerStay2D(Collider2D collision)
	{
		if(collision != null)
        {
            if (collision.CompareTag("OuterZone"))
            {
                isCanPlace = false;
            }
        }
	}

    private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision != null)
		{
			if (collision.CompareTag("OuterZone"))
			{
				isCanPlace = true;
				pegsArrangementBehaviour.PegIsPlaced();
				//bottomOuterZone.enabled = true;
				Debug.Log("Exit trigger");
			}
        }
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision != null)
		{
			if (collision.collider.CompareTag("OuterZone"))
			{
				isCanPlace = true;
			}
		}
	}
}
