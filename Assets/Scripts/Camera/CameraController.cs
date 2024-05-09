using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // A and D keys
        float verticalInput = Input.GetAxis("Vertical"); // W and S keys
        float depthInput = -Input.GetAxis("Mouse ScrollWheel") * Mathf.Min(transform.position.y, 1000f); // Mouse scroll wheel

        speed = Mathf.Min(transform.position.y * 3, 1000f);

        // Calculate movement vector based on input and speed
        Vector3 movement = speed * Time.deltaTime * new Vector3(horizontalInput, depthInput, verticalInput);

        // Move the camera
        transform.Translate(movement, Space.World);
    }
}
