using UnityEngine;

public class IsometricCameraRotation : MonoBehaviour
{
    public float rotationSpeed = 50f;

    /* void Update()
    {
        if (Input.GetMouseButton(1)) // Right mouse button
        {
            float mouseDeltaX = Input.GetAxis("Mouse X");

            transform.Rotate(Vector3.up, mouseDeltaX * rotationSpeed * Time.deltaTime, Space.World);

        }
    } */

    public void RotateCamera()
    {
        Quaternion targetRotation = Quaternion.AngleAxis(45f, Vector3.up) * transform.rotation;
        StartCoroutine(RotateTo(targetRotation, rotationSpeed / 100f)); // Adjust duration as needed

        }

        private System.Collections.IEnumerator RotateTo(Quaternion targetRotation, float duration)
        {
        Quaternion startRotation = transform.rotation;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRotation;
    }
}
