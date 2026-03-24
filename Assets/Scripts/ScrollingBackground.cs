using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 2f;

    [Tooltip("Check this box ONLY on Background_2")]
    public bool isSecondBackground = false;

    private float width;

    void Start()
    {
        // Get the exact width of your background image
        width = GetComponent<SpriteRenderer>().bounds.size.x;

        // Automatically snaps the images perfectly into place when the game starts!
        if (isSecondBackground)
        {
            transform.position = new Vector3(width, transform.position.y, 0);
        }
        else
        {
            transform.position = new Vector3(0, transform.position.y, 0);
        }
    }

    void Update()
    {
        // Move the background left over time
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // When the image goes completely off the left side of the screen...
        if (transform.position.x <= -width)
        {
            // ...teleport it seamlessly to the right side to keep the loop going!
            transform.position = new Vector3(transform.position.x + (width * 2f), transform.position.y, 0);
        }
    }
}