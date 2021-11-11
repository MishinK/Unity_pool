using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_target : MonoBehaviour
{
    public GameObject   target;
   
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name != target.name)
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
		if (other.gameObject.name != target.name)
        	gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
