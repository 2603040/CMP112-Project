 using UnityEngine;  
 using System.Collections;    
 using UnityEngine.InputSystem;
 using System;

//code originaly from this stack overflow question https://stackoverflow.com/questions/56382451/how-to-rotate-camera-around-player-in-unity-c-sharp
//I then edited it so it works with the new input system and added the ability for vertical camera rotation

public class CameraController : MonoBehaviour {

     public float xturnSpeed = 5.0f;
     public float yturnSpeed = 5.0f;
     public GameObject player;

     private Transform playerTransform;     
     private Vector3 offset;
     private float yOffset = 10.0f;
     private float zOffset = 10.0f;
     private Vector2 PreviosPos = new Vector2(0,0);

     void Start () {
         playerTransform = player.transform;
         offset = new Vector3(playerTransform.position.x, playerTransform.position.y + yOffset, playerTransform.position.z + zOffset);
     }

        
     void Update()
     {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        //gets the angle perpendicular to the cameras current y rotation
        float perpToYAngle = transform.localEulerAngles[1]+90;
        float perpToYAngleRad =(perpToYAngle* (float)3.14159)/180;

        //gets the vector that is perpendicular to the cameras current y rotation
        Vector3 localLeft = new Vector3 (Mathf.Sin(perpToYAngleRad),0,Mathf.Cos(perpToYAngleRad));

        //rotates the camera vertically around the axis it is perpedicular to
        offset = Quaternion.AngleAxis (mouseDelta.y * -yturnSpeed, localLeft) * offset;


        offset = Quaternion.AngleAxis (mouseDelta.x * xturnSpeed, Vector3.up) * offset;
        transform.position = playerTransform.position + offset; 
        transform.LookAt(playerTransform.position);



         transform.position = playerTransform.position + offset; 
     } 
}
