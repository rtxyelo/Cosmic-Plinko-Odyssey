using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PegsArrangementBehaviour : MonoBehaviour
{
    private string currentLevelKey = "CurrentLevel";
    private int currentLevelValue;

    [SerializeField] private RectTransform commonPegPosition;
    [SerializeField] private RectTransform jumpPegPosition;
    //private Transform speedPegPosition;

    private int commonPegsCount;
    private int jumpPegsCount;

    [SerializeField] private GameObject commonPegPrefab;
    [SerializeField] private GameObject jumpPegPrefab;

    private List<PegBehaviour> commonPegsInstances = new List<PegBehaviour>();
    private List<PegBehaviour> jumpPegsInstances = new List<PegBehaviour>();

    [SerializeField] private TMP_Text commonPegCountDisplay;
    [SerializeField] private TMP_Text jumpPegCountDisplay;

    private bool commonPegIsPlaced = true;
    private bool jumpPegIsPlaced = true;

    private void Start()
    {
        if (!PlayerPrefs.HasKey(currentLevelKey))
        {
            PlayerPrefs.SetInt(currentLevelKey, 1);
        }
        currentLevelValue = PlayerPrefs.GetInt(currentLevelKey, 1);
        CalculatePegsCountByLvl(currentLevelValue);

    }

    private void Update()
    {
        PlaceCommonPeg();
        PlaceJumpPeg();
        DisplayPegsCount();
	}

    private void PlaceCommonPeg()
    {
        if (commonPegsCount > 0 && commonPegIsPlaced)
        {
			commonPegIsPlaced = false;
            var go = Instantiate(commonPegPrefab, commonPegPosition);
            go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, 0);
			commonPegsInstances.Add(go.GetComponent<PegBehaviour>());
			commonPegsCount--;
        }
    }

    private void PlaceJumpPeg()
    {
        if (jumpPegsCount > 0 && jumpPegIsPlaced)
        {
			jumpPegIsPlaced = false;
            var go = Instantiate(jumpPegPrefab, jumpPegPosition);
            go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, 0);
            jumpPegsInstances.Add(go.GetComponent<PegBehaviour>());
            jumpPegsCount--;
        }
    }

    private void DisplayPegsCount()
    {
		commonPegCountDisplay.text = (commonPegsCount + CheckUnusedPegCount(commonPegsInstances)).ToString();
		jumpPegCountDisplay.text = (jumpPegsCount + CheckUnusedPegCount(jumpPegsInstances)).ToString();
	}

    private int CheckUnusedPegCount(List<PegBehaviour> pegs)
    {
        int count = 0;
        foreach (var peg in pegs)
        {
            if (!peg.isCanPlace)
            {
                count++;
            }
        }
        return count;
    }

	private void CalculatePegsCountByLvl(int currentLvl)
    {
        switch (currentLvl)
        {
            case 1:
                {
                    commonPegsCount = 3;
                    jumpPegsCount = 1;
                    break;
                }
            case 2:
                {

                    break;
                }
            case 3:
                {

                    break;
                }
            case 4:
                {

                    break;
                }
            case 5:
                {

                    break;
                }
            case 6:
                {

                    break;
                }
            case 7:
                {

                    break;
                }
            case 8:
                {

                    break;
                }
            case 9:
                {

                    break;
                }
            case 10:
                {

                    break;
                }
        }
    }

    public void PegIsPlaced(int pegType)
    {
        switch (pegType)
        {
            case 1:
                commonPegIsPlaced = true; break;
            case 2:
                jumpPegIsPlaced = true; break;
        }
    }
}
