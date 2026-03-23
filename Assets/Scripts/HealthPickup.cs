using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 3f;
    public float healAmount = 0.25f; // Adjust based on your health system

    void Update()
    {
        // Moves the health pickup to the left over time
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    // Using OnTriggerEnter2D is usually best for powerups so they don't bounce off the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player playerScript = collision.GetComponent<Player>();

            if (playerScript != null)
            {
                // Attempt to heal the player. 
                // We expect Heal() to return true if successful, and false if at max health.
                bool wasConsumed = playerScript.Heal(healAmount);

                if (wasConsumed)
                {
                    // Optional: Add a sound effect or particle instantiation here
                    Destroy(gameObject);
                }
                // If wasConsumed is false (player has full health), the pickup just passes through them.
            }
        }
    }
}