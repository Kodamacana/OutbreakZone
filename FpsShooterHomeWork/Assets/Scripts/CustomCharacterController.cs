using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCharacterController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float gravity = 20f;

    private Rigidbody rb;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Check if the character is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        // Calculate movement direction based on user input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput);

        // Apply movement input
        Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);

        // Jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Apply gravity
        rb.AddForce(Vector3.down * gravity * Time.deltaTime, ForceMode.Acceleration);
    }
}
