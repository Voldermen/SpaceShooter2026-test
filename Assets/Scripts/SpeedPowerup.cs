using UnityEngine;

public class SpeedPowerup : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 3f;
    public float speedMultiplier = 2.0f;
    public float boostDuration = 5.0f;

    void Update()
    {
        // Move left across the screen
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player playerScript = collision.GetComponent<Player>();

            if (playerScript != null)
            {
                // Give the player the boost and destroy the pickup
                playerScript.ActivateSpeedBoost(speedMultiplier, boostDuration);
                Destroy(gameObject);
            }
        }
    }
}