using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed= 0.09f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){

        // This is cArlos, the code is working fine, but I want to make the bullet move faster. Can you help me with that?   
    }

    // Update is called once per frame
    void Update(){
        this.transform.Translate(Vector3.right * speed * Time.deltaTime);
        
    }
}
// test