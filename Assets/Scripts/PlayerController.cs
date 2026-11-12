using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public int moveSpeed;

    private Rigidbody rb;
    private float xMovement;
    private float yMovement;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent <Rigidbody>();
    }

    // Update is called once per frame
      void OnMove (InputValue movementValue)
   {
    Vector2 movement = movementValue.Get<Vector2>();

    xMovement = movement.x;
    yMovement = movement.y;

   }

       void FixedUpdate() 
   {

    Vector3 movement = new Vector3 (xMovement,0f,yMovement);

    rb.AddForce(movement*moveSpeed);
   }
}
