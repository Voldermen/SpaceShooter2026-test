using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    // set in inspector
    public float enemySpawnDelay;
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    public GameObject healthPickupPrefab;
    public GameObject speedPowerupPrefab;
    public BoxCollider2D spawnRange;
    public UI ui;
    public GameObject hEnemyPrefab;

    // --- NEW BOSS VARIABLES ---
    [Header("Boss Settings")]
    public GameObject bossPrefab;
    public Slider bossHealthBar;
    private float bossScore = 10000f; 

    private bool isBossAlive = false;
    

    // private fields
    private float powerUpDelay;
    private float enemySpawnTimer;
    private float powerupSpawnTimer;

    private void Start()
    {
        powerUpDelay = Random.Range(5f, 10f);
        powerupSpawnTimer = 0;

        if (bossHealthBar != null)
        {
            bossHealthBar.gameObject.SetActive(false);
        }
    }

    private void SpawnEnemy()
    {
        Vector3 enemySpawnPt = new Vector3(
            Random.Range(spawnRange.bounds.min.x, spawnRange.bounds.max.x),
            Random.Range(spawnRange.bounds.min.y, spawnRange.bounds.max.y),
            0);
        Instantiate(enemyPrefab, enemySpawnPt, Quaternion.identity);
    }
    private void SpawnHEnemy()
    {
        Vector3 enemySpawnPt= new Vector3 ( Random.Range(spawnRange.bounds.min.x,spawnRange.bounds.max.x),
        Random.Range(spawnRange.bounds.min.y, spawnRange.bounds.max.y),0);
        Instantiate(hEnemyPrefab, enemySpawnPt, Quaternion.identity);
    }

    private void SpawnPowerup()
    {
        Vector3 powerupSpawnPt = new Vector3(
            Random.Range(spawnRange.bounds.min.x, spawnRange.bounds.max.x),
            Random.Range(spawnRange.bounds.min.y, spawnRange.bounds.max.y),
            0);

        float dropChance = Random.Range(0f, 100f);

        if (dropChance < 20f)
        {
            Instantiate(healthPickupPrefab, powerupSpawnPt, Quaternion.identity);
        }
        else if (dropChance >= 20f && dropChance < 40f)
        {
            Instantiate(speedPowerupPrefab, powerupSpawnPt, Quaternion.identity);
        }
        else
        {
            Instantiate(powerupPrefab, powerupSpawnPt, Quaternion.identity);
        }
    }

    // --- SPAWN BOSS METHOD ---
    private void SpawnBoss()
    {
        isBossAlive = true;

        Vector3 bossSpawnPt = new Vector3(spawnRange.bounds.max.x + 2f, 0, 0);
        GameObject bossObj = Instantiate(bossPrefab, bossSpawnPt, Quaternion.identity);
        Boss boss = bossObj.GetComponent<Boss>();
        int bossDifficulty= Mathf.FloorToInt(Score.score / 20000f); // every 20000 points the bosses bullets move faster and it has more health.

        BossHealth bossHealthScript = bossObj.GetComponent<BossHealth>();
        if (bossHealthScript != null)
        {
            bossHealthScript.gameTransfer(bossHealthBar, this, bossDifficulty); // the boss health bar, a current instance of the Game script, and the bosses difficulty level are passed to the boss health script.
        }

        boss.difficultyLevel(bossDifficulty); // the fire rate is altered every time the player hits every 20000.
    }

    // --- REQUIRED BY BOSSHEALTH.CS ---
    public void bossIsDef()
    {
        isBossAlive = false;
    }

    void Update()
    {
        if (!ui.IsReady)
        {
            return;
        }

        // --- BOSS TIMER ---
        if (!isBossAlive && Score.score >= bossScore)
        {
            SpawnBoss();
            bossScore += 10000; // A boss spawns every 10000 points.
        }

        // check spawn enemy (ONLY if the boss is NOT alive!)
        if (!isBossAlive)
        {
            enemySpawnTimer += Time.deltaTime;
            if (enemySpawnTimer >= enemySpawnDelay)
            {
                SpawnHEnemy();
                SpawnEnemy();
                enemySpawnTimer = 0.0f;
            }
        }

        // check spawn powerup
        powerupSpawnTimer += Time.deltaTime;
        if (powerupSpawnTimer >= powerUpDelay)
        {
            SpawnPowerup();
            powerUpDelay = Random.Range(5, 10);
            powerupSpawnTimer = 0.0f;
        }
    }
}