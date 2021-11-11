using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : MonoBehaviour {
	public Ball			ball;
	
	public GameObject border_bottom;
		
	private float		pow;
	private bool		in_use;
	
	// Use this for initialization
	void Start () {
		pow = 0;
		in_use = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Space) && transform.position.y > border_bottom.transform.position.y)
		{
			in_use = true;
			if (pow < 0.6f)
				pow += 0.01f;
			transform.Translate(Vector3.down * Time.deltaTime * 2);
		}
		else
		{
			if (in_use == true)
			{
				in_use = false;
				transform.position = ball.transform.position;
				ball.set_pow(pow);
				pow = 0f;
			}
		}
	}

	public void set_pos(float pos_y) {
			transform.position = new Vector3(transform.position.x, pos_y, transform.position.z);
	}
}