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

     void Start () {
         playerTransform = player.transform;
         offset = new Vector3(0, 0, zOffset);
     }

        
     void Update()
     {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        //rotates the camera around the local right vector
        offset = Quaternion.AngleAxis (mouseDelta.y * -yturnSpeed, transform.right) * offset;


        offset = Quaternion.AngleAxis (mouseDelta.x * xturnSpeed, Vector3.up) * offset;
        transform.position = playerTransform.position + offset; 
        transform.LookAt(playerTransform.position);



         transform.position = playerTransform.position + offset; 
     } 
}
