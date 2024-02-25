using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonPegBehaviour : PegBehaviour
{
	private PegsArrangementBehaviour pegsArrangementBehaviour;
	protected Transform commonPegPosition;

    protected override void Start()
    {
		base.Start();
		pegsArrangementBehaviour = GameObject.Find("PegsArrangementBehaviour").GetComponent<PegsArrangementBehaviour>();
        commonPegPosition = GameObject.FindGameObjectWithTag("CommonPegPosition").GetComponent<Transform>();
    }

    protected override void Update()
	{
		base.Update();
        RespawnUnusedPegs();
	}

	protected override void RespawnUnusedPegs()
	{
		if (!isMouseDrug && isPegClicked && !isCanPlace)
		{
			pegsArrangement.commonPegsCount++;
            Destroy(gameObject);
		}
	}

	protected override void OnMouseUp()
    {
        base.OnMouseUp();

        if (!isCanPlace)
        {
            // todo: redo this if i cant make respawn system
            //Debug.Log("Peg Position is " + gameObject.transform.position);
            //transform.position = commonPegPosition.position;
            pegImage.enabled = false;
        }
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
                pegsArrangementBehaviour.PegIsPlaced(1);
            }
        }
		base.OnTriggerExit2D(collision);
	}
}
