using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private Animator animator;
    private bool isDead = false;
    private GameManager gameManager;

    private Collider enemyCollider;

    // Reference to the health bar's foreground image
    public UnityEngine.UI.Image healthBarForeground;

    public bool IsDestroyed { get; private set; } = false;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        enemyCollider = GetComponent<Collider>(); // Get the enemy's collider
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        // Update the health bar
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        healthBarForeground.fillAmount = healthPercentage;
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;
        animator.SetBool("Death", true);
        enemyCollider.enabled = false; // Disable the collider to allow the ball to pass through
        GetComponent<MovingEnemy>().moveSpeed = 0;

        if (gameManager != null)
        {
            gameManager.OnEnemyDefeated(gameObject);
        }

        StartCoroutine(DestroyAfterAnimation());
    }

    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(3f);
        IsDestroyed = true;
        Destroy(gameObject);
    }
}
