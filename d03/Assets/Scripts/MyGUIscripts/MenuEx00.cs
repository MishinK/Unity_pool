using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEx00 : MonoBehaviour
{
	public void QuitGame() {
		Debug.Log("Quit Game");
		Application.Quit ();
	}

	public void PlayGame() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
