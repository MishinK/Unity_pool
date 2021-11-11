using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speedM = 2.5f;
    public GameController gameController;

    // Mouse Rotation Control
    public float speedH = 15.0f;
    public float speedV = 15.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    // Walking Sound
    public AudioSource footStep;
    private bool footStepPlay;
    private Coroutine coroutine;
    private bool isMoving;

	private Rigidbody rb;
    private Camera cam;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponent<Camera>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (gameController.cctvDetected)
        {
            gameController.detectionLevel -= 0.05f;
        }
    }

    void Update()
    {
        if (!gameController.gameOver)
        {
            // Mouse
            MouseMovement();

            // Keyboard
            KeyboardMovement();

            // Raycasting
            ObjectDetermination();
        }
    }

    private void MouseMovement()
    {
        float tmp = pitch - (speedV * Input.GetAxis("Mouse Y"));
        if (tmp >= -30.0f && tmp <= 30.0f)
            pitch = tmp;
        yaw += speedH * Input.GetAxis("Mouse X");
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

    private void KeyboardMovement()
    {
        if (Input.GetKey("w"))
            moveCamera(Vector3.forward);
        else if (Input.GetKey("s"))
            moveCamera(Vector3.back);
        else if (Input.GetKey("a"))
            moveCamera(Vector3.left);
        else if (Input.GetKey("d"))
            moveCamera(Vector3.right);
        else if (footStepPlay)
        {
            StopCoroutine(coroutine);
            footStepPlay = false;
            isMoving = false;
            speedM = 2.5f;
            gameController.run = false;
        }

        // Run
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (isMoving)
            {
                speedM = 4.5f;
                gameController.run = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedM = 2.5f;
            gameController.run = false;
        }
    }

    private void ObjectDetermination()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            string hitObject = hit.transform.name;
            float distance = Vector3.Distance(hit.transform.position, transform.position);
            switch (hitObject)
            {
                case "Fan":
                    gameController.objectVisible = 1;
                    break;
                case "KeyCard":
                    gameController.objectVisible = 2;
                    break;
                case "KeyCardReader":
                    gameController.objectVisible = 3;
                    break;
                case "Target":
                    gameController.objectVisible = 4;
                    break;
                default:
                    gameController.objectVisible = -1;
                    break;
            }
            if (distance > 1.7f)
                gameController.objectVisible = -1;
        }
        else
        {
            gameController.objectVisible = -1;
        }
    }

    private IEnumerator PlayFootStep()
    {
        while (true)
        {
            if (!gameController.run)
            {
                footStep.Play();
                yield return new WaitForSeconds(.6f);
            }
            else
            {
                footStep.Play();
                yield return new WaitForSeconds(.3f);
            }
        }

    }

    void moveCamera(Vector3 direction)
    {
        isMoving = true;
        transform.Translate(direction * Time.deltaTime * speedM);
        transform.position = new Vector3(transform.position.x, 1.312f, transform.position.z);
        if (!footStepPlay)
        {
            footStepPlay = true;
            coroutine = StartCoroutine(PlayFootStep());
        }
    }
}
