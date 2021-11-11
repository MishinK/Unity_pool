using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GolfController : MonoBehaviour
{
	public SoundManager sound;

    public CameraController cameraScript;
    public GUIController uiController;
    public BallController ballController;
    public GameObject ball;
    public GameObject arrow;
    public GameObject[] teeboxes;
    public GameObject[] golfHole;
    public GameObject inWaterText;
    public int[] parNumber;
    public GameObject holeInMessage;
    public ScorePanelController scorePanelController;
    private bool isPowerBarStarted;
    private int shotNumber;
    private int holeNumber;
    [HideInInspector] public int clubNumber;
    private bool clubChanged;

    [HideInInspector] public bool isShoot;
    [HideInInspector] public float terrainForward;
    [HideInInspector] public float terrainUp;
    
	public int terrainIndex;

    private float forward;
    private float up;

    private Vector3 prevPosition;

    private void Start()
    {
        clubNumber = 1;
        holeNumber = 1;
        SetHoleInfo();
    }

    void Update()
    {
        if (cameraScript.locked && !ballController.hole)
        {
            if (ballController.isSleeping)
            {
                Quaternion ballRotation = Camera.main.transform.rotation;
                ballRotation.x = 0;
                ballRotation.z = 0;

                ball.transform.rotation = Quaternion.RotateTowards(ball.transform.rotation, ballRotation, 20.0f);
            }
            else
            {
                TurnOffUI();
            }
            if (ballController.isSleeping && !uiController.onLockScreen.activeSelf)
            {
                uiController.onLockScreen.SetActive(true);
                arrow.SetActive(true);
            }
            if (Input.GetKeyDown("space"))
            {
                if (!isPowerBarStarted)
                {
                    uiController.StartPowerBar();
                    isPowerBarStarted = true;
                }
                else
                {
                    prevPosition = ball.transform.position;
                    ballController.Hit(forward, up);
                    uiController.ShotInfo(++shotNumber);
                    isShoot = true;
                }

            }
            if (clubChanged && Input.GetKeyDown("r"))
            {
                if (clubNumber < 4)
				{ 
					arrow.transform.Rotate(-20f, 0f, 0f, Space.World);
                    clubNumber++;
				}
                else
				{
                    clubNumber = 1;
					arrow.transform.Rotate(60f, 0f, 0f, Space.World);
				}
                SetClubInfo();
            }
        }
        else if (!cameraScript.locked)
        {
            TurnOffUI();
        }
        if (ballController.hole)
        {
            holeInMessage.SetActive(true);
            scorePanelController.UpdateScore(shotNumber, holeNumber);
            scorePanelController.Activate();
            if (Input.GetKeyDown("return"))
            {
                if (holeNumber < 3)
                {
					sound.Play("Applause");
                    isPowerBarStarted = false;
                    uiController.StopPowerBar(true);
                    isShoot = false;
                    holeNumber++;
                    shotNumber = 0;
                    SetHoleInfo();
                    holeInMessage.SetActive(false);
                    scorePanelController.DeActivate();
                }
                else
				{
					sound.Play("Victory");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}
            }
        }
        if (ballController.inWater)
        {
            ball.transform.position = prevPosition;
            inWaterText.SetActive(true);
            if (Input.GetKeyDown("return"))
            {
                uiController.ShotInfo(++shotNumber);
                ballController.inWater = false;
                inWaterText.SetActive(false);
            }

        }
		if (terrainIndex == 0 )
        {
			clubChanged = true;
        }
	
        if (terrainIndex == 1)
        {
			clubChanged = false;
			
			if (clubNumber == 1)
				arrow.transform.Rotate(-40f, 0f, 0f, Space.World);
			else if (clubNumber == 2)
				arrow.transform.Rotate(-20f, 0f, 0f, Space.World);
			else if (clubNumber == 4)
				arrow.transform.Rotate(20f, 0f, 0f, Space.World);
			
			
			clubNumber = 3;
		
            SetClubInfo();
            
        }

        if (terrainIndex == 2 )
        {
			clubChanged = false;
			
			if (clubNumber == 1)
				arrow.transform.Rotate(-60f, 0f, 0f, Space.World);
			else if (clubNumber == 2)
				arrow.transform.Rotate(-40f, 0f, 0f, Space.World);
			else if (clubNumber == 3)
				arrow.transform.Rotate(-20f, 0f, 0f, Space.World);
		
			clubNumber = 4;
			
            SetClubInfo();
        }


        if (Input.GetKeyDown(KeyCode.Tab))
            scorePanelController.Activate();
        if (Input.GetKeyUp(KeyCode.Tab))
            scorePanelController.DeActivate();
    }


    private void SetHoleInfo()
    {
        uiController.HoleInfo(holeNumber, parNumber[holeNumber - 1]);
        Transform t = teeboxes[holeNumber - 1].transform;
        Vector3 transport = new Vector3(t.position.x, t.position.y, t.position.z);
        ball.transform.position = transport;
        if (holeNumber > 1)
            golfHole[holeNumber - 2].SetActive(false);
        golfHole[holeNumber - 1].SetActive(true);
        ball.transform.LookAt(golfHole[holeNumber - 1].transform);
        ballController.hole = false;
    }

    private void SetClubInfo()
    {
        uiController.ClubInfo(clubNumber);
        switch (clubNumber)
        {
			case 1:
                forward = 2.0f;
                up = 0.8f;
                
                arrow.transform.localScale = new Vector3(0.3f, 0.3f, 0.5f);
                break;
            case 2:
                forward = 3.0f;
                up = 0.8f;
              
                arrow.transform.localScale = new Vector3(0.3f, 0.3f, 0.9f);
                break;
            case 3:
                forward = 2.5f;
                up = 1.5f;
                
                arrow.transform.localScale = new Vector3(0.3f, 0.3f, 0.7f);
                break;
            case 4:
                forward = 1.8f;
                up = 2.2f;
               
                arrow.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                break;
        }
        forward *= terrainForward;
        up *= terrainUp;
    }

    private void TurnOffUI()
    {
        uiController.onLockScreen.SetActive(false);
        if (isPowerBarStarted)
        {
            uiController.StopPowerBar(true);
            isPowerBarStarted = false;
        }
        arrow.SetActive(false);
    }
}
