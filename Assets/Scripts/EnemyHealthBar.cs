using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public GameObject healthBarPrefab;
    private GameObject healthBar;
    private Image healthBarFill;
    private Transform healthBarTransform;
    private EnemyHealth enemyHealth;
    private Vector3 healthBarOffset = new Vector3(0, 10.0f, 0); // Adjust the Y offset as needed

    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        healthBar = Instantiate(healthBarPrefab, transform.position + healthBarOffset, Quaternion.identity, transform);
        healthBarTransform = healthBar.transform;
        healthBarFill = healthBar.GetComponentInChildren<Image>();
    }

    void Update()
    {
        if (healthBar != null)
        {
            // Update the position of the health bar to stay above the enemy
            healthBarTransform.position = transform.position + healthBarOffset;

            // Update the health bar fill amount based on the enemy's current health
            healthBarFill.fillAmount = (float)enemyHealth.currentHealth / enemyHealth.maxHealth;
        }
    }

    public void DestroyHealthBar()
    {
        if (healthBar != null)
        {
            Destroy(healthBar);
        }
    }
}
