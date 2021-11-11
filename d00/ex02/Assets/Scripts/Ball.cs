using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	public Club		club;
	public GameObject hole;
	public GameObject border_bottom;
	public GameObject border_top;

	private float	pow;
	private bool	dir_up;
	private bool	moving;
	private int		score;

	// Use this for initialization
	void Start () {
		pow = 0f;
		dir_up = true;
		moving = false;
		score = -15;
	}
	
	void is_in_hole() {
		if (hole.transform.position.y - transform.position.y <= 0.7f && hole.transform.position.y - transform.position.y >= -0.7f && pow < 0.03f)
		{
			Debug.Log("Score: " + score);
			transform.position = new Vector3(100f, 0f, 0f);
			club.set_pos(100f);
		}
	}

	// Update is called once per frame
	void Update () {
		if (pow > 0)
		{
			if (dir_up == true)
			{
				if (transform.position.y >= border_top.transform.position.y)
				{
					transform.Translate(Vector3.down * pow);
					dir_up = false;
				}
				else
					transform.Translate(Vector3.up * pow);
			}
			else
			{
				if (transform.position.y <= border_bottom.transform.position.y)
				{
					transform.Translate(Vector3.up * pow);
					dir_up = true;
				}
				else
				{
					transform.Translate(Vector3.down * pow);
				}
			}
			is_in_hole();
			pow -= 0.005f;
		}
		else
		{
			dir_up = true;
			if (moving == true)
			{
				moving = false;
				score += 5;
				Debug.Log("Score: " + score);
				is_in_hole();
				club.set_pos(transform.position.y);
			}
		}
	}

	public void set_pow (float pow) {
		if (moving == false)
		{
			this.pow = pow;
			moving = true;
		}
	}
}