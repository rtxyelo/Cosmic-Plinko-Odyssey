using System.Collections;
using System.Collections.Generic;
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
		MoveListPegs();
		StartPegsOffCollision();
		PlayingAreaOnCollision();
	}

	private void StartPegsOffCollision()
	{
		List<PegBehaviour> playingPegs = new List<PegBehaviour>();
		playingPegs = playingAreaPegs.Where(x => x.isMouseDrug).ToList();

		foreach (PegBehaviour playingPeg in playingPegs)
		{
			foreach (PegBehaviour startPeg in startPegs)
			{
				Collider2D colliderStartPeg = startPeg.GetComponent<CircleCollider2D>();
				Collider2D ballStartPeg = startPeg.transform.GetChild(0).GetComponent<CircleCollider2D>();

				Collider2D colliderPlayingPeg = playingPeg.GetComponent<CircleCollider2D>();
				Collider2D ballPlayingPeg = playingPeg.transform.GetChild(0).GetComponent<CircleCollider2D>();

				Physics2D.IgnoreCollision(colliderStartPeg, colliderPlayingPeg);
				Physics2D.IgnoreCollision(colliderStartPeg, ballPlayingPeg);
				Physics2D.IgnoreCollision(ballStartPeg, colliderPlayingPeg);
				Physics2D.IgnoreCollision(ballStartPeg, ballPlayingPeg);

			}
		}
	}

	private void PlayingAreaOnCollision()
	{
		foreach (PegBehaviour playingPeg in playingAreaPegs)
		{
			List<PegBehaviour> playingPegs = new List<PegBehaviour>();
			playingPegs = playingAreaPegs.Where(x => x != playingPeg).ToList();

			foreach(PegBehaviour peg in playingPegs)
			{
				Collider2D colliderStartPeg = peg.GetComponent<CircleCollider2D>();
				Collider2D ballStartPeg = peg.transform.GetChild(0).GetComponent<CircleCollider2D>();

				Collider2D colliderPlayingPeg = playingPeg.GetComponent<CircleCollider2D>();
				Collider2D ballPlayingPeg = playingPeg.transform.GetChild(0).GetComponent<CircleCollider2D>();

				Physics2D.IgnoreCollision(colliderStartPeg, colliderPlayingPeg, false);
				Physics2D.IgnoreCollision(colliderStartPeg, ballPlayingPeg, false);
				Physics2D.IgnoreCollision(ballStartPeg, colliderPlayingPeg, false);
				Physics2D.IgnoreCollision(ballStartPeg, ballPlayingPeg, false);
			}
		}
	}

	private void MoveListPegs()
	{
		List<PegBehaviour> movedPegs = new List<PegBehaviour>();
		movedPegs = startPegs.Where(x => x.isPegClicked && !playingAreaPegs.Contains(x)).ToList();
		playingAreaPegs.AddRange(movedPegs);
		startPegs = startPegs.Where(x => !movedPegs.Contains(x)).ToList();
	}

	private void ClearPegsList(ref List<PegBehaviour> pegs)
	{
		pegs = pegs.Where(x => x != null).ToList();
	}
}
