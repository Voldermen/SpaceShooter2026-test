using UnityEngine;
using UnityEngine.UI;
public class Shield : MonoBehaviour
{
    // set in inspector
    public float maxProtectionTime;
    public GameObject shieldBubble;
    public bool IsActive { get; private set; }
    public Slider slider;
    // private variables
    private float protectionTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider.value=1.0f;
        protectionTime = maxProtectionTime;
        shieldBubble.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        slider.value=Mathf.Clamp(protectionTime / maxProtectionTime, 0f, 1f);
        if (SpaceShooterInput.Instance.input.Shield.IsPressed())
        {
            if (protectionTime > 0){
            protectionTime -= Time.deltaTime;
            IsActive = true; // shield is active but decreases.
            }
            else
            {
                IsActive=false;
                protectionTime=0; // if you run out of shield.
            }
        }
        else
        {
            protectionTime += Time.deltaTime;
            protectionTime = Mathf.Clamp(protectionTime, 0, maxProtectionTime);
            IsActive=false; // as the shield recharges.
        }
        shieldBubble.SetActive(IsActive); // if shield is active the player sprite will have a change.
    }

    public void FullRefill()
    {
        protectionTime= maxProtectionTime;
    }
}
