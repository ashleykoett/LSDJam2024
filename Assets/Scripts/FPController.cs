using UnityEngine;

public class FPController : MonoBehaviour
{
    [Header("Movement Inputs")]
    public KeyCode forwardKey;
    public KeyCode backKey;
    public KeyCode leftKey;
    public KeyCode rightKey;

    [Header("Movement Settings")]
    public float moveSpeed;
    public float turnSpeed;

    [Header("Look Settings")]
    public float lookSpeed;
    public float maxLookAngle = 60f;

    private Rigidbody rb;
    private float translation;
    private float strafe;
    private float verticalLookRotation = 0f;

    [SerializeField]
    private Camera playerCamera;

    public bool playerWalking = false;

    private AudioSource audioSource;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        // Lock the cursor to the screen
        Cursor.lockState = CursorLockMode.Locked;

        // Find the camera component on the player
        playerCamera = GetComponentInChildren<Camera>();
        if (playerCamera == null)
        {
            Debug.LogError("No Camera found as a child of the player.");
        }
    }

    void Update()
    {
        playerWalking = IsWalking();
      

        // Handle movement
        translation = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        strafe = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.Translate(strafe, 0, translation);

        // Handle turning
        float mouseX = Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;
        transform.Rotate(0, mouseX, 0);

        // Handle looking up and down
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime;
        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -maxLookAngle, maxLookAngle);

        if (playerCamera != null)
        {
            playerCamera.transform.localRotation = Quaternion.Euler(verticalLookRotation, 0, 0);
        }
        


    }

    //checks if the player is walking by looking at translation values
    bool IsWalking()
    {
        return Mathf.Abs(translation) > 0.001f || Mathf.Abs(strafe) > 0.001f;
    }
    
}
