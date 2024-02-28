using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MousePointerBehaviour : MonoBehaviour
{
    //[HideInInspector]
    public bool isMouseInStartZone = false;
	//[HideInInspector]
	public bool isPegCanPlaced = false;

    private Collider2D collision;
    private Collider2D pegPlaceCollision;

    [SerializeField]
    private LayerMask LayersToCollide;

	// Start is called before the first frame update
	void Start()
    {
        isMouseInStartZone = false;
		isPegCanPlaced = false;
	}

    // Update is called once per frame
    void Update()
    {
        MoveController();
        CheckOverlap();
		CheckPegSpaceOverlap();
	}

    void MoveController()
    {
        transform.position = GetMousePos();
    }

    void CheckOverlap()
    {
		collision = Physics2D.OverlapCircle(transform.position, transform.localScale.x / 2, 1 << 7);
		isMouseInStartZone = collision != null;
	}

    void CheckPegSpaceOverlap()
    {
        int layers = LayersToCollide.value;

		pegPlaceCollision = Physics2D.OverlapCircle(transform.position, transform.localScale.x*2f, layers);
		isPegCanPlaced = pegPlaceCollision == null;
	}

    Vector3 GetMousePos()
    {
		Vector3 cursor = Input.mousePosition;

		cursor = Camera.main.ScreenToWorldPoint(cursor);

		cursor.z = 100;

        return cursor;
	}
}
