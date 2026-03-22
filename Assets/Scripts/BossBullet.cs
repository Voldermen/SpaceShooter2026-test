using UnityEngine;

public class BossBullet : MonoBehaviour
{
   public float speed= 11f;
    public float rotationSpeed= 200f;

    private Transform target;

    public void SetTarget(Transform currentTar) // the bullet references the players transform to track them.
    {
        target=currentTar;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (target == null) // if there is no target then destroy the bullets.
        {
            Destroy(gameObject);
            return;
        }
        
        float angleOfRotation= Mathf.Atan2(target.position.y-transform.position.y, target.position.x-transform.position.x) * Mathf.Rad2Deg + 180f;
        var targetOfRotation= Quaternion.Euler(new Vector3(0f,0f,angleOfRotation));
        transform.rotation= Quaternion.RotateTowards(transform.rotation, targetOfRotation, rotationSpeed* Time.deltaTime); // this rotation code was found on stack overflow by user jwadsack. I changed it to fit my sprites. moving left instead of up.

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime); // moves towards the player. transform.position is where the bullet is. target.position is where the player is.
            
        
    }

    private void OnCollisionEnter2D(Collision2D c) // the bosses bullets can damage the player by 0.25f by calling the damage player method.
    {
        if (c.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            c.gameObject.GetComponent<Player>().DamageFromEnemy();
        }
        else if (c.gameObject.CompareTag("bullet")){ // player can shoot down the bosses bullets.
            Destroy(gameObject);
            Destroy(c.gameObject);
    }

    }
}