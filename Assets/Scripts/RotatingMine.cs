using UnityEngine;

public class RotatingMine : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 360f; // Degrees to rotate per second
    public float lifetime = 5f; // Destroy after 5 seconds to prevent memory leaks

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Give the mine an initial forward velocity based on the direction it's facing
        // In 2D, transform.up is usually "forward" depending on your sprite's orientation. 
        // If it shoots sideways, change this to transform.right.
        rb.linearVelocity = Vector2.left * moveSpeed;

        // Destroy the mine after a few seconds so they don't exist forever
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Rotate the mine around the Z axis (which is the axis facing the camera in 2D)
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Get the Player script and deal damage
            Player playerScript = collision.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.DamageFromEnemy();
            }

            Destroy(gameObject); // Destroy the mine on impact
        }
    }
}