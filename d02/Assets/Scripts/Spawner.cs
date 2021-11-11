using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public GameObject type_of_unit;
	public AttackEntity castle;
	public float spawn_time = 10.0f;

	private float timer;
	
	
	// Use this for initialization
	void Start () {
		timer = spawn_time;
	}
	
	// Update is called once per frame
	void Update () {
		if (castle != null) {
			timer += Time.deltaTime;
			if (timer >= spawn_time) {
				timer = 0.0f;
				Vector3 position = new Vector3(transform.position.x * Random.Range(1.0f, 1.05f), transform.position.y * Random.Range(1.0f, 1.05f), 0);
				GameObject.Instantiate (type_of_unit, position, transform.rotation);
			}
		}
	}

	public void buildingDestroyed() {
		Debug.Log ("building Destroyed!");
		spawn_time += 2.5f;
	}
}
