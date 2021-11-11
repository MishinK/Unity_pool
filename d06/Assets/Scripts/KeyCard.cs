using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCard : MonoBehaviour
{
	private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
		if (timer > 0.03f)
		{
			timer %= 0.03f;
			transform.Rotate(0, 10f, 0);
		}
    }
}
