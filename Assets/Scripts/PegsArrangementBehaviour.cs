using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class PegsArrangementBehaviour : MonoBehaviour
{                                           // 1  2  3  4  5 | 6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25
    private int[] commonPegsCountByLvlList = { 2, 4, 6, 8, 4, 4, 6, 8, 4, 6, 8, 3, 5, 7, 5, 7, 9, 6, 8, 3, 5, 7, 5, 7, 9, 6, 8, 3, 5, 7, 5, 7, 9, 6, 8, 6, 8, 3, 5, 7, 5, 7, 9, 6, 3, 5, 7, 5, 7, 9};
    private int[] jumpPegsCountByLvlList =   { 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 1, 1, 2, 2, 2, 3, 3, 3, 1, 1, 2, 2, 2, 3, 3, 3, 1, 1, 1, 1, 2, 2, 2, 3, 3, 3, 1, 2, 2, 2, 3, 3, 3};

    private string currentLevelKey = "CurrentLevel";
    private int currentLevelValue;

    [SerializeField] private RectTransform commonPegPosition;
    [SerializeField] private RectTransform jumpPegPosition;

	[HideInInspector] public int commonPegsCount;
	[HideInInspector] public int jumpPegsCount;

    [SerializeField] private GameObject commonPegPrefab;
    [SerializeField] private GameObject jumpPegPrefab;
    [HideInInspector] public bool isRemoveJumpPegMaterial = false;

    private bool isBonusesTriggersOn = true;
    private bool isBonusesTriggersOff = true;


    [SerializeField] private BallBehaviour ballBehaviour;

	private List<PegBehaviour> commonPegsInstances = new List<PegBehaviour>();
	private List<PegBehaviour> jumpPegsInstances = new List<PegBehaviour>();

    [SerializeField] private TMP_Text commonPegCountDisplay;
    [SerializeField] private TMP_Text jumpPegCountDisplay;

    private bool commonPegIsPlaced = true;
    private bool jumpPegIsPlaced = true;

    [SerializeField] private GameObject pointsBonusesList;
    [SerializeField] private GameObject speedBonusesList;
    [SerializeField] private GameObject reboundBonusesList;
    [SerializeField] private GameObject healthBonusesList;

    List<BoxCollider2D> pointsBonusesListColliders = new List<BoxCollider2D>();
    List<BoxCollider2D> speedBonusesListColliders = new List<BoxCollider2D>();
    List<BoxCollider2D> reboundBonusesListColliders = new List<BoxCollider2D>();
    List<BoxCollider2D> healthBonusesListColliders = new List<BoxCollider2D>();

    private GameObject pauseButton;

	[HideInInspector] public static List<GameObject> listOfCollectBonuses = new List<GameObject>();

    private void Start()
    {
        if (!PlayerPrefs.HasKey(currentLevelKey))
        {
            PlayerPrefs.SetInt(currentLevelKey, 1);
        }
        currentLevelValue = PlayerPrefs.GetInt(currentLevelKey, 1);
        CalculatePegsCountByLvl(currentLevelValue);
        FillListsOfBonusesColliders();
        pauseButton = GameObject.Find("PauseButton");
	}

    private void Update()
    {
        if (!GameBehaviour.isGameStart)
        {
            PlaceCommonPeg();
            PlaceJumpPeg();
            DisplayPegsCount();
        }

        ClearPegsList(ref commonPegsInstances);
        ClearPegsList(ref jumpPegsInstances);


		if (ballBehaviour.isStart && isBonusesTriggersOn)
        {
            isBonusesTriggersOn = false;
            BonusesColliderTrigger(true);
        }
        else if (!ballBehaviour.isStart && isBonusesTriggersOff)
        {
            isBonusesTriggersOff = false;
            BonusesColliderTrigger(false);
        }
	}

    private void FillListsOfBonusesColliders()
    {
        var pointBonuses = GameObject.FindGameObjectsWithTag("PointsBonus");
        foreach (var bonus in pointBonuses)
        {
            var collider = bonus.GetComponent<BoxCollider2D>();
            pointsBonusesListColliders.Add(collider);
        }

        var speedBonuses = GameObject.FindGameObjectsWithTag("SpeedBonus");
        foreach (var speed in speedBonuses)
        {
            var collider = speed.GetComponent<BoxCollider2D>();
            speedBonusesListColliders.Add(collider);
        }

        var reboundBonuses = GameObject.FindGameObjectsWithTag("ReboundBonus");
        foreach (var rebound in reboundBonuses)
        {
            var collider = rebound.GetComponent<BoxCollider2D>();
            reboundBonusesListColliders.Add(collider);
        }

        var healthBonuses = GameObject.FindGameObjectsWithTag("HealthBonus");
        foreach (var health in healthBonuses)
        {
            var collider = health.GetComponent<BoxCollider2D>();
            healthBonusesListColliders.Add(collider);
        }
    }

    private void PlaceCommonPeg()
    {
        if (commonPegsCount > 0 && commonPegIsPlaced)
        {
            commonPegIsPlaced = false;
            var go = Instantiate(commonPegPrefab, commonPegPosition);
            go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, 0);
            var peg = go.GetComponent<PegBehaviour>();
			commonPegsInstances.Add(peg);
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
			var peg = go.GetComponent<PegBehaviour>();
			jumpPegsInstances.Add(peg);
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

    private void ClearPegsList(ref List<PegBehaviour> pegs)
    {
		if (pegs.Any(x => x == null))
		{
			pegs = pegs.Where(x => x != null).ToList();
		}
    }

    private void CalculatePegsCountByLvl(int currentLvl)
    {
        pointsBonusesList.transform.GetChild(currentLvl - 1).gameObject.SetActive(true);
        speedBonusesList.transform.GetChild(currentLvl - 1).gameObject.SetActive(true);
        reboundBonusesList.transform.GetChild(currentLvl - 1).gameObject.SetActive(true);
        healthBonusesList.transform.GetChild(currentLvl - 1).gameObject.SetActive(true);

        commonPegsCount = commonPegsCountByLvlList[currentLvl - 1];
        jumpPegsCount = jumpPegsCountByLvlList[currentLvl - 1];
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

    public void LoseRestartGame()
    {
        isRemoveJumpPegMaterial = true;
        isBonusesTriggersOff = true;
        isBonusesTriggersOn = true;

        foreach (var commonPeg in commonPegsInstances)
        {
            if (!commonPeg.gameObject.activeSelf)
            {
                commonPeg.gameObject.SetActive(true);
            }
        }

        foreach (var jumpPeg in jumpPegsInstances)
        {
            if (!jumpPeg.gameObject.activeSelf)
            {
                jumpPeg.gameObject.SetActive(true);
            }
        }

        foreach (var bonus in listOfCollectBonuses)
        {
            Debug.Log(bonus);
            if (bonus != null)
                bonus.SetActive(true);
        }

        ballBehaviour.PlaceBallOnInitialPosition();
        ballBehaviour.touchCount = 0;

        ScoreBehaviour.playerScore = 0;
        ScoreBehaviour.pointsBonusesCollectCount = 0;

        if (!pauseButton.activeSelf)
            pauseButton.SetActive(true);
    }

    private void BonusesColliderTrigger(bool triggerIsOn)
    {
        foreach (var item in pointsBonusesListColliders)
        {
            item.isTrigger = triggerIsOn;
        }
        foreach (var item in speedBonusesListColliders)
        {
            item.isTrigger = triggerIsOn;
        }
        foreach (var item in reboundBonusesListColliders)
        {
            item.isTrigger = triggerIsOn;
        }
        foreach (var item in pointsBonusesListColliders)
        {
            item.isTrigger = triggerIsOn;
        }
    }
}
