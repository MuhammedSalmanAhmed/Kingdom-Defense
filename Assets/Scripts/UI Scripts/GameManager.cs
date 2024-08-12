using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int enemiesToDefeat;
    public int maxEnemiesPerLevel;
    public int currentLevel;

    private int enemiesDefeated;
    private int enemiesSpawned;
    private bool isAdvancing;

    public EnemySpawn enemySpawn;

    void Start()
    {
        enemiesDefeated = 0;
        enemiesSpawned = 0;
        isAdvancing = false;

        if (enemySpawn != null)
        {
            enemySpawn.maxEnemies = maxEnemiesPerLevel;
        }
    }

    public void OnEnemyDefeated(GameObject enemy)
    {
        enemiesDefeated++;
        Debug.Log($"Enemy defeated: {enemiesDefeated}/{enemiesToDefeat}");

        // Stop spawning new enemies if the required number is reached
        if (enemiesDefeated >= enemiesToDefeat && !isAdvancing)
        {
            isAdvancing = true;
            Debug.Log("All required enemies defeated, waiting for last enemy's death animation...");
            StartCoroutine(WaitForLastEnemyDeath(enemy));
        }
    }

    private IEnumerator WaitForLastEnemyDeath(GameObject enemy)
    {
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            // Wait for the enemy to finish its death animation and be destroyed
            yield return new WaitUntil(() => enemyHealth.IsDestroyed);
        }

        Debug.Log("Last enemy's death animation complete, advancing to the next level...");
        AdvanceToNextLevel();
    }

    void AdvanceToNextLevel()
    {
        currentLevel++;
        if (currentLevel >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("Game Over! You've completed all levels.");
            // Implement logic for game completion here
            Application.Quit();
        }
        else
        {
            Debug.Log($"Advancing to Level {currentLevel}");
            SceneManager.LoadScene(currentLevel);
        }
    }

    public void OnEnemySpawned()
    {
        enemiesSpawned++;
        Debug.Log($"Enemy spawned: {enemiesSpawned}/{maxEnemiesPerLevel}");
        if (enemiesSpawned >= maxEnemiesPerLevel)
        {
            enemySpawn.StopSpawning();
        }
    }
}
