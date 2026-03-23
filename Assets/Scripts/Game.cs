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

    // --- NEW BOSS VARIABLES ---
    [Header("Boss Settings")]
    public GameObject bossPrefab;
    public Slider bossHealthBar;
    public float timeBeforeBoss = 30f;

    private bool bossHasSpawned = false;
    private bool isBossAlive = false;
    private float gameTimer = 0f;

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
        bossHasSpawned = true;
        isBossAlive = true;

        Vector3 bossSpawnPt = new Vector3(spawnRange.bounds.max.x + 2f, 0, 0);
        GameObject bossObj = Instantiate(bossPrefab, bossSpawnPt, Quaternion.identity);

        BossHealth bossHealthScript = bossObj.GetComponent<BossHealth>();
        if (bossHealthScript != null)
        {
            bossHealthScript.gameTransfer(bossHealthBar, this);
        }
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
        if (!bossHasSpawned)
        {
            gameTimer += Time.deltaTime;
            if (gameTimer >= timeBeforeBoss)
            {
                SpawnBoss();
            }
        }

        // check spawn enemy (ONLY if the boss is NOT alive!)
        if (!isBossAlive)
        {
            enemySpawnTimer += Time.deltaTime;
            if (enemySpawnTimer >= enemySpawnDelay)
            {
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