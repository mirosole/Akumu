using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float runSpeed;
    public bool BlockMovemant { get; set; }
    public bool BlockRotation { get; set; }


    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float mouseSensitivity = 2.0f;
    [SerializeField] private float cameraBobFrequencyWalking = 8f;
    [SerializeField] private float cameraBobFrequencyRunning = 11f;
    [SerializeField] private float cameraBobAmplitude = 0.02f;
    [SerializeField] private float bobSmoothingSpeed = 5f;
    [SerializeField] private float extraGravityMultiplier = 2.0f;
    [SerializeField] private Transform cameraTransform;


    private Vector3 cameraInitialPosition;
    private float bobbingTime;
    private float verticalRotation;
    private Rigidbody rb;
    private bool isRunning;
    private float currentBobAmplitude;
    private bool isFalling;
    
    // Boolean used for pause control
    public bool isPaused = false;

    void Start()
    {
        runSpeed = walkSpeed * 1.4f;
        cameraInitialPosition = cameraTransform.localPosition;
        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotation;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentBobAmplitude = 0f;
        isRunning = false;
        verticalRotation = 0.0f;
        bobbingTime = 0.0f;
        isFalling = false;
    }

    void Update()
    {
        if(isPaused || BlockRotation)
            return; 
        isRunning = Input.GetKey(KeyCode.LeftShift);

        // camera 
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        // effects 
        if (rb.velocity.magnitude > 0.1f && !isFalling)
        {
            bobbingTime += Time.deltaTime * (isRunning ? cameraBobFrequencyRunning : cameraBobFrequencyWalking);

            float bobbingOffsetX = Mathf.Sin(bobbingTime) * currentBobAmplitude;
            float bobbingOffsetY = Mathf.Cos(bobbingTime * 2) * currentBobAmplitude * 0.5f;
            cameraTransform.localPosition = cameraInitialPosition + new Vector3(bobbingOffsetX, bobbingOffsetY, 0);

            currentBobAmplitude = Mathf.Lerp(currentBobAmplitude, cameraBobAmplitude, Time.deltaTime * bobSmoothingSpeed);
        }
        else
        {
            currentBobAmplitude = Mathf.Lerp(currentBobAmplitude, 0f, Time.deltaTime * bobSmoothingSpeed);
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, cameraInitialPosition, Time.deltaTime * bobSmoothingSpeed);
        }
    }

    void FixedUpdate()
    {
        if (BlockMovemant)
            return;

        // movement 
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        Vector3 movement = (transform.forward * moveVertical + transform.right * moveHorizontal).normalized * currentSpeed;

        float verticalVelocity = rb.velocity.y;

        isFalling = verticalVelocity < -0.1f;

        // additional gravity 
        if (isFalling)
        {
            verticalVelocity += Physics.gravity.y * (extraGravityMultiplier - 1) * Time.fixedDeltaTime;
        }

        // final speed 
        rb.velocity = new Vector3(movement.x, verticalVelocity, movement.z);
    }

    public float getWalkSpeed() => walkSpeed;
    public float getRunSpeed() => runSpeed;
    public bool isPlayerRunning() => isRunning;
    public bool isPlayerFalling() => isFalling;



}
