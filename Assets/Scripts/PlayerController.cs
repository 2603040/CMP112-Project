using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public int moveSpeed;
    public GameObject camera;

    private Rigidbody rb;
    private Vector3 xMovement;
    private Vector3 yMovement;
    private Vector2 movement;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent <Rigidbody>();

        //makes the cursor invisible and locks it in the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
      void OnMove (InputValue movementValue)
   {
    movement = movementValue.Get<Vector2>();
   }

   void Update(){
    //needs to be in update to get accurate camera location
    Vector3 cameraRight = camera.transform.right;
    Vector3 cameraForward = camera.transform.forward;

    //multiplys the movement vectors components with the coresponding camera direction to align them
    xMovement = movement.x * cameraRight;
    yMovement = movement.y * cameraForward;
   }

       void FixedUpdate() 
   {

    Vector3 movementVect = xMovement + yMovement;
    rb.AddForce(movementVect*moveSpeed);
    
   }
}
