using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public GameController gameController;

    float rotationLeft = 360;
    float rotationSpeed = 30;

    void Update()
    {

        float rotation = rotationSpeed * Time.deltaTime;
        if (rotationLeft > rotation)
        {
            rotationLeft -= rotation;
        }
        else
        {
            rotation = rotationLeft;
            rotationLeft = 360;
        }
        transform.Rotate(rotation, rotation, rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "MainCamera")
            gameController.detected = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "MainCamera")
            gameController.detected = false;
    }
}
