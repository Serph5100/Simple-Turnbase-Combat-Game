using UnityEngine;

public class IsometricCameraPanner : MonoBehaviour
{
    public float panSpeed = 6f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector2 panLimitX;
    public Vector2 panLimitZ;
    private Camera _camera;

    // Update is called once per frame
    private void Awake()
    {
        _camera = GetComponentInChildren<Camera>();
        if (_camera == null)
        {
            Debug.LogError("Camera component not found on this GameObject.");
        }
        
    }
    void Update()
    {
        Vector2 panPosition = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        //transform.position += new Vector3(panPosition.x, 0, panPosition.y) * (panSpeed * Time.deltaTime);
        transform.position +=
        Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) *
        new Vector3(panPosition.x, 0, panPosition.y) *
        (panSpeed * Time.deltaTime);

        // Clamp the camera position to the defined limits
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, panLimitX.x, panLimitX.y),
            transform.position.y,
            Mathf.Clamp(transform.position.z, panLimitZ.x, panLimitZ.y)
        );
        
    }
}
