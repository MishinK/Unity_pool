using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBall : MonoBehaviour
{
	public GameObject PadelLeft;
    public GameObject PadelRight;
	public float speed;

	private float PongBallHSpeed;
    private float PongBallVSpeed;
	bool pongBallNeedsReset;
    int scoreLeft;
    int scoreRight;

    void Start()
    {
        int choice = Random.Range(0, 2);
        PongBallVSpeed = choice == 0 ? -speed/10 : speed/10;
        choice = Random.Range(0, 2);
        PongBallHSpeed = choice == 0 ? -speed/10 : speed/10;
		pongBallNeedsReset = false;
		scoreLeft = 0;
		scoreRight = 0;
    }

    void Update()
    {
        if (transform.position.x < -11)
        {
            scoreRight++;
            pongBallNeedsReset = true;
        }
        if (transform.position.x > 11)
        {
            scoreLeft++;
            pongBallNeedsReset = true;
        }
        if (pongBallNeedsReset)
        {
            pongBallNeedsReset = false;
            Debug.Log("Player 1: " + scoreLeft + " | Player 2: " + scoreRight);
            transform.position = new Vector3(0, 0, 0);

            int choice = Random.Range(0, 2);
            PongBallVSpeed = choice == 0 ? -speed/10 : speed/10;
            choice = Random.Range(0, 2);
            PongBallHSpeed = choice == 0 ? -speed/10 : speed/10;
        }
        if (transform.position.y + PongBallVSpeed > 4.5 || transform.position.y + PongBallVSpeed < -4.5)
            PongBallVSpeed = -PongBallVSpeed;

        if ((transform.position.x + PongBallHSpeed < -9 && transform.position.x + PongBallHSpeed > -9.2) || (transform.position.x + PongBallHSpeed > 9 && transform.position.x + PongBallHSpeed < 9.2))
        {
            if ((Mathf.Abs(PadelLeft.transform.position.y - transform.position.y) < 2) || (Mathf.Abs(PadelRight.transform.position.y - transform.position.y) < 2))
				PongBallHSpeed = -PongBallHSpeed;
			PongBallHSpeed *= 1.1f;
        }
        transform.Translate(PongBallHSpeed, PongBallVSpeed, 0f);
    }
}
