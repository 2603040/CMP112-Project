using UnityEngine;
using UnityEngine.InputSystem;
using TMPro; 

public class PlayerController : MonoBehaviour
{

    public TextMeshProUGUI coincountText;
    public GameObject camera;
    public int moveSpeed;
    public int jumpPower = 100;

    private Rigidbody rb;

    private Vector3 xMovement;
    private Vector3 yMovement;
    private Vector2 movement;

    private bool grounded;
    private Vector3 previousCheckpoint;
    private Vector3 cameraRight;
    private Vector3 cameraForward;

    private int coinCount = 0; 
    




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent <Rigidbody>();
        previousCheckpoint = transform.position;

        //makes the cursor invisible and locks it in the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SetCoinCountText();
    }

    void SetCoinCountText() 
    { 
    coincountText.text = "Count: " + coinCount.ToString(); 
    }

    void OnMove (InputValue movementValue)
   {
        movement = movementValue.Get<Vector2>();
   }

   void OnJump (){
    

        RaycastHit hitInfo;
        Ray ray = new Ray(transform.position, -Vector3.up);

        //Do the raycast. Store the information in hitInfo
        bool rayHit = Physics.Raycast(ray, out hitInfo, 1);

        if (rayHit){
            //makes it so you cant jump if the floor is too steep
            //the o.7 makes it stop working just after 45degrees
            grounded = Vector3.Dot(Vector3.up, hitInfo.normal) > 0.7f;


            if (grounded){
                //makes player jump away from whatever they are jumping off of
                rb.AddForce(hitInfo.normal*jumpPower);
            }
        }
   }

   void OnReset(){
        //resets the players location to that of the previous check point and resets all velocity to zero
        transform.position = previousCheckpoint;
        rb.linearVelocity = new Vector3 (0,0,0);
        rb.angularVelocity = new Vector3 (0,0,0);

   }
   
   void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Checkpoint")){

            previousCheckpoint = other.gameObject.transform.position;

        }
        if (other.gameObject.CompareTag("Death")){
            OnReset();
        }
        if (other.gameObject.CompareTag("Coin"))
        {

            other.gameObject.SetActive(false);
            coinCount = coinCount + 1;
            SetCoinCountText();

        }
     }

     void OnTriggerStay(Collider other){
        if (other.gameObject.CompareTag("Booster")){
            rb.AddForce(-other.transform.forward*other.gameObject.GetComponent<Booster>().boostPower);

        }
     }


    void OnLook(InputValue lookvalue){
        //the player input component only seems to be able to affect one object at a time 
        //so I am sending the data to the camera
        camera.SendMessage("OnLook",lookvalue);

        //needs to be in update to get accurate camera location
        cameraRight = camera.transform.right;

        //I dont use camera.transform.forward as that cares about the y rotation which I dont want it to.
        cameraForward = new Vector3 (-cameraRight.z,0.0f,cameraRight.x);
    }

    void OnScroll(InputValue scrollValue){
        camera.SendMessage("OnScroll",scrollValue);
    }

   void Update(){
        
        //multiplys the movement vectors components with the camera direction to align them
        //this is at 90 degrees to what you think it should be because the addTourque function is weird and dumb
        xMovement = movement.x * -cameraForward;
        yMovement = movement.y * cameraRight;
   }

    void FixedUpdate() {

        Vector3 movementVect = xMovement + yMovement;
        rb.AddTorque(movementVect*moveSpeed);
    
   }
}
