using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    [HideInInspector]
    public int currentHealth;
    private Animator animator;
    private bool isDead = false;
    private MovingEnemy movingEnemy;
    private Collider enemyCollider;
    private EnemyHealthBar enemyHealthBar;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        movingEnemy = GetComponent<MovingEnemy>();
        enemyCollider = GetComponent<Collider>();
        enemyHealthBar = GetComponent<EnemyHealthBar>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        movingEnemy.moveSpeed = 0; // Stop the enemy's movement
        animator.SetBool("Death", true);
        enemyCollider.enabled = false; // Disable the collider
        enemyHealthBar.DestroyHealthBar(); // Destroy the health bar
        StartCoroutine(DestroyAfterAnimation());
    }

    private IEnumerator DestroyAfterAnimation()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
