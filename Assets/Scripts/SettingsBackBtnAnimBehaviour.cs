using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsBackBtnAnimBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject mainMenu;

    public void SwitchSettingsMenuByMainMenu()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

}
