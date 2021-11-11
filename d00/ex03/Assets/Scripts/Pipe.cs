using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
	public float speed;
	public float start;
	public float end;

	public float min;
	public float max;

	private int  score;
	public GameObject bird;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-speed * Time.deltaTime, 0, 0);
		if (transform.position.x < end)
		{
			transform.Translate(start, 0, 0);
			score++;
		}
		if (bird)
		{
			if ((transform.position.x - bird.transform.position.x > -0.8f) && (transform.position.x - bird.transform.position.x < 0.8f))
			{
				if ((bird.transform.position.y > max) || (bird.transform.position.y < min))
					Destroy(bird);
			}
		}
		else
		{
			Debug.Log("Score: "  + score + "\nTime: " + Mathf.RoundToInt(Time.time) + "s");
			GameObject.Destroy(this.gameObject);
		}
    }
}
