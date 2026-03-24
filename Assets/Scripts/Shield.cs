using UnityEngine;
using System;
using UnityEngine.UI; // Required for the Image component
// Removed the [RequireComponent(typeof(Slider))] line!

public class Shield : MonoBehaviour
{
    // set in inspector
    public float maxProtectionTime;
    public GameObject shield;
    public Image shieldBarImage; // Your new pixel bar!

    // private
    private float protectionTime;

    public bool IsActive { get; private set; }

    void Start()
    {
        protectionTime = maxProtectionTime;
        shield.SetActive(false);

        // Ensure the bar starts full
        if (shieldBarImage != null)
        {
            shieldBarImage.fillAmount = 1.0f;
        }
    }

    void Update()
    {
        if (shield == null)
        {
            return;
        }

        // UPDATE THE IMAGE FILL AMOUNT HERE
        if (shieldBarImage != null)
        {
            shieldBarImage.fillAmount = Mathf.Clamp(protectionTime / maxProtectionTime, 0f, 1f);
        }

        if (SpaceShooterInput.Instance.input.Shield.IsPressed())
        {
            if (protectionTime > 0)
            {
                protectionTime -= Time.deltaTime;
                IsActive = true;
            }
            else
            {
                IsActive = false;
                protectionTime = 0;
            }
        }
        else
        {
            protectionTime += Time.deltaTime;
            protectionTime = Mathf.Clamp(protectionTime, 0, maxProtectionTime);
            IsActive = false;
        }

        shield.SetActive(IsActive);
    }

    public void FullRefill()
    {
        protectionTime = maxProtectionTime;
    }
}