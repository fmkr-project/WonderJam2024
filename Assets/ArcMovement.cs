using UnityEngine;

public class ArcMovement : MonoBehaviour
{
    public float horizontalSpeed = 1f; 
    public float gravity = 9.81f; 
    private float verticalSpeed = 0f; 
    
    void Update()
    {
        
        transform.position += Vector3.right * horizontalSpeed * Time.deltaTime;
        
       
        verticalSpeed -= gravity * Time.deltaTime;
        
        
        transform.position += Vector3.up * verticalSpeed * Time.deltaTime;
    }
}