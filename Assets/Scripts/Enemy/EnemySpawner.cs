using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public Vector3 spawnDistance = new Vector3(0, 0, 67f);
    public float spawnInterval = 5f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnEnemiesRoutine());
    }

    private IEnumerator SpawnEnemiesRoutine()
    {
        while (true)
        {
            if (GameManager.Instance.isGameStarted)
            {
                SpawnEnemies();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemies()
    {
        Vector3 spawnPosition = player.position + spawnDistance;
        Vector3 randomOffset = new Vector3(Random.Range(-3.5f, 3.5f), 0, 0);
        Instantiate(enemyPrefab, spawnPosition + randomOffset, Quaternion.identity);
    }
}
