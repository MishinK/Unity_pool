using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public GameObject pos_player;
    private Vector3 diff;

    void Start()
    {
        diff = transform.position - pos_player.transform.position;
    }

    void Update()
    {
        transform.position = diff + pos_player.transform.position;
    }
}
