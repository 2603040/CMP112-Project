 using UnityEngine;  
 using System.Collections;    
 using UnityEngine.InputSystem;
 using System;

//code originaly from this stack overflow question https://stackoverflow.com/questions/56382451/how-to-rotate-camera-around-player-in-unity-c-sharp
//I then edited it so it works with the new input system and added the ability for vertical camera rotation

public class CameraController : MonoBehaviour {

     public float xturnSpeed = 5.0f;
     public float yturnSpeed = 5.0f;
     public float zOffset = 5.0f;
     public GameObject player;

     private Transform playerTransform;     
     private Vector3 offset;
     private float viewDistance = 15;

     void Start () {
         playerTransform = player.transform;
         offset = new Vector3(0, 0, zOffset);
     }

        
     void OnLook(InputValue lookValue)
     {
        Vector2 mouseDelta = lookValue.Get<Vector2>();

        //rotates the camera around the local right vector
        offset = Quaternion.AngleAxis (mouseDelta.y * -yturnSpeed, transform.right) * offset;
        offset = Quaternion.AngleAxis (mouseDelta.x * xturnSpeed, Vector3.up) * offset;
     } 

     void OnScroll(InputValue scrollValue){
            viewDistance -= scrollValue.Get<float>();
            Debug.Log(viewDistance);

            if (viewDistance <= 0){
                viewDistance = 1;
            }

            if (viewDistance > 20){
                viewDistance = 20;
            }

            float multiplyer = viewDistance/offset.magnitude;
                offset = new Vector3 (offset.x*multiplyer,offset.y*multiplyer,offset.z*multiplyer);
        
     }
        
        
        
     

     void Update(){
        transform.position = playerTransform.position + offset; 
        transform.LookAt(playerTransform.position);
     }
}
