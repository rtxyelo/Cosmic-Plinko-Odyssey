using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] List<GameObject> pointsBonusesList = new List<GameObject>();
    [SerializeField] List<GameObject> speedBonusesList = new List<GameObject>();
    [SerializeField] List<GameObject> reboundBonusesList = new List<GameObject>();
    [SerializeField] List<GameObject> healthBonusesList = new List<GameObject>();


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

        if (ballBehaviour.isStart && isBonusesTriggersOn)
        {
            Debug.Log("BonusesColliderTrigger ON");
            isBonusesTriggersOn = false;
            BonusesColliderTrigger(true);
        }
        else if (!ballBehaviour.isStart && isBonusesTriggersOff)
        {
            Debug.Log("BonusesColliderTrigger OFF");
            isBonusesTriggersOff = false;
            BonusesColliderTrigger(false);
        }
        //Debug.Log("isBonusesTriggersOff " + isBonusesTriggersOff);
        //Debug.Log("isBonusesTriggersOn " + isBonusesTriggersOn);
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


        //foreach (var point in pointsBonusesList)
        //{
        //    var collider = point.GetComponentsInChildren<BoxCollider2D>();
        //    pointsBonusesListColliders.AddRange(collider);
        //}

        //foreach (var speed in speedBonusesList)
        //{
        //    var collider = speed.GetComponentsInChildren<BoxCollider2D>();
        //    speedBonusesListColliders.AddRange(collider);
        //}

        //foreach (var rebound in reboundBonusesList)
        //{
        //    var collider = rebound.GetComponentsInChildren<BoxCollider2D>();
        //    reboundBonusesListColliders.AddRange(collider);
        //}

        //foreach (var health in healthBonusesList)
        //{
        //    var collider = health.GetComponentsInChildren<BoxCollider2D>();
        //    healthBonusesListColliders.AddRange(collider);
        //}
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
                    commonPegsCount = 5;
                    jumpPegsCount = 0;
                    pointsBonusesList[0].SetActive(true);
                    break;
                }
            case 2:
                {
                    commonPegsCount = 7;
                    jumpPegsCount = 1;
                    pointsBonusesList[1].SetActive(true);
                    speedBonusesList[0].SetActive(true);
                    break;
                }
            case 3:
                {
                    commonPegsCount = 8;
                    jumpPegsCount = 2;
                    pointsBonusesList[2].SetActive(true);
                    speedBonusesList[1].SetActive(true);
                    reboundBonusesList[0].SetActive(true);
                    break;
                }
            case 4:
                {
                    commonPegsCount = 9;
                    jumpPegsCount = 1;
                    pointsBonusesList[3].SetActive(true);
                    speedBonusesList[2].SetActive(true);
                    reboundBonusesList[1].SetActive(true);
                    healthBonusesList[0].SetActive(true);
                    break;
                }
            case 5:
                {
                    commonPegsCount = 8;
                    jumpPegsCount = 3;
                    break;
                }
            case 6:
                {
                    commonPegsCount = 9;
                    jumpPegsCount = 3;
                    break;
                }
            case 7:
                {
                    commonPegsCount = 10;
                    jumpPegsCount = 2;
                    break;
                }
            case 8:
                {
                    commonPegsCount = 11;
                    jumpPegsCount = 3;
                    break;
                }
            case 9:
                {
                    commonPegsCount = 12;
                    jumpPegsCount = 4;
                    break;
                }
            case 10:
                {
                    commonPegsCount = 15;
                    jumpPegsCount = 5;
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

    public void LoseRestartGame()
    {
        commonPegIsPlaced = false;
        jumpPegIsPlaced = false;
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
        int currentLvl = PlayerPrefs.GetInt(currentLevelKey, 1);
        switch (currentLvl)
        {
            case 1:
                {
                    pointsBonusesListColliders[0].isTrigger = triggerIsOn;
                    break;
                }
            case 2:
                {
                    pointsBonusesListColliders[0].isTrigger = triggerIsOn;
                    speedBonusesListColliders[0].isTrigger = triggerIsOn;
                    break;
                }
            case 3:
                {
                    pointsBonusesListColliders[0].isTrigger = triggerIsOn;
                    pointsBonusesListColliders[1].isTrigger = triggerIsOn;
                    speedBonusesListColliders[0].isTrigger = triggerIsOn;
                    reboundBonusesListColliders[0].isTrigger = triggerIsOn;
                    break;
                }
            case 4:
                {
                    pointsBonusesListColliders[0].isTrigger = triggerIsOn;
                    pointsBonusesListColliders[1].isTrigger = triggerIsOn;
                    speedBonusesListColliders[0].isTrigger = triggerIsOn;
                    reboundBonusesListColliders[0].isTrigger = triggerIsOn;
                    healthBonusesListColliders[0].isTrigger = triggerIsOn;
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
}
