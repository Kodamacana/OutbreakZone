using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class MoveMe : MonoBehaviour
{
    [SerializeField] GameObject FlashLight;
    bool AR;
    [SerializeField] GameObject ARKitGameObject;
    [Range(1, 10)]
    public float AR_sensivity = 1;
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public float lookYLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    float rotationY = 0;
    [SerializeField] ZombieController zombieController;
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

    public KeyCode aimlockKey = KeyCode.Mouse1;
    public float maxDistance = 100f;
    public float aimlockDistance = 10f;
    public float aimlockAngle = 45f;
    public float smoothSpeed = 5f;

    private Transform lockedTarget;
    private bool isAimlockActive = false;


    void Start()
    {
        playerCamera = Camera.main;
        //zombieController.SpawnZombies();
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
        if (ARKitGameObject != null)
        {
            if (ARKitGameObject.activeSelf)
            {
                AR = true;
                maxDistance = 1000f;
                aimlockDistance = 100f;
                aimlockAngle = 90f;
                smoothSpeed = 50f;
            }
            else
            {
                //maxDistance = 100f;
                //aimlockDistance = 10f;
                //aimlockAngle = 45f;
                //smoothSpeed = 5f;
                AR = false;
            }
        }
        bool ARFire = false;
        float mouthSmile_L = skinnedMeshRenderer.GetBlendShapeWeight(skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("mouthSmile_L"));
        float mouthSmile_R = skinnedMeshRenderer.GetBlendShapeWeight(skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("mouthSmile_R"));

        if ((AR) && mouthSmile_L >= 40 && mouthSmile_R >= 40) { ARFire = true; }

        if (Input.GetKeyDown(KeyCode.F)) 
        {
            FlashLight.SetActive(!FlashLight.activeSelf);
        }

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
        if (AR)
        {
            RaycastHit hit;
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

            // Fare pozisyonunda bir ýþýn oluþtur ve çarpýþma kontrolü yap
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                // Çarpýþma noktasýndaki düþmaný hedefle
                if (Physics.Raycast(ray, out hit, maxDistance))
                {
                    // Çarpýþma noktasýndaki düþmaný hedefle
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        float distanceToEnemy = Vector3.Distance(transform.position, hit.collider.transform.position);
                        Vector3 directionToEnemy = hit.collider.transform.position - transform.position;
                        float angleToEnemy = Vector3.Angle(transform.forward, directionToEnemy);

                        // Aimlock tuþuna basýldýðýnda, düþman belirli bir mesafede ve belirli bir bakýþ açýsý içerisindeyse aimlock'u etkinleþtir
                        if (ARFire && distanceToEnemy <= aimlockDistance && angleToEnemy <= aimlockAngle)
                        {
                            lockedTarget = hit.collider.transform;
                            isAimlockActive = true;
                        }
                    }
                }

                // Aimlock tuþu býrakýldýðýnda aimlock'u kapat
                if (ARFire)
                {
                    isAimlockActive = false;
                }

                // Hedef kilidi varsa, her güncellemede ona doðru pürüzsüz bir þekilde dön
                if (lockedTarget != null && isAimlockActive)
                {
                    Vector3 targetPosition = lockedTarget.position;
                    Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);
                }
            }
        
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