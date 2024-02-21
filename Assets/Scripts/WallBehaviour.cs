using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviour : MonoBehaviour
{
	// setting parameters with and heights
	private float SettingWidth = 5.63f;
    private float SettingHeight = 10f;

    [SerializeField]
    private GameObject topWall;
    [SerializeField]
    private GameObject bottomWall;  
    [SerializeField]
    private GameObject leftWall;
    [SerializeField]
    private GameObject rightWall;

    private Vector3 topLeftPoint;
    private Vector3 bottomLeftPoint;
    private Vector3 bottomRightPoint;
    private Vector3 topRightPoint;

    private int screenWidth;
    private int screenHeight;
    private Vector3 cameraPos;

    private float coordWidth;
    private float coordHeight;

    // Start is called before the first frame update
    void Start()
    {
        // get screen size in pixels
        screenWidth = Camera.main.pixelWidth;
        screenHeight = Camera.main.pixelHeight;
        cameraPos = Camera.main.transform.position;

		// get corner point
		bottomLeftPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, -cameraPos.z));
		topLeftPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, screenHeight, -cameraPos.z));
		bottomRightPoint = Camera.main.ScreenToWorldPoint(new Vector3(screenWidth, 0, -cameraPos.z));
		topRightPoint = Camera.main.ScreenToWorldPoint(new Vector3(screenWidth, screenHeight, -cameraPos.z));

		// get screen size in coords
		coordWidth = Vector3.Distance(bottomRightPoint, bottomLeftPoint);
		coordHeight = Vector3.Distance(topLeftPoint, bottomLeftPoint);

		// set walls scale
		leftWall.transform.localScale = new Vector3(0, 1, 0) * (coordHeight / SettingHeight) * leftWall.transform.localScale.y + new Vector3(leftWall.transform.localScale.x, 0, leftWall.transform.localScale.z);
        rightWall.transform.localScale = new Vector3(0, 1, 0) * (coordHeight / SettingHeight) * rightWall.transform.localScale.y + new Vector3(rightWall.transform.localScale.x, 0, rightWall.transform.localScale.z);
        topWall.transform.localScale = new Vector3(1, 0, 0) * (coordWidth / SettingWidth) * topWall.transform.localScale.x + new Vector3(0, topWall.transform.localScale.y, topWall.transform.localScale.z);
        bottomWall.transform.localScale = new Vector3(1, 0, 0) * (coordWidth / SettingWidth) * bottomWall.transform.localScale.x + new Vector3(0, bottomWall.transform.localScale.y, bottomWall.transform.localScale.z);

        // set walls position
        leftWall.transform.position = (bottomLeftPoint + topLeftPoint) / 2 + Mathf.Sign((bottomLeftPoint + topLeftPoint).x) * new Vector3(1, 0, 0) * leftWall.transform.localScale.x / 2;
        rightWall.transform.position = (bottomRightPoint + topRightPoint) / 2 + Mathf.Sign((bottomRightPoint + topRightPoint).x) * new Vector3(1, 0, 0) * rightWall.transform.localScale.x / 2;
        topWall.transform.position = (topLeftPoint + topRightPoint) / 2 + Mathf.Sign((topLeftPoint + topRightPoint).y) * new Vector3(0, 1, 0) * topWall.transform.localScale.y / 2;
        bottomWall.transform.position = (bottomLeftPoint + bottomRightPoint) / 2 + Mathf.Sign((bottomLeftPoint + bottomRightPoint).y) * new Vector3(0, 1, 0) * bottomWall.transform.localScale.y / 2;
    }

	// Update is called once per frame
	void Update()
    {
        
    }
}
