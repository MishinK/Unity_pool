using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
	public GameObject   target_out;

    void OnCollisionEnter2D(Collision2D other)
    {
		other.transform.position = target_out.transform.position;
    }
}
