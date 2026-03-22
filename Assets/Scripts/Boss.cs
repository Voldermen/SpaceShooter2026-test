using UnityEngine;
using System.Collections;


public class Boss : MonoBehaviour
{
    // public variables
    public float speed= 3f; // bosses travel speed.
    public float xPosition=7f; // this is the x position where the boss should stop at.
    public float UpAndDown= 2f; // how far the boss can move up or down.
    public GameObject bossBulletPrefab;
    public Transform bbSpawn; // boss bullet spawn point.
    public float timeBetweenShots=5f; // how long in between each time the boss shoots.
    public GameObject phaseTwoBulletsPrefab;
    public float rateOfFire=5f;
    
    // private variables
    private float startingY; // this is the initial y position of when the boss hits the intial x position (Like a center point for y).
    private bool stoppingPoint= false; // checks if the boss has made it to the area of the screen I want it to stay within.
    private Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool isPhaseTwo=false;
    private float timer=0f;
    void Start()
    {
       startingY= transform.position.y; // This will save the initial y position.

       GameObject playerObj= GameObject.FindGameObjectWithTag("Player"); // gets the player object.
       
       player=playerObj.transform;// this points to the player objects transform (basically coordinates).
       

        StartCoroutine(shooting()); // this is a coroutine it is a method that will allow me to make the boss not spam bullets. if it was in the update method it would spawn a homing bullet every frame. https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Coroutine.html
    }

    // Update is called once per frame
    void Update()
    {
        if (!stoppingPoint)
        {
            Vector3 targetPosition= new Vector3(xPosition,startingY,0); // this is the position that I want the boss to go to.
            
            transform.position=Vector3.MoveTowards(transform.position,targetPosition, speed*Time.deltaTime); //how fast the boss should move to it's position.

            if (transform.position.x >= xPosition) // once the boss has reached the stopping point it will stop the boss from traveling anymore on the x axis.
            {
                stoppingPoint=true;
            }
        }
        else
        {
            // once the boss is in position it will move up and down on a sine wave using Mathf.Sin to go from -1 and 1.
            // Time.time is used to make the boss move continuously over time.
            float yMovement =startingY + Mathf.Sin(Time.time * speed) * UpAndDown;
            
            transform.position = new Vector3(xPosition, yMovement,0); // makes it to where the boss will only change y position.
        }
        if (isPhaseTwo)
        {
            timer += Time.deltaTime;
            if (timer >= rateOfFire) // Like how the enemy spawns work have it wait a bit before firing.
            {
                Instantiate(phaseTwoBulletsPrefab, bbSpawn.position, Quaternion.identity);
                timer=0f;
            }
        }
    }

    public void beginSecondPhase() // second phase flag.
    {
        isPhaseTwo=true;
    }

private IEnumerator shooting() // This coroutine also helps with me doing the second phase because it is running along side it.
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenShots);

            if (stoppingPoint)
            {
                GameObject bossBullet = Instantiate(bossBulletPrefab, bbSpawn.position, Quaternion.identity);

                BossBullet homing = bossBullet.GetComponent<BossBullet>();
                homing.SetTarget(player); // has the  player's transform. SetTarget takes this in as the arg.
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D c) // if the player makes contact with the boss the player will take damage.
    {
        if (c.gameObject.CompareTag("Player"))
        {
            c.gameObject.GetComponent<Player>().DamageFromEnemy();
        }
    }
}

