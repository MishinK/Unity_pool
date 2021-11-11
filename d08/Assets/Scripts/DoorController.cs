using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject left_door;
	public GameObject right_door;

	private bool close;
	private Transform left_door_old;
	private Transform right_door_old;
    void Start()
    {
        close = false;
    }

    
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player" && !close)
        {
			left_door.transform.position += new Vector3(0.0003f*40000, 0f, 0f);
			right_door.transform.position -= new Vector3(0.0003f*40000, 0f, 0f);
			transform.position -= new Vector3(0f, 0.0001f*40000, 0f);
			close = true;
        }
    }

	private void OnTriggerExit(Collider other)
    {
		if (other.transform.tag == "Player" && close)
        {
			left_door.transform.position -= new Vector3(0.0003f*40000, 0f, 0f);
			right_door.transform.position += new Vector3(0.0003f*40000, 0f, 0f);
			transform.position += new Vector3(0f, 0.0001f*40000, 0f);
			close = false;
		}
    }
}
