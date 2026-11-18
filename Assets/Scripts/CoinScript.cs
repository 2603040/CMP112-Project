using UnityEngine;

public class CoinScript : MonoBehaviour
{
    void Update() 
    { 
        transform.Rotate(new Vector3(12, 40, 25) * Time.deltaTime); 
    } 
}

