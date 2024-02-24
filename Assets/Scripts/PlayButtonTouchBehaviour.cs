using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonTouchBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject levelsPanel;

    public void PlayButtonTouch()
    {
        menuPanel.SetActive(false);
        levelsPanel.SetActive(true);
    }
}
