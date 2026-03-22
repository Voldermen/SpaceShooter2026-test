using UnityEngine;
using UnityEngine.UI;
public class BossHealth : MonoBehaviour
{
    // public variables
    public float maxHp=6f;
    

    //private variables
    private float currentHp;
    private Slider bossHealth;
    private Game game;

    private bool secondPhase= false;

    public void gameTransfer(Slider healthBar, Game gameSript) // this is the current instance of the game script and the boss health bar from Game.cs. It is sent over when a boss spawns.
    {
        bossHealth=healthBar;
        game=gameSript;
        currentHp=maxHp;
        if(bossHealth != null) // if there is a health bar then:
        {
            bossHealth.maxValue=maxHp;
            bossHealth.value=currentHp;
            bossHealth.gameObject.SetActive(true); // causes the health bar to show up in the scene.
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

     private void OnCollisionEnter2D(Collision2D c) // boss health when player shoots it.
    {
        if (c.gameObject.CompareTag("bullet"))
        {
            damageFromPlayer(1f);
            Destroy(c.gameObject);
        }
    }

    private void BossDeath()
    {
        if (bossHealth != null)
        {
            bossHealth.gameObject.SetActive(false); // removes the boss health bar when it dies.
        }
        if (game != null) // null ref defesive check.
        {
            game.bossIsDef(); // lets enemies spawn and removes health bar from screen.
        }
        Destroy(gameObject); // removes the boss.
    }

    public void damageFromPlayer(float damaged)
    {
        currentHp -= damaged;
        currentHp= Mathf.Clamp(currentHp,0,maxHp); // keeps the bosses health from going above or below it's intended health pool.
     
        if (bossHealth != null)
        {
            bossHealth.value=currentHp; // updates the bosses health bar when hit.
        }
        if (!secondPhase && currentHp <= maxHp / 2f) // boss enters second phase.
        {
            secondPhase=true;
            Boss boss= GetComponent<Boss>();
            boss.beginSecondPhase();
        }

        if (currentHp <= 0)
        {
            BossDeath();
        }
    }
}
