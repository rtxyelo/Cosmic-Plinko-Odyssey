using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonPegBehaviour : PegBehaviour
{
	private PegsArrangementBehaviour pegsArrangementBehaviour;

    protected override void Start()
    {
        pegsArrangementBehaviour = GameObject.Find("PegsArrangementBehaviour").GetComponent<PegsArrangementBehaviour>();
		base.Start();
    }

	protected override void Update()
	{
		base.Update();

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
                pegsArrangementBehaviour.PegIsPlaced(1);
            }
        }
    }
}
