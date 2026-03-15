using UnityEngine;

public class Player : MonoBehaviour{
 //set in inspector
   public float speed = 5.1f;
   public GameObject bulletPrefab;
   public Transform bulletSpawnPoint;

   private SpaceShooterInputActions inputActions;


   private const float Y_LIMIT = 4.6f;
   private const float X_LIMIT = 8.0f; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start(){
    
        inputActions= new();
        inputActions.Enable();
        inputActions.Standard.Enable();
    }

    // Update is called once per frame
    private void Update(){
       if  (inputActions.Standard.Fire.WasPressedThisFrame()){ 
            // If the fire button is pressed, instantiate a bullet at the spawn point
           GameObject bulletObj = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
       }
       if (inputActions.Standard.MoveUp.IsPressed()){ 
            // If up is pressed, move the player up
           this.transform.Translate(Vector3.up * speed * Time.deltaTime);
         
       } 
       else if (inputActions.Standard.MoveDown.IsPressed()){ 
            // If down is pressed, move the player down
           this.transform.Translate(Vector3.down * speed * Time.deltaTime);
       } 
       if (this.transform.position.y > (Y_LIMIT - 0.3f)){ 
            // If the player goes above the Y limit, set its position to the Y limit
           this.transform.position = new Vector3(transform.position.x, (Y_LIMIT - 0.3f));
       }
       else if (this.transform.position.y < (-Y_LIMIT - 0.1f)){ 
            // If the player goes below the Y limit, set its position to the negative Y limit
           this.transform.position = new Vector3(transform.position.x, (-Y_LIMIT - 0.1f));
       }

       if (inputActions.Standard.MoveRight.IsPressed()){ 
            // If right is pressed, move the player right
           this.transform.Translate(Vector3.right * speed * Time.deltaTime);
       }
       else if (inputActions.Standard.MoveLeft.IsPressed()){ 
            // If left is pressed, move the player left
           this.transform.Translate(Vector3.left * speed * Time.deltaTime);
       }

       if (this.transform.position.x > (X_LIMIT - 0.1f)){ 
            // If the player goes above the X limit, set its position to the X limit
           this.transform.position = new Vector3((X_LIMIT - 0.1f), transform.position.y);
       }

       else if (this.transform.position.x < (-X_LIMIT - 0.6f)){ 
            // If the player goes below the X limit, set its position to the negative X limit.                                            
            // I added 0.8f to account for the size of the player sprite, so it doesn't go off screen completely.
           this.transform.position = new Vector3((-X_LIMIT - 0.6f) , transform.position.y);
       }

    }
}

// test