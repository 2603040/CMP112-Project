using UnityEngine;

public class scr : MonoBehaviour 
{ 
 
 
    void Update() 
    { 
        transform.Rotate(new Vector3(12, 40, 25) * Time.deltaTime); 
    } 
} 
