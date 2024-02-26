using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

public class PegStartColliderBehaviour : MonoBehaviour
{
    [HideInInspector]
    public List<PegBehaviour> startPegs = new List<PegBehaviour>();
	[HideInInspector]
	public List<PegBehaviour> playingAreaPegs = new List<PegBehaviour>();

    // Update is called once per frame
    void Update()
    {
		ClearPegsList(ref startPegs);
		ClearPegsList(ref playingAreaPegs);
	}

	public void MovePegToPlayingAreaList(PegBehaviour peg)
	{
		if (startPegs.Contains(peg))
		{
			startPegs.Remove(peg);
			playingAreaPegs.Add(peg);
			OnStartPlayingCollision(peg);
		}
	}

	public void AddStartPeg(PegBehaviour peg)
	{
		startPegs.Add(peg);
		OffStartStartCollision(peg);
		OffStartPlayingCollision(peg);
	}

	private void OffStartStartCollision(PegBehaviour peg)
	{
		foreach (var sPeg in startPegs.Where(x => x != peg))
		{
			Collider2D colliderStartPeg = peg.GetComponent<CircleCollider2D>();
			Collider2D ballStartPeg = peg.transform.GetChild(0).GetComponent<CircleCollider2D>();

			Collider2D colliderPlayingPeg = sPeg.GetComponent<CircleCollider2D>();
			Collider2D ballPlayingPeg = sPeg.transform.GetChild(0).GetComponent<CircleCollider2D>();

			Physics2D.IgnoreCollision(colliderStartPeg, colliderPlayingPeg);
			Physics2D.IgnoreCollision(colliderStartPeg, ballPlayingPeg);
			Physics2D.IgnoreCollision(ballStartPeg, colliderPlayingPeg);
			Physics2D.IgnoreCollision(ballStartPeg, ballPlayingPeg);
		}
	}

	private void OffStartPlayingCollision(PegBehaviour peg)
	{
		foreach(var pPeg in playingAreaPegs)
		{
			Collider2D colliderStartPeg = peg.GetComponent<CircleCollider2D>();
			Collider2D ballStartPeg = peg.transform.GetChild(0).GetComponent<CircleCollider2D>();

			Collider2D colliderPlayingPeg = pPeg.GetComponent<CircleCollider2D>();
			Collider2D ballPlayingPeg = pPeg.transform.GetChild(0).GetComponent<CircleCollider2D>();

			Physics2D.IgnoreCollision(colliderStartPeg, colliderPlayingPeg);
			Physics2D.IgnoreCollision(colliderStartPeg, ballPlayingPeg);
			Physics2D.IgnoreCollision(ballStartPeg, colliderPlayingPeg);
			Physics2D.IgnoreCollision(ballStartPeg, ballPlayingPeg);
		}
	}

	private void OnStartPlayingCollision(PegBehaviour peg)
	{
		foreach (var pPeg in playingAreaPegs.Where(x => x != peg))
		{
			Collider2D colliderStartPeg = peg.GetComponent<CircleCollider2D>();
			Collider2D ballStartPeg = peg.transform.GetChild(0).GetComponent<CircleCollider2D>();

			Collider2D colliderPlayingPeg = pPeg.GetComponent<CircleCollider2D>();
			Collider2D ballPlayingPeg = pPeg.transform.GetChild(0).GetComponent<CircleCollider2D>();

			Physics2D.IgnoreCollision(colliderStartPeg, colliderPlayingPeg, false);
			Physics2D.IgnoreCollision(colliderStartPeg, ballPlayingPeg, false);
			Physics2D.IgnoreCollision(ballStartPeg, colliderPlayingPeg, false);
			Physics2D.IgnoreCollision(ballStartPeg, ballPlayingPeg, false);
		}
	}

	// todo: call function in PegBehaviour script when peg destroys
	private void ClearPegsList(ref List<PegBehaviour> pegs)
	{
		if (pegs.Any(x => x == null))
		{
			pegs = pegs.Where(x => x != null).ToList();
		}
	}
}
