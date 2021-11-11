using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_moving : MonoBehaviour
{
	public GameObject goal;
	public float speed;

	Vector3 origPos;

	float distance;
	bool back = false;

	public void Start()
	{
    	origPos = transform.position;
	}

	public void Update()
	{
    	if(!back)
    	{
        	distance = Vector3.Distance(transform.position, goal.transform.position);
        	if (distance > 0.01f)
            	transform.position = Vector3.MoveTowards(transform.position, goal.transform.position, speed);
        	else
            	back = true;
    	}
    	else
    	{
        	distance = Vector3.Distance(transform.position, origPos);
        	if (distance > 0.01f)
            	transform.position = Vector3.MoveTowards(transform.position, origPos, speed);
        	else
            	back = false;
    	}
	}
}
