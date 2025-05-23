using System;
using UnityEngine;

public class IsometricCameraZoom : MonoBehaviour
{
    public float zoomSpeed = 2f;
    public float zoomSmoothness = 5f;

    public float minZoom = 5f;
    public float maxZoom = 20f;

    private float _currentZoom;

    private Camera _camera;

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
        _currentZoom = Mathf.Clamp(_currentZoom - Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime, minZoom, maxZoom);
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _currentZoom, Time.deltaTime * zoomSmoothness);
    }
}
