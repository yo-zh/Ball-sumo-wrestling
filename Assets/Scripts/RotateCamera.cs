using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] float cameraSpeed = 1.0f;
    private float horizontalInput;

    void Update()
    {
        horizontalInput = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, horizontalInput * cameraSpeed);
    }
}
