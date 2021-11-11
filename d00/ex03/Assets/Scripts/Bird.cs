using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {
	public float	speed;
	private float	jump;
	public float 	min;

	// Use this for initialization
	void Start () {
		jump = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < min)
			GameObject.Destroy(this.gameObject);
		if (Input.GetKey(KeyCode.Space))
			jump = 0.5f;
		if (jump > 0f)
		{
			transform.Translate(Vector3.up * Time.deltaTime * speed);
			jump -= 0.05f;
		}
		else
		{
			transform.Translate(Vector3.down * Time.deltaTime * speed * 2);
		}
	}
}