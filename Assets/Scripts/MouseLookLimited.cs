using UnityEngine;

public class MouseLookLimited : MonoBehaviour
{
    [Header("Sensitivity Settings")]
    public float baseSensitivity = 15f;

    [Header("Rotation Limits")]
    public float maxLookLeft = -15f;
    public float maxLookRight = 15f;
    public float maxLookUp = -15f;
    public float maxLookDown = 15f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        // Initialize rotation values with the current rotation of the camera
        Vector3 initialRotation = transform.localEulerAngles;
        rotationX = initialRotation.y;
        rotationY = initialRotation.x;
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * baseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * baseSensitivity * Time.deltaTime;

        // Scale sensitivity based on proximity to limits
        float horizontalScale = GetScaleFactor(rotationX, maxLookLeft, maxLookRight);
        float verticalScale = GetScaleFactor(rotationY, maxLookUp, maxLookDown);

        mouseX *= horizontalScale;
        mouseY *= verticalScale;

        // Update rotation angles
        rotationX += mouseX; // Horizontal rotation
        rotationY -= mouseY; // Vertical rotation (subtract to invert)

        // Clamp the rotations to the defined limits
        rotationX = Mathf.Clamp(rotationX, maxLookLeft, maxLookRight);
        rotationY = Mathf.Clamp(rotationY, maxLookUp, maxLookDown);

        // Apply the rotation to the transform
        transform.localRotation = Quaternion.Euler(rotationY, rotationX, 0f);
    }

    // Calculate scale factor based on proximity to rotation limits
    private float GetScaleFactor(float current, float minLimit, float maxLimit)
    {
        float distanceToMin = Mathf.Abs(current - minLimit);
        float distanceToMax = Mathf.Abs(current - maxLimit);

        // Find the smallest distance to a limit
        float distanceToEdge = Mathf.Min(distanceToMin, distanceToMax);

        // Normalize the distance (1.0 at the middle, approaching 0.0 near the edges)
        float totalRange = Mathf.Abs(maxLimit - minLimit) / 2f;
        return Mathf.Clamp01(distanceToEdge / totalRange);
    }
}
