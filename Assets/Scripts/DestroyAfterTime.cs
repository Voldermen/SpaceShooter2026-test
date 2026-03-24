using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    // Adjust this to match how long your animation is!
    public float lifetime = 0.5f;

    void Start()
    {
        // Destroys this object after 'lifetime' seconds
        Destroy(gameObject, lifetime);
    }
}