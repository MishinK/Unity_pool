using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerScript_ex00 : MonoBehaviour {

    public bool moveable;
    public float speed;
    public float center;
    [SerializeField] float up_velocity;
   
	private Rigidbody2D rb2D;
	private bool isGrounded;
	private bool hasJumped;


	public GameObject   end;
    // Start is called before the first frame update
    void Start()
    {
        moveable = false;
		rb2D = gameObject.GetComponent<Rigidbody2D> ();
		isGrounded = true;
		hasJumped = false;
    }


    // Update is called once per frame
    void Update()
    {
		if (moveable)
		{
			if (hasJumped) {
				rb2D.AddForce(new Vector2(0, 4.0f), ForceMode2D.Impulse);
				isGrounded = false;
				hasJumped = false;
			}
        	if (Input.GetKeyDown("space") && isGrounded)
			{
           		transform.Translate(Vector3.up * up_velocity);
				hasJumped = true;
			}
        	if (Input.GetKey("left"))
            	transform.Translate(Vector3.left * speed);
        	if (Input.GetKey("right"))
            	transform.Translate(Vector3.right * speed);
			
			if(!isGrounded && rb2D.velocity.y == 0) {
				isGrounded = true;
			}

			if (transform.position.y <= end.transform.position.y - 1f)
			{
				Debug.Log("Game over!");
				SceneManager.LoadScene("ex05");
			}
		}
    }
}