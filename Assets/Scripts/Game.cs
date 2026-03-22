using UnityEngine;
using UnityEngine.UI;
public class Game : MonoBehaviour
{// set in inspector.
    public BoxCollider2D SpawnRange;
    public GameObject BossPrefab;
    public float enemySpawnDelay;
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    public Slider bossHealthBar;
    public GameObject hEnemyPrefab;
    
// privatec fields
    private float enemySpawnTimer;
    private float powerupSpawnTimer;
    private float powerUpDelay;
    private bool bossSpawnIn= false; // checks for boss this is used to make sure we dont get duplicate bosses spawning and enemies spawning while a boss is on screen.
    private float bossScore=10000; // starting limit for the boss spawn score count.
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        powerUpDelay = Random.Range(5f,10f);
        powerupSpawnTimer=0;
        if(bossHealthBar != null){ // starts game without boss bar on screen.
        bossHealthBar.gameObject.SetActive(false);
        }
    }
        private void SpawnEnemy()
    {
        Vector3 enemySpawnPt= new Vector3 ( Random.Range(SpawnRange.bounds.min.x,SpawnRange.bounds.max.x),
        Random.Range(SpawnRange.bounds.min.y, SpawnRange.bounds.max.y),0);
        Instantiate(enemyPrefab, enemySpawnPt, Quaternion.identity);
    }

        private void SpawnHEnemy()
    {
        Vector3 enemySpawnPt= new Vector3 ( Random.Range(SpawnRange.bounds.min.x,SpawnRange.bounds.max.x),
        Random.Range(SpawnRange.bounds.min.y, SpawnRange.bounds.max.y),0);
        Instantiate(hEnemyPrefab, enemySpawnPt, Quaternion.identity);
    }

    


    private void SpawnPowerup()
    {
        Vector3 powerupSpawnPt= new Vector3 ( Random.Range(SpawnRange.bounds.min.x,SpawnRange.bounds.max.x),
        Random.Range(SpawnRange.bounds.min.y, SpawnRange.bounds.max.y),0);
        Instantiate(powerupPrefab, powerupSpawnPt, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        //check spawn enemy and if there is a boss do not spawn enemies.
        if (!bossSpawnIn){
       enemySpawnTimer += Time.deltaTime;
       if (enemySpawnTimer >= enemySpawnDelay)
        {
            SpawnHEnemy();
            SpawnHEnemy();
            SpawnEnemy();
            SpawnEnemy();
            SpawnEnemy();
            enemySpawnTimer=0.0f;
        } 
        }
        // check spawn powerup
        powerupSpawnTimer += Time.deltaTime;
        if (powerupSpawnTimer >= powerUpDelay)
        {
            SpawnPowerup();
            powerUpDelay=Random.Range(5,10);
            powerupSpawnTimer=0.0f;
        }

        if (!bossSpawnIn && Score.score >= bossScore) // if there is no boss and the score is greater or equal to the next iteration of the boss score then a boss will spawn.
        {
            SpawnBoss();
            bossScore += 10000; // increases the score cap for the next boss.
        }

        


    }

       private void SpawnBoss()
    {
        
        bossSpawnIn=true; // A boolean check to see if there is currently a boss. The game starts with it being false.
        Vector3 bossSpawnPt= new Vector3(13f,0f,0f); // This spawns the boss in at a set point off of the screen.
        GameObject bossObj= Instantiate(BossPrefab,bossSpawnPt, Quaternion.identity);
        BossHealth bossHealth= bossObj.GetComponent<BossHealth>();
        if(bossHealth != null)
        {
            bossHealth.gameTransfer(bossHealthBar,this); // sends the health bar over to the bossHealth script and a current instance of the Game script.
        }
    }

    public void bossIsDef() // this method is for when a boss is defeated. it will let enemies spawn and remove the health bar upon defeat.
    {
        bossSpawnIn=false;
        if(bossHealthBar != null)
        {
            bossHealthBar.gameObject.SetActive(false);
        }
    }
}
