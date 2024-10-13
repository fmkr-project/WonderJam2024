using UnityEngine;

public class SlowMove : MonoBehaviour
{
    public Vector3 direction = Vector3.right; 
    public float speed = 0.1f; 
    public float acceleration = 0.01f;

    void Update()
    {
        transform.position += direction.normalized * speed * Time.deltaTime;
        speed += acceleration * Time.deltaTime; 
    }
}