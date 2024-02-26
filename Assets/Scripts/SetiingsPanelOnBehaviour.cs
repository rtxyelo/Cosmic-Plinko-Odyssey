using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetiingsPanelOnBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject mainMenu;

    public void SwitchMainMenuBySettingsMenu()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
}
