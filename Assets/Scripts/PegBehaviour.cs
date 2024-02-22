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
	private CircleCollider2D circleCollider;
	private GameObject bottomOuterZone;
	private PegsArrangementBehaviour pegsArrangementBehaviour;


    void Start()
    {
		prevPoint = transform.position;
		rigidBody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        pegImage = GetComponent<Image>();
        circleCollider.isTrigger = true;

        pegsArrangementBehaviour = GameObject.Find("PegsArrangementBehaviour").GetComponent<PegsArrangementBehaviour>();
        bottomOuterZone = GameObject.Find("Bottom");
    }

    // Update is called once per frame
    void Update()
    {
        PegMoveController();
        //PegPlaceController();

        if (isMouseDrug && isCanPlace)
        {
            bottomOuterZone.SetActive(true);
        }
        else
            bottomOuterZone.SetActive(false);
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

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision != null)
    //    {
    //        if (collision.CompareTag("OuterZone"))
    //        {
    //            if(bottomOuterZone)
    //            {
    //                bottomOuterZone.SetActive(false);
    //                Debug.Log("Deactivate bottom");
    //            }
    //        }
    //    }
    //}

    private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision != null)
		{
			if (collision.CompareTag("OuterZone"))
			{
				isCanPlace = true;
                pegsArrangementBehaviour.PegIsPlaced();
                bottomOuterZone.SetActive(true);
				Debug.Log("Trigger exit");
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
