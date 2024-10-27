using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;         // The target to orbit around, such as the player
    public float zoomSpeed = 5.0f;   // Speed for zooming in/out
    public float panSpeed = 5.0f;
    public float rotationSpeed = 100.0f; // Speed for rotating the camera
    public float minZoom = 5.0f;     // Minimum zoom distance
    public float maxZoom = 20.0f;    // Maximum zoom distance

    private bool isCameraControlActive = false; // Tracks if camera control is active
    private float currentZoom = 5;          // Initial zoom level
    private Vector3 currentRotation;

    void Start()
    {
        // Initialize currentRotation based on the camera's initial rotation
        currentRotation = transform.eulerAngles;
        HandleCameraControl();
    }

    void Update()
    {
        transform.parent.localRotation = Quaternion.identity;
        isCameraControlActive = Input.GetMouseButton(1);

        if (isCameraControlActive)
        {
            HandleCameraControl();
        }
    }

    void HandleCameraControl()
    {
        // Rotate camera around the player based on mouse movement
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        if (Input.GetMouseButton(2))
        {
            transform.localPosition += Vector3.right * mouseX + Vector3.up * mouseY;
        }
        else
        {
            // Adjust current rotation based on mouse input
            currentRotation.y += mouseX;
            currentRotation.x -= mouseY; // Invert Y axis for natural feel

            // Clamp the vertical rotation to avoid flipping
            currentRotation.x = Mathf.Clamp(currentRotation.x, -40, 80);

            // Apply the rotation to the camera
            Quaternion rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0);
            transform.position = player.position - rotation * Vector3.forward * currentZoom;
            transform.LookAt(player);

            // Zoom in and out based on the scroll wheel
            float scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom - scroll, minZoom, maxZoom);
        }

        

        
    }
}
