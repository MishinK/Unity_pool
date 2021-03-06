using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public GameObject[] objects;
    public GameObject Laser_door;
    public GameObject fanParticle;
    public GameObject lights;
    public GUIController gui;

    // Detection Level Control
    [HideInInspector] public float detectionLevel;
    [HideInInspector] public bool detected;
    [HideInInspector] public bool run;
    [HideInInspector] public bool cctvDetected;
    private bool warning;

    [HideInInspector] public bool cardKey;
    [HideInInspector] public bool document;

    // GameOver
    [HideInInspector] public bool gameOver;
    public AudioSource resetSound;
    private bool resetPlay;

    // Background Music
    public AudioSource normalAudio;
    public AudioSource panicAudio;
    public AudioSource warningAudio;
	public AudioSource keyUpAudio;

    // Interaction
    [HideInInspector] public int objectVisible = -1;
    

	[HideInInspector] public bool invis = false;
    private void Update()
    {
        if (!gameOver)
            CalculateDetectionLevel();
        if (detectionLevel >= 1.0f)
            GameStatus("Mission Has Failed\nReinitializing........");
        if (document)
            GameStatus("Mission Completed\nReinitializing........");
        SetInstruction();
        if (objectVisible > 0 && Input.GetKeyDown("e"))
            TakeAction(objectVisible);
    }

    private void SetInstruction()
    {
        string instruction;
        switch (objectVisible)
        {
            case 1:
                instruction = "Press E to activate";
                break;
            case 2:
                instruction = "Press E to pickup";
                break;
            case 3:
                if (cardKey)
                    instruction = "Press E to use a cardkey";
                else
                    instruction = "Need to find a cardkey";
                break;
            case 4:
                instruction = "Press E to pickup";
                break;
            default:
                instruction = "";
                break;
        }
        StartCoroutine(FadeInandOut(instruction));
    }

    private IEnumerator FadeInandOut(string instruction)
    {
        if (objectVisible > 0)
            gui.instruction.CrossFadeAlpha(1, 2.0f, false);
        else
        {
            gui.instruction.CrossFadeAlpha(0, 2.0f, false);
            yield return new WaitForSeconds(1.0f);
        }
        gui.SetInstruction(instruction);
    }

    private void CalculateDetectionLevel()
    {
        float detectionCalc = -0.001f;

        if (cctvDetected)
		{
			if (invis)
            	detectionCalc += 0.003f;
			else
				detectionCalc += 0.01f;
		}
        if (detected)
            detectionCalc += 0.005f;
        if (run)
            detectionCalc += 0.003f;
        detectionLevel += detectionCalc;
        if (detectionLevel < 0f || detectionLevel > 1f)
            detectionLevel = (detectionLevel < 0f ? 0f : 1f);
        gui.SetDetectionBar(detectionLevel);
        if (detectionLevel >= 0.75 && !warning)
            BlinkText(true);
        if (detectionLevel < 0.75 && warning)
            BlinkText(false);
    }

    private void BlinkText(bool trueOrFalse)
    {
        gui.StartBlinking(trueOrFalse);
        warning = trueOrFalse;
        if (warning)
        {
            normalAudio.Stop();
            panicAudio.Play();
            warningAudio.Play();
        }
        else
        {
            panicAudio.Stop();
            normalAudio.Play();
            warningAudio.Stop();
        }
    }

    private void GameStatus(string str)
    {
        gui.SetInstruction("");
        gameOver = true;
        gui.GameMessage(str);
        if (!resetPlay)
        {
            resetSound.Play();
            resetPlay = true;
        }
        if (!document)
        {
            gui.StartBlinking(false);
            if (warning)
                BlinkText(false);
        }
    }


	public void TakeAction(int obj)
    {
        switch (obj)
        {
            case 1:
                fanParticle.SetActive(true);
                objects[0].transform.name = "ActivatedFan";
                break;
            case 2:
                cardKey = true;
                keyUpAudio.Play();
                Destroy(objects[1]);
                lights.SetActive(true);
                lights.GetComponent<AudioSource>().Play();
				normalAudio.Stop();
				panicAudio.Play();
                break;
            case 3:
                if (cardKey)
                {
                    cardKey = false;
                    objects[2].GetComponent<AudioSource>().Play();
					objects[4].SetActive(true);
                    Destroy(Laser_door);
                }
                break;
            case 4:
                document = true;
                Destroy(objects[3]);
                break;
        }
    }
}
