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

    // Start is called before the first frame update
    void Start()
    {
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

		if (isStart)
		{
			rigidBody.isKinematic = false;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
	    if(collision != null)
        {
            if (collision.gameObject.CompareTag("Exit"))
            {
                Destroy(this.gameObject);
            }

			if (collision.gameObject.CompareTag("Peg"))
			{
				touchCount++;
			}
        }
	}
}
