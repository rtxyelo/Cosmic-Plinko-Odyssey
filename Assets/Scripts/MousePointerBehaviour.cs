using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MousePointerBehaviour : MonoBehaviour
{
    //[HideInInspector]
    public bool isMouseInStartZone = false;

    private Collider2D collision;

	// Start is called before the first frame update
	void Start()
    {
        isMouseInStartZone = false;

	}

    // Update is called once per frame
    void Update()
    {
        MoveController();
        CheckOverlap();
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

    Vector3 GetMousePos()
    {
		Vector3 cursor = Input.mousePosition;

		cursor = Camera.main.ScreenToWorldPoint(cursor);

		cursor.z = 1;

        return cursor;
	}
}
