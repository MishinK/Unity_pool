using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baloon : MonoBehaviour
{
	private int		breath;
	private float 	elapsed;
    
    void Start()
    {
        transform.localScale += new Vector3(4, 4, 0);
		breath = 4;
		elapsed = 0f;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && breath > 0)
        {
			breath -= 1;
			transform.localScale += new Vector3(1F, 1F, 0);
        }
		else
		{
			transform.localScale += new Vector3(-0.01F, -0.01F, 0);
		}
		elapsed += Time.deltaTime;
		if (elapsed >= 0.3f)
		{
        	elapsed = elapsed % 0.3f;
			if (breath < 4)
			{
				breath++;
			}
		}
		if (transform.localScale.x <= 0.2 || transform.localScale.x >= 9)
		{
			GameObject.Destroy(this);
			Debug.Log("Ballon life time: " + Mathf.RoundToInt(Time.time) + "s");
		}

    }
}
