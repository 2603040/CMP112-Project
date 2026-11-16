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

    //I dont use camera.transform.forward as that cares about the y rotation which I dont want it to.
    Vector3 cameraForward = new Vector3 (-cameraRight.z,0.0f,cameraRight.x);


    //multiplys the movement vectors components with the coresponding camera direction to align them
    xMovement = movement.x * -cameraForward;
    yMovement = movement.y * cameraRight;
   }

       void FixedUpdate() 
   {

    Vector3 movementVect = xMovement + yMovement;
    rb.AddTorque(movementVect*moveSpeed);
    
   }
}
