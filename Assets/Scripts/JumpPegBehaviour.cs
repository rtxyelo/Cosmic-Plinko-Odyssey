using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPegBehaviour : PegBehaviour
{
    private PegsArrangementBehaviour pegsArrangementBehaviour;
    private PhysicsMaterial2D physicsMaterial;

    protected override void Start()
    {
        pegsArrangementBehaviour = GameObject.Find("PegsArrangementBehaviour").GetComponent<PegsArrangementBehaviour>();
        base.Start();
        physicsMaterial = GetComponent<Rigidbody2D>().sharedMaterial;
        physicsMaterial.bounciness = 1.0f;
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
                pegsArrangementBehaviour.PegIsPlaced(2);
            }
        }
    }
}
