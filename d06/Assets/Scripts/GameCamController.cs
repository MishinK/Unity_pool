using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamController : MonoBehaviour
{
    public  GameController gameController;
	public AudioSource  warning;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "MainCamera")
		{
            gameController.cctvDetected = true;
			warning.Play();
		}
		
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "MainCamera")
		{
            gameController.cctvDetected = false;
			warning.Stop();
		}
    }
}
