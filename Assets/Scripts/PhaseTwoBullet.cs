using UnityEngine;

public class PhaseTwoBullet : MonoBehaviour
{
    public float speed= 7f;
    

    // Update is called once per frame
    void Update()
    {
       this.transform.Translate(Vector3.left * speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D c)
    {
        // 1. Check if the bullet hits the shield forcefield first
        if (c.gameObject.CompareTag("Shield"))
        {
            Destroy(gameObject); // Bullet safely absorbs into the shield
        }
        // 2. Otherwise, if it hits the player's hull
        else if (c.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject); // Destroy the bullet
            c.gameObject.GetComponent<Player>().DamageFromEnemy(); // Hurt the player
        }
    }
}