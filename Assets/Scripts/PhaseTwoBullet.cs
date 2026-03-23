using UnityEngine;

public class PhaseTwoBullet : MonoBehaviour
{
    public float speed= 7f;
    

    // Update is called once per frame
    void Update()
    {
       this.transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    
    private void OnCollisionEnter2D(Collision2D c) // the bosses bullets can damage the player by 0.25f by calling the damage player method.
    {
        if (c.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            c.gameObject.GetComponent<Player>().DamageFromEnemy();
        }
}
}