using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {
  // set in inspector
  public float speed = 0.1f;
  public GameObject bulletPrefab;
  public Transform bulletSpawnPoint;
    public Image healthBarImage;
  public Shield shield;
  public GameObject expoPrefab;
  public UI ui;
  public AudioClip clipNormalFire;
  public AudioClip clipSuperFire;
  public AudioClip clipHurt;
  public AudioClip clipPowerupReceived;

  // private fields
  private AudioSource audioSrc;
  private float health;
  private const float Y_LIMIT = 4.6f;
  private float originalSpeed;
  private const float X_LIMIT = 8.0f;
  private void Start() {
    health = 1.0f;
    audioSrc = GetComponent<AudioSource>();
    originalSpeed = speed;
    }

  private void Update() {
    healthBarImage.fillAmount = health;

    if (SpaceShooterInput.Instance.input.Fire.WasPressedThisFrame()) {
      GameObject bulletObj = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
      audioSrc.clip = clipNormalFire;
      audioSrc.Play();
    }
    if (SpaceShooterInput.Instance.input.SuperFire.WasPressedThisFrame()) {
      GameObject bulletObj = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
      bulletObj.GetComponent<Bullet>().speed *= 2;
      Instantiate(bulletPrefab, bulletSpawnPoint.position + Vector3.up * 0.5f, Quaternion.identity);
      Instantiate(bulletPrefab, bulletSpawnPoint.position + Vector3.up * -0.5f, Quaternion.identity);
      audioSrc.clip = clipSuperFire;
      audioSrc.Play();
    }

    var vertMove = SpaceShooterInput.Instance.input.MoveVertically.ReadValue<float>();
    this.transform.Translate(Vector3.up * speed * Time.deltaTime * vertMove);

    if (this.transform.position.y > Y_LIMIT) {
      this.transform.position = new Vector3(transform.position.x, Y_LIMIT);
    }
    else if (this.transform.position.y < -Y_LIMIT) {
      this.transform.position = new Vector3(transform.position.x, -Y_LIMIT);
    }

    var horzMove = SpaceShooterInput.Instance.input.MoveHorizontally.ReadValue<float>();
    this.transform.Translate(Vector3.right * speed * Time.deltaTime * horzMove);

    if (this.transform.position.x > X_LIMIT){
      this.transform.position=new Vector3(X_LIMIT,transform.position.y);
    }
    else if (this.transform.position.x < -X_LIMIT){
      this.transform.position = new Vector3(-X_LIMIT,transform.position.y);
      
    }
  }

  public void DamageFromEnemy() {
    if (!shield.IsActive) {
      audioSrc.clip = clipHurt;
      audioSrc.Play();
      health -= 0.25f;

      healthBarImage.fillAmount = health;


      if (health <= 0) {
        var expoObj = Instantiate(expoPrefab, transform.position, Quaternion.identity);
        Destroy(expoObj, expoObj.GetComponent<ParticleSystem>().main.duration);
        Destroy(gameObject);
        ui.ShowGameOver();
      }
    }
  }

  public void RefillShield() {
    audioSrc.clip = clipPowerupReceived;
    audioSrc.Play();
    shield.FullRefill();
  }

    public bool Heal(float amount)
    {
        if (health >= 1.0f){
            // Player is at full health, tell the pickup not to consume itself
            return false;
        }
        health += amount;

        // Clamp health so it doesn't exceed the max of 1.0f
        if (health > 1.0f){
            health = 1.0f;
        }

        // Play the powerup sound when healed!
        if (audioSrc != null && clipPowerupReceived != null){
            audioSrc.clip = clipPowerupReceived;
            audioSrc.Play();
        }
        return true; // Successfully healed!
    }

    public void ActivateSpeedBoost(float multiplier, float duration) {
        StartCoroutine(SpeedBoostRoroutine(multiplier, duration));
    }

    private IEnumerator SpeedBoostRoroutine(float multiplier, float duration) {
        speed *= multiplier;
        yield return new WaitForSeconds(duration);
        speed = originalSpeed; // Reset to original speed after boost duration
    }
}
