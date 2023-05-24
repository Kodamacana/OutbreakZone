using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class MoveMe : MonoBehaviour
{
    [SerializeField] bool AR;
    [Range(1, 10)]
    public float ARsensivity = 1;
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    [Header("AR Callibration")]
    [Header("------------------")]
    [Header("Head Rotation")]
    [SerializeField] float LeftCoordinate = 0;
    [SerializeField] float RightCoordinate = 0;
    [SerializeField] GameObject HeadObject;
    Vector3 HeadRotation;

    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;
    Mesh mesh;
    int blendShapeIndex = 0;
    int blendShapeCount = 0;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        mesh = skinnedMeshRenderer.sharedMesh;

        blendShapeCount = skinnedMeshRenderer.sharedMesh.blendShapeCount;
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Callibration()
    {
        //head rotationPosition = Vector3 HeadRotation
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            if (!AR)
            {
                rotationX += Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }
            else
            {
                var result = Mathf.Abs(RightCoordinate) + Mathf.Abs(LeftCoordinate);
                result = 360 / result;
                float originalValue = HeadObject.transform.localRotation.eulerAngles.y;

                playerCamera.transform.localRotation = Quaternion.Euler(HeadObject.transform.localRotation.eulerAngles.x, 0, 0);
                transform.rotation = Quaternion.Euler(0, originalValue * result, 0);
            }
        }
    }




   
        

        
    

}