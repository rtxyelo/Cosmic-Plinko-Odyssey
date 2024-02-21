using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegBehaviour : MonoBehaviour
{
    private bool isMouseDrug = false;
    private bool isCanPlace = true;

    private Vector3 prevPoint = Vector3.zero;

	private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
		prevPoint = transform.position;
		rigidBody = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update()
    {
        PegMoveController();
        //PegPlaceController();
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
	}

    private void OnMouseUp()
    {
        isMouseDrug = false;
    }

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
