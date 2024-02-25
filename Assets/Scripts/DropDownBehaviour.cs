using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDownBehaviour : MonoBehaviour
{
    public static bool isShowDropDown;

    private List<GameObject> dropDownObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < transform.childCount; ++i)
        {
			dropDownObjects.Add(transform.GetChild(i).gameObject);
		}

        isShowDropDown = false;
	}

    // Update is called once per frame
    void Update()
    {
        ShowDropDown();
	}

    private void ShowDropDown()
    {
        if (isShowDropDown)
        {
            foreach (GameObject obj in dropDownObjects)
            {
                obj.SetActive(true);
            }
            isShowDropDown = false;
		}
        else
        {
			foreach (GameObject obj in dropDownObjects)
			{
				obj.SetActive(false);
			}
		}
    }
}
