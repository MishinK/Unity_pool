using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuPanelGameTower : MonoBehaviour
{
	public towerScript tower;
	public Text damage, energy, range, reload;
	void Start () {
		damage.text = tower.damage.ToString ();
		energy.text = tower.energy.ToString ();
		range.text = tower.range.ToString ();
		reload.text = tower.fireRate.ToString ();
	}
}
