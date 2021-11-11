using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject[] enemies;
    private GameObject enemy;
    private bool noRepeat;

    private void Start()
    {
        StartCoroutine(SpawnNewEnemy());
    }

    private void Update()
    {
        if (!noRepeat)
            StartCoroutine(SpawnNewEnemy());
    }

    private IEnumerator SpawnNewEnemy()
    {
        noRepeat = true;
        int type = Random.Range(0, 2);
        enemy = (GameObject)Instantiate(enemies[type], transform);
		yield return new WaitForSeconds(30.0f);
        noRepeat = false;
    }
}
