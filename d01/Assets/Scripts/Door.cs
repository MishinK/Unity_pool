using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	public GameObject 	target;
   	public GameObject   door;

    void OnTriggerStay2D(Collider2D other)
    {
		if (other.name == target.name)
        {
			if (door.transform.localScale.x > 0.00001f)
                door.transform.localScale = new Vector2(door.transform.localScale.x/1.1f, door.transform.localScale.y);
            if (door.transform.localScale.y > 0.00001f)
                door.transform.localScale = new Vector2(door.transform.localScale.x, door.transform.localScale.y/1.1f);
			if (transform.localScale.y > 0.00001f)
				transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y/1.1f);
		}
    }
}
