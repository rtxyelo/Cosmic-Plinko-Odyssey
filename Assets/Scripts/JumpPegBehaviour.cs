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
    protected Transform jumpPegPosition;

    protected override void Start()
    {
        base.Start();
		pegsArrangementBehaviour = GameObject.Find("PegsArrangementBehaviour").GetComponent<PegsArrangementBehaviour>();
        jumpPegPosition = GameObject.FindGameObjectWithTag("JumpPegPosition").GetComponent<Transform>();
    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();

        if (!isCanPlace)
        {
			// todo: redo this if i cant make respawn system
			//Debug.Log("Peg Position is " + gameObject.transform.position);
            //transform.position = jumpPegPosition.position;
            pegImage.enabled = false;
        }
    }

    protected override void Update()
    {
        base.Update();
		PhysicMaterialChangeController();
        RespawnUnusedPegs();
	}

	protected override void RespawnUnusedPegs()
	{
		if (!isMouseDrug && isPegClicked && !isCanPlace)
		{
			pegsArrangement.jumpPegsCount++;
            Destroy(gameObject);
		}
	}

	void PhysicMaterialChangeController()
    {
        if(!isMaterialChanged && ballScript.isStart)
        {
            rigidBody.sharedMaterial = physicsMaterial;
			rigidBody.sharedMaterial.bounciness = bounciness;
            isMaterialChanged = true;
        }
        else if (pegsArrangementBehaviour.isRemoveJumpPegMaterial)
        {
            OffJumpPegMeterial();
        }
    }

    public void OffJumpPegMeterial()
    {
        rigidBody.sharedMaterial = null;
        isMaterialChanged = false;
    }

	protected override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
		if (collision != null)
        {
            if (collision.CompareTag("OuterZone") && !isPegBeenPlaced)
            {
				pegsArrangementBehaviour.PegIsPlaced(2);
			}
        }
        base.OnTriggerExit2D(collision);
	}
}
