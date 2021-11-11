using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
	private float	speed;
	public GameObject   end;
	public GameObject   target;
	// Use this for initialization
	void Start () {
		speed = Random.Range(0.5f, 2f);
	}
	
	void Update () {
		transform.Translate(Vector3.down * Time.deltaTime * speed);
		if (transform.position.y <= end.transform.position.y)
		{
			GameObject.Destroy(this.gameObject);
		}
	}

	void OnTriggerStay2D(Collider2D other)
    {
		if (other.name == target.name)
        {
			GameObject.Destroy(other);
		}
	}
}
