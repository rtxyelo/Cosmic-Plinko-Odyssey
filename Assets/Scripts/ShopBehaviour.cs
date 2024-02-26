using System.Collections;
using UnityEngine;
using TMPro;

public class ShopBehaviour : MonoBehaviour
{
    private string _currentBallKey = "CurrentBall";
    private string _moneyCountKey = "MoneyCount";
    private string _hasNormalBallKey = "HasNormalBall";
    private string _hasMeduimBallKey = "HasMediumBall";
    private string _hasHardBallKey = "HasHardBall";

    [SerializeField] private TMP_Text _selectBtnText;
    [SerializeField] private TMP_Text _balanceText;

    private int _curentBall = 0;
    private int _normalBallPrice = 5;
    private int _mediumBallPrice = 10;
    private int _hardBallPrice = 20;

    void Start()
    {
        if (!PlayerPrefs.HasKey(_currentBallKey))
        {
            PlayerPrefs.SetInt(_currentBallKey, 0);
        }
        if (!PlayerPrefs.HasKey(_moneyCountKey))
        {
            PlayerPrefs.SetInt(_moneyCountKey, 0);
        }
        if (!PlayerPrefs.HasKey(_hasNormalBallKey))
        {
            PlayerPrefs.SetInt(_hasNormalBallKey, 0);
        }
        if (!PlayerPrefs.HasKey(_hasMeduimBallKey))
        {
            PlayerPrefs.SetInt(_hasMeduimBallKey, 0);
        }
        if (!PlayerPrefs.HasKey(_hasHardBallKey))
        {
            PlayerPrefs.SetInt(_hasHardBallKey, 0);
        }


        //PlayerPrefs.SetInt(_currentBallKey, 0);
        //PlayerPrefs.SetInt(_moneyCountKey, 0);
        //PlayerPrefs.SetInt(_hasNormalBallKey, 0);
        //PlayerPrefs.SetInt(_hasMeduimBallKey, 0);
        //PlayerPrefs.SetInt(_hasHardBoatKey, 0);


        Debug.Log("currentBallKey " + PlayerPrefs.GetInt(_currentBallKey, 0));
        Debug.Log("moneyCount " + PlayerPrefs.GetInt(_moneyCountKey, 0));
        Debug.Log("hasNormalBallKey " + PlayerPrefs.GetInt(_hasNormalBallKey, 0));
        Debug.Log("hasMeduimBallKey " + PlayerPrefs.GetInt(_hasMeduimBallKey, 0));
        Debug.Log("hasHardBallKey " + PlayerPrefs.GetInt(_hasHardBallKey, 0));

        _balanceText.text = "$ " + PlayerPrefs.GetInt(_moneyCountKey, 0).ToString();

        if (_curentBall == PlayerPrefs.GetInt(_currentBallKey, 0))
        {
            PlayerPrefs.SetInt(_currentBallKey, 0);
            _selectBtnText.text = "Selected";
        }
    }

    #region Public Methods
    public void BoatSelect()
    {
        // Easy ball
        if (_curentBall == 0)
        {
            if (_curentBall != PlayerPrefs.GetInt(_currentBallKey, 0))
            {
                PlayerPrefs.SetInt(_currentBallKey, 0);
                _selectBtnText.text = "Selected";
            }
        }
        // Normal ball
        if (_curentBall == 1)
        {
            // Just select
            if (_curentBall != PlayerPrefs.GetInt(_currentBallKey, 0) && PlayerPrefs.GetInt(_hasNormalBallKey, 0) == 1)
            {
                PlayerPrefs.SetInt(_currentBallKey, 1);
                _selectBtnText.text = "Selected";

                Debug.Log("currentBallKey " + PlayerPrefs.GetInt(_currentBallKey, 0));
                Debug.Log("moneyCountKey " + PlayerPrefs.GetInt(_moneyCountKey, 0));
                Debug.Log("hasNormalBallKey " + PlayerPrefs.GetInt(_hasNormalBallKey, 0));
                Debug.Log("hasMeduimBallKey " + PlayerPrefs.GetInt(_hasMeduimBallKey, 0));
                Debug.Log("hasHardBallKey " + PlayerPrefs.GetInt(_hasHardBallKey, 0));
            }
            // Else try to buy
            else if (PlayerPrefs.GetInt(_hasNormalBallKey, 0) == 0)
            {
                BuyBoat(_curentBall);
            }
        }
        // Medium Boat
        else if (_curentBall == 2)
        {
            // Just select
            if (_curentBall != PlayerPrefs.GetInt(_currentBallKey, 0) && PlayerPrefs.GetInt(_hasMeduimBallKey, 0) == 1)
            {
                PlayerPrefs.SetInt(_currentBallKey, 2);
                _selectBtnText.text = "Selected";

                Debug.Log("currentBallKey " + PlayerPrefs.GetInt(_currentBallKey, 0));
                Debug.Log("moneyCountKey " + PlayerPrefs.GetInt(_moneyCountKey, 0));
                Debug.Log("hasNormalBallKey " + PlayerPrefs.GetInt(_hasNormalBallKey, 0));
                Debug.Log("hasMeduimBallKey " + PlayerPrefs.GetInt(_hasMeduimBallKey, 0));
                Debug.Log("hasHardBallKey " + PlayerPrefs.GetInt(_hasHardBallKey, 0));
            }
            // Else try to buy
            else if (PlayerPrefs.GetInt(_hasMeduimBallKey, 0) == 0)
            {
                BuyBoat(_curentBall);
            }
        }
        // Hard boat
        if (_curentBall == 3)
        {
            // Just select
            if (_curentBall != PlayerPrefs.GetInt(_currentBallKey, 0) && PlayerPrefs.GetInt(_hasHardBallKey, 0) == 1)
            {
                PlayerPrefs.SetInt(_currentBallKey, 3);
                _selectBtnText.text = "Selected";

                Debug.Log("currentBallKey " + PlayerPrefs.GetInt(_currentBallKey, 0));
                Debug.Log("moneyCountKey " + PlayerPrefs.GetInt(_moneyCountKey, 0));
                Debug.Log("hasNormalBallKey " + PlayerPrefs.GetInt(_hasNormalBallKey, 0));
                Debug.Log("hasMeduimBallKey " + PlayerPrefs.GetInt(_hasMeduimBallKey, 0));
                Debug.Log("hasHardBallKey " + PlayerPrefs.GetInt(_hasHardBallKey, 0));
            }
            // Else try to buy
            else if (PlayerPrefs.GetInt(_hasHardBallKey, 0) == 0)
            {
                BuyBoat(_curentBall);
            }
        }
    }
    #endregion



    #region Private Methods
    private void BuyBoat(int boatForBuy)
    {
        // Normal boat
        if (boatForBuy == 1)
        {
            if (PlayerPrefs.GetInt(_moneyCountKey, 0) >= _normalBallPrice)
            {
                PlayerPrefs.SetInt(_hasNormalBallKey, 1);
                PlayerPrefs.SetInt(_moneyCountKey, PlayerPrefs.GetInt(_moneyCountKey, 0) - _normalBallPrice);
                _balanceText.text = "$ " + PlayerPrefs.GetInt(_moneyCountKey, 0).ToString();
                _selectBtnText.text = "Select";

                Debug.Log("currentBallKey " + PlayerPrefs.GetInt(_currentBallKey, 0));
                Debug.Log("moneyCountKey " + PlayerPrefs.GetInt(_moneyCountKey, 0));
                Debug.Log("hasNormalBallKey " + PlayerPrefs.GetInt(_hasNormalBallKey, 0));
                Debug.Log("hasMeduimBallKey " + PlayerPrefs.GetInt(_hasMeduimBallKey, 0));
                Debug.Log("hasHardBallKey" + PlayerPrefs.GetInt(_hasHardBallKey, 0));
            }
        }
        // Medium boat
        else if (boatForBuy == 2)
        {
            if (PlayerPrefs.GetInt(_moneyCountKey, 0) >= _mediumBallPrice)
            {
                PlayerPrefs.SetInt(_hasMeduimBallKey, 1);
                PlayerPrefs.SetInt(_moneyCountKey, PlayerPrefs.GetInt(_moneyCountKey, 0) - _mediumBallPrice);
                _balanceText.text = "$ " + PlayerPrefs.GetInt(_moneyCountKey, 0).ToString();
                _selectBtnText.text = "Select";

                Debug.Log("currentBallKey " + PlayerPrefs.GetInt(_currentBallKey, 0));
                Debug.Log("moneyCountKey " + PlayerPrefs.GetInt(_moneyCountKey, 0));
                Debug.Log("hasNormalBallKey " + PlayerPrefs.GetInt(_hasNormalBallKey, 0));
                Debug.Log("hasMeduimBallKey " + PlayerPrefs.GetInt(_hasMeduimBallKey, 0));
                Debug.Log("hasHardBallKey" + PlayerPrefs.GetInt(_hasHardBallKey, 0));
            }
        }
        // Hard boat
        else if (boatForBuy == 3)
        {
            if (PlayerPrefs.GetInt(_moneyCountKey, 0) >= _hardBallPrice)
            {
                PlayerPrefs.SetInt(_hasHardBallKey, 1);
                PlayerPrefs.SetInt(_moneyCountKey, PlayerPrefs.GetInt(_moneyCountKey, 0) - _hardBallPrice);
                _balanceText.text = "$ " + PlayerPrefs.GetInt(_moneyCountKey, 0).ToString();
                _selectBtnText.text = "Select";

                Debug.Log("currentBallKey " + PlayerPrefs.GetInt(_currentBallKey, 0));
                Debug.Log("moneyCountKey " + PlayerPrefs.GetInt(_moneyCountKey, 0));
                Debug.Log("hasNormalBallKey " + PlayerPrefs.GetInt(_hasNormalBallKey, 0));
                Debug.Log("hasMeduimBallKey " + PlayerPrefs.GetInt(_hasMeduimBallKey, 0));
                Debug.Log("hasHardBallKey " + PlayerPrefs.GetInt(_hasHardBallKey, 0));
            }
        }
    }

    private void SetSelectButtonText(int _curentBoatNum)
    {
        Debug.Log("currentBallKey " + PlayerPrefs.GetInt(_currentBallKey, 0));
        Debug.Log("currentBall " + _curentBall);

        // Current select boat
        int currBoat = PlayerPrefs.GetInt(_currentBallKey, 0);

        // Easy boat
        if (_curentBoatNum == 0 && currBoat != _curentBoatNum)
        {
            _selectBtnText.text = "Select";
        }
        else if (_curentBoatNum == 0 && currBoat == _curentBoatNum)
        {
            _selectBtnText.text = "Selected";
        }

        // Normal boat
        else if (_curentBoatNum == 1 && currBoat != _curentBoatNum && PlayerPrefs.GetInt(_hasNormalBallKey, 0) == 1)
        {
            _selectBtnText.text = "Select";
        }
        else if (_curentBoatNum == 1 && currBoat == _curentBoatNum && PlayerPrefs.GetInt(_hasNormalBallKey, 0) == 1)
        {
            _selectBtnText.text = "Selected";
        }
        else if (_curentBoatNum == 1 && PlayerPrefs.GetInt(_hasNormalBallKey, 0) != 1)
        {
            _selectBtnText.text = $"BUY ${_normalBallPrice}";
        }

        // Medium boat
        else if (_curentBoatNum == 2 && currBoat != _curentBoatNum && PlayerPrefs.GetInt(_hasMeduimBallKey, 0) == 1)
        {
            _selectBtnText.text = "Select";
        }
        else if (_curentBoatNum == 2 && currBoat == _curentBoatNum && PlayerPrefs.GetInt(_hasMeduimBallKey, 0) == 1)
        {
            _selectBtnText.text = "Selected";
        }
        else if (_curentBoatNum == 2 && PlayerPrefs.GetInt(_hasMeduimBallKey, 0) != 1)
        {
            _selectBtnText.text = $"BUY ${_mediumBallPrice}";
        }

        // Hard boat
        else if (_curentBoatNum == 3 && currBoat != _curentBoatNum && PlayerPrefs.GetInt(_hasHardBallKey, 0) == 1)
        {
            _selectBtnText.text = "Select";
        }
        else if (_curentBoatNum == 3 && currBoat == _curentBoatNum && PlayerPrefs.GetInt(_hasHardBallKey, 0) == 1)
        {
            _selectBtnText.text = "Selected";
        }
        else if (_curentBoatNum == 3 && PlayerPrefs.GetInt(_hasHardBallKey, 0) != 1)
        {
            _selectBtnText.text = $"BUY ${_hardBallPrice}";
        }
    }
    #endregion
}