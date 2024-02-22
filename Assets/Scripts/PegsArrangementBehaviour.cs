using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegsArrangementBehaviour : MonoBehaviour
{
    private string currentLevelKey = "CurrentLevel";
    private int currentLevelValue;

    [SerializeField] private RectTransform commonPegPosition;
    //private Transform jumpPegPosition;
    //private Transform speedPegPosition;

    private int commonPegsCount;

    [SerializeField] private GameObject pegPrefab;

    private bool pegIsPlaced = false;

    private void Start()
    {
        Debug.Log("commonPegPosition " + commonPegPosition.anchoredPosition);

        if (!PlayerPrefs.HasKey(currentLevelKey))
        {
            PlayerPrefs.SetInt(currentLevelKey, 1);
        }
        currentLevelValue = PlayerPrefs.GetInt(currentLevelKey, 1);
        CalculatePegsCountByLvl(currentLevelValue);

        // Instantiate pegs by positions
        PlaceCommonPeg();

    }

    private void Update()
    {
        while (commonPegsCount > 0 && pegIsPlaced)
        {
            pegIsPlaced = false;
            PlaceCommonPeg();
        }
    }

    private void PlaceCommonPeg()
    {
        //GameObject pegGo = Instantiate(pegPrefab, commonPegPosition.anchoredPosition, Quaternion.identity);
        GameObject pegGo = Instantiate(pegPrefab, commonPegPosition);
        Debug.Log("Peg is placed ");
        commonPegsCount--;
    }

    private void CalculatePegsCountByLvl(int currentLvl)
    {
        switch (currentLvl)
        {
            case 1:
                {
                    commonPegsCount = 3;
                    Debug.Log("Common Pegs Count " + commonPegsCount);
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

    public void PegIsPlaced()
    {
        //pegIsPlaced = true;
    }
}
