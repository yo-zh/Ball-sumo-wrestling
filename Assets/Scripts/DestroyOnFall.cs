using UnityEngine;

public class DestroyOnFall : MonoBehaviour
{
    void Update()
    {
        if (transform.position.x > 50 || transform.position.x < -50 || 
            transform.position.z > 50 || transform.position.z < -50 || 
            transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }
}
