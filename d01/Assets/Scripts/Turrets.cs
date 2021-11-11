using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrets : MonoBehaviour
{
	public GameObject			R_prefab;
	public GameObject			Y_prefab;
	public GameObject			B_prefab;
	public GameObject			start;

	private float 				elapsed;

	private GameObject			R_key;
	private GameObject			Y_key;
	private GameObject			B_key;

	// Use this for initialization
	void Start () {
		elapsed = 0f;
		R_key = null;
		Y_key = null;
		B_key = null;
	}
	
	// Update is called once per frame
	void Update () {
		elapsed += Time.deltaTime * Random.Range(1, 5);
		if (elapsed >= 0.5f)
		{
        	elapsed = elapsed % 0.5f;
			int cube_type = Random.Range(0, 3);
			if (cube_type == 0)
			{
				if (!R_key)
					R_key = GameObject.Instantiate(R_prefab, new Vector3(start.transform.position.x+0.7f, start.transform.position.y, 0) , Quaternion.identity);
			}
			else if (cube_type == 1)
			{
				if (!Y_key)
					Y_key = GameObject.Instantiate(Y_prefab,  new Vector3(start.transform.position.x, start.transform.position.y, 0), Quaternion.identity);
			}
			else if (cube_type == 2)
			{
				if (!B_key)
					B_key = GameObject.Instantiate(B_prefab, new Vector3(start.transform.position.x+1.4f, start.transform.position.y, 0), Quaternion.identity);
			}
		}
	}
}
