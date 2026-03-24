using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [Header("Boss Stats")]
    public float baseMaxHp = 6f;

    // private variables
    private float currentHp;
    private Slider bossHealth;
    private Game game;
    private bool secondPhase = false;
    private float maxHp;

    // This is sent over from Game.cs when the boss spawns
    public void gameTransfer(Slider healthBar, Game gameScript, int bossDifficulty)
    {
        bossHealth = healthBar;
        game = gameScript;
        
        maxHp= baseMaxHp + (bossDifficulty * 3f); //increases the max health of the bossby 3 hp every 20000 points.
        currentHp = maxHp;

        if (bossHealth != null)
        {
            bossHealth.maxValue = maxHp;
            bossHealth.value = currentHp;
            bossHealth.gameObject.SetActive(true); // Show health bar
        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        // FIX: Changed "bullet" to "Bullet" to match your other scripts!
        if (c.gameObject.CompareTag("Bullet"))
        {
            damageFromPlayer(1f);
            Destroy(c.gameObject);
        }
    }

    /* NOTE: If your bullets pass right through the boss without hurting it, 
       delete the OnCollisionEnter2D method above and use this one instead:
       
    private void OnTriggerEnter2D(Collider2D c) 
    {
        if (c.gameObject.CompareTag("Bullet"))
        {
            damageFromPlayer(1f);
            Destroy(c.gameObject);
        }
    }
    */

    public void damageFromPlayer(float damaged)
    {
        currentHp -= damaged;
        // Keeps health from going above max or below 0
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);

        // Update the UI Slider
        if (bossHealth != null)
        {
            bossHealth.value = currentHp;
        }

        // Trigger Phase 2 at 50% health
        if (!secondPhase && currentHp <= maxHp / 2f)
        {
            secondPhase = true;
            Boss boss = GetComponent<Boss>();
            if (boss != null)
            {
                boss.beginSecondPhase();
            }
        }

        // Check for death
        if (currentHp <= 0)
        {
            BossDeath();
        }
    }

    private void BossDeath()
    {
        if (bossHealth != null)
        {
            bossHealth.gameObject.SetActive(false); // Hide health bar
        }

        if (game != null)
        {
            game.bossIsDef(); // Tell Game.cs to start normal waves again
        }

        Destroy(gameObject); // Destroy the boss
    }
}