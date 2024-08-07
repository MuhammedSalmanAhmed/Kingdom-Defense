using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of enemy prefabs to spawn
    public float spawnInterval = 4.0f; // Time interval between spawns
    public float spawnY = 0.57f; //Y position for spawning
    public float minX = -5.0f;  //Minimum X position for spawning
    public float maxX = 5.0f;  //Maximum X positon for spawning
    public float spawnZ = 30.0f; // Z position for spawning (front of the screen)
    public float moveSpeed = 2.0f; // Speed at which enemies move

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0;
        }
    }

    void SpawnEnemy()
    {
        float spawnX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);

        // Instantiate enemy and rotate it to face the opposite direction
        GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPosition, Quaternion.Euler(0, 180, 0));

        // Set the move speed
        enemy.GetComponent<MovingEnemy>().moveSpeed = moveSpeed;
    }
}
