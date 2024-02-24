using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class JumpPegBehaviour : PegBehaviour
{
    private float bounciness = 1.0f;

	[SerializeField]
	private PhysicsMaterial2D physicsMaterial;

	private PegsArrangementBehaviour pegsArrangementBehaviour;
    private bool isMaterialChanged = false;

    protected override void Start()
    {
        base.Start();
		pegsArrangementBehaviour = GameObject.Find("PegsArrangementBehaviour").GetComponent<PegsArrangementBehaviour>();
	}

    protected override void Update()
    {
        base.Update();
		PhysicMaterialChangeController();

	}

    void PhysicMaterialChangeController()
    {
        if(!isMaterialChanged && ballScript.isStart)
        {
            rigidBody.sharedMaterial = physicsMaterial;
			rigidBody.sharedMaterial.bounciness = bounciness;
            isMaterialChanged = true;
        }
    }

	protected override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        if (collision != null)
        {
            if (collision.CompareTag("OuterZone"))
            {
				pegsArrangementBehaviour.PegIsPlaced(2);
			}
        }
    }
}
