using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float spawnInterval = 4.0f;
    public float spawnY = 0.57f;
    public float minX = -5.0f;
    public float maxX = 5.0f;
    public float spawnZ = 30.0f;
    public float moveSpeed = 2.0f;

    public int maxEnemies;
    private int spawnedEnemies;

    private float timer;
    private bool isSpawning = true;

    public GameManager gameManager;

    void Update()
    {
        if (!isSpawning) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval && spawnedEnemies < maxEnemies)
        {
            SpawnEnemy();
            timer = 0;
        }
    }

    void SpawnEnemy()
    {
        float spawnX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);

        GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPosition, Quaternion.Euler(0, 180, 0));
        enemy.GetComponent<MovingEnemy>().moveSpeed = moveSpeed;

        spawnedEnemies++;
        gameManager.OnEnemySpawned();
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }
}
