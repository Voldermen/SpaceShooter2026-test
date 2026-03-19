using UnityEngine;

public class Game : MonoBehaviour
{// set in inspector.
    public BoxCollider2D SpawnRange;
    public float enemySpawnDelay;
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
// privatec fields
    private float enemySpawnTimer;
    private float powerupSpawnTimer;
    private float powerUpDelay;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        powerUpDelay = Random.Range(5f,10f);
        powerupSpawnTimer=0;
    }
        private void SpawnEnemy()
    {
        Vector3 enemySpawnPt= new Vector3 ( Random.Range(SpawnRange.bounds.min.x,SpawnRange.bounds.max.x),
        Random.Range(SpawnRange.bounds.min.y, SpawnRange.bounds.max.y),0);
        Instantiate(enemyPrefab, enemySpawnPt, Quaternion.identity);
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
        //check spawn enemy
       enemySpawnTimer += Time.deltaTime;
       if (enemySpawnTimer >= enemySpawnDelay)
        {
            SpawnEnemy();
            enemySpawnTimer=0.0f;
        } 
        // check spawn powerup
        powerupSpawnTimer += Time.deltaTime;
        if (powerupSpawnTimer >= powerUpDelay)
        {
            SpawnPowerup();
            powerUpDelay=Random.Range(5,10);
            powerupSpawnTimer=0.0f;
        }
    }
}
