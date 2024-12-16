using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FPController : MonoBehaviour
{
    [Header("Movement Inputs")]
    public KeyCode forwardKey;

    public KeyCode backKey;

    public KeyCode leftKey;

    public KeyCode rightKey;

    [Header("Movement Speed")]

    public float moveSpeed;

    public float turnSpeed;

    private Rigidbody rb;

    private float translation;
    private float strafe;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
         Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        translation = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        strafe = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.Translate(strafe, 0, translation);
    }
}
