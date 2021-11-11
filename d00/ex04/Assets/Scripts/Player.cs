using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject PadelLeft;
    public GameObject PadelRight;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {

        if (Input.GetKey(KeyCode.W) && PadelLeft.transform.position.y < 3.5f)
        {
            PadelLeft.transform.Translate(0f, 0.1f, 0f);
        } else if (Input.GetKey(KeyCode.S) && PadelLeft.transform.position.y > -3.5f)
        {
            PadelLeft.transform.Translate(0f, -0.1f, 0f);
        }
        if (Input.GetKey(KeyCode.UpArrow) && PadelRight.transform.position.y < 3.5f)
        {
            PadelRight.transform.Translate(0f, 0.1f, 0f);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && PadelRight.transform.position.y > -3.5f)
        {
            PadelRight.transform.Translate(0f, -0.1f, 0f);
        }

    }
}