using UnityEngine;

public class SpaceShooterInput : MonoBehaviour
{
    public SpaceShooterInputActions.StandardActions input;
    public static SpaceShooterInput Instance { get; private set; }

    private void Awake()
    {
        Instance=this;
        var inputActions = new SpaceShooterInputActions();
        inputActions.Enable();
        input= inputActions.Standard;
        input.Enable();
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
