using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuPanelGamePlayerInfo : MonoBehaviour
{
	public GameObject objmanager;
	private gameManager manager;
	public Text userHP, userEnergy;

	void Start () {
		manager = objmanager.GetComponent<MenuButtons> ().manager;
	}
	void Update () {
		int playerHP = manager.playerHp;
		if (playerHP < 0)
			playerHP = 0;
		userHP.text = playerHP.ToString () + "hp";
		userEnergy.text = manager.playerEnergy.ToString ();
	}
}
