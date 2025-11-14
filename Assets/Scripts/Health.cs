using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100.0f;
    private float currentHealth;
    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        gameManager = FindFirstObjectByType<GameManager>();

    }

    // Public method to apply damage to the object
    public void ApplyDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    // Method to handle the object's death
    void Death()
    {
        if (gameManager != null)
        {
            //tell game manager a death has occured
            gameManager.EnemyDefeated(transform.position);
        }
        Destroy(gameObject);
    }

}
