using UnityEngine;

public class Powerup : MonoBehaviour
{
    public float speed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D c){
        
        if (c.gameObject.CompareTag("Player")){
            Destroy(gameObject);
            
           c.gameObject.GetComponent<Player>().RefillShield();
        }


    }
}
