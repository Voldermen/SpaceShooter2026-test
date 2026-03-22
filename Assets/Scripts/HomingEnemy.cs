using UnityEngine;

public class HomingEnemy : MonoBehaviour
{
    // public variables
    public float speed= 10f;
    public float rotationSpeed= 200f;

    // private variables
    private Transform target;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerObj= GameObject.FindGameObjectWithTag("Player");
        target=playerObj.transform;
        
    }

  

    // Update is called once per frame
    void Update()
    {
        if (target == null) // case for if there is no target. They will do nothing if there is not a target.
        {
            return;
        }
        float angleOfRotation= Mathf.Atan2(target.position.y-transform.position.y, target.position.x-transform.position.x) * Mathf.Rad2Deg + 180f;
        var targetOfRotation= Quaternion.Euler(new Vector3(0f,0f,angleOfRotation));
        transform.rotation= Quaternion.RotateTowards(transform.rotation, targetOfRotation, rotationSpeed* Time.deltaTime); // this rotation code was found on stack overflow by user jwadsack. I changed it to fit my sprites. moving left instead of up.

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D c){
        if (c.gameObject.CompareTag("bullet")){
            Destroy(gameObject);
            Destroy(c.gameObject);
            Score.HitEnemey();
        }
        else if (c.gameObject.CompareTag("Player")){
            Destroy(gameObject);
            
            c.gameObject.GetComponent<Player>().DamageFromEnemy(); // gets player script then calls the DamageFromEnemy function.
        }


    }
}
