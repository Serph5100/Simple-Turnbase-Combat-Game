using UnityEngine;

public class IsometricCameraRotation : MonoBehaviour
{
    public float rotationSpeed = 50f;

    void Update()
    {
        if (Input.GetMouseButton(1)) // Right mouse button
        {
            float mouseDeltaX = Input.GetAxis("Mouse X");

            transform.Rotate(Vector3.up, mouseDeltaX * rotationSpeed * Time.deltaTime, Space.World);
            
        }
    }
}
