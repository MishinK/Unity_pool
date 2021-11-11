using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
	public gameManager manager;

	public void play() {
		manager.changeSpeed (1);
	}

	public void pause() {
		manager.changeSpeed (0);
	}

	public void speedX2() {
		manager.changeSpeed (2);
	}

	public void speedX4() {
		manager.changeSpeed (4);
	}
}
