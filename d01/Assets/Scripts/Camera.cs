using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camera : MonoBehaviour
{
    private int which_player;
    [SerializeField] GameObject obj_Claire;
    [SerializeField] float Claire_speed;
    [SerializeField] GameObject obj_John;
    [SerializeField] float John_speed;
    [SerializeField] GameObject obj_Thomas;
    [SerializeField] float Thomas_speed;

    // Start is called before the first frame update
    void Start()
    {
        which_player = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        obj_Claire.GetComponent<playerScript_ex00>().moveable = false;
        obj_John.GetComponent<playerScript_ex00>().moveable = false;
        obj_Thomas.GetComponent<playerScript_ex00>().moveable = false;
        if (Input.GetKeyDown("1"))
            which_player = 1;
        if (Input.GetKeyDown("2"))
            which_player = 2;
        if (Input.GetKeyDown("3"))
            which_player = 3;
        if (which_player == 1)
        {
			obj_John.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			obj_Thomas.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

			obj_Claire.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
			obj_Claire.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
	
            this.transform.position = new Vector3(obj_Claire.transform.position.x, obj_Claire.transform.position.y, -10);
            obj_Claire.GetComponent<playerScript_ex00>().speed = Claire_speed;
            obj_Claire.GetComponent<playerScript_ex00>().moveable = true;
            obj_Claire.GetComponent<playerScript_ex00>().center = 0.4f;

        }
        if (which_player == 2)
        {
			obj_Thomas.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			obj_Claire.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

			obj_John.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
			obj_John.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
	
            this.transform.position = new Vector3(obj_John.transform.position.x, obj_John.transform.position.y, -10);
            obj_John.GetComponent<playerScript_ex00>().speed = John_speed;
            obj_John.GetComponent<playerScript_ex00>().moveable = true;
            obj_John.GetComponent<playerScript_ex00>().center = 0.4f;

        }
        if (which_player == 3)
        {
			obj_John.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			obj_Claire.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			obj_Thomas.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
			obj_Thomas.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

            this.transform.position = new Vector3(obj_Thomas.transform.position.x, obj_Thomas.transform.position.y, -10);
            obj_Thomas.GetComponent<playerScript_ex00>().speed = Thomas_speed;
            obj_Thomas.GetComponent<playerScript_ex00>().moveable = true;
            obj_Thomas.GetComponent<playerScript_ex00>().center = 0.4f;
        }
    }
}
