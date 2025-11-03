using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 3.0f;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float rotationSpeed = 10.0f;  // Smoothing for TPS rotation
    [SerializeField] private Transform thirdPersonCameraTransform;


    private Rigidbody rBody;
    private InputSystem_Actions playerInput;
    private Vector2 moveInput; //  store the players X,Y input
    private bool grounded = true;
    private bool isFirstPersonMode = true;  //Default to FPS

    private void Awake()
    {
        rBody = GetComponent<Rigidbody>();
        playerInput = new InputSystem_Actions();
        playerInput.Player.Jump.performed += context => Jump();

    }
    private void OnEnable()
    {
        playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        playerInput.Player.Disable();
    }

    public void SetMovementMode(bool isFirstPerson) 
    {
        isFirstPersonMode = isFirstPerson;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = playerInput.Player.Move.ReadValue<Vector2>(); 
    }
    private void FixedUpdate()
    {
        // All physics based movement should be in FixedUpdate()
        //  FixedUpdate() runs in sync with the physics engine
        //  Set rBody velocity directly but preserve Y velocity so movement doesn't interfere with gravity

        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

        if (isFirstPersonMode)
        {
            //First Person Logic
            //Move relative to the PlayerLook
            Vector3 worldRelativeMove = transform.TransformDirection(moveDirection);
            rBody.linearVelocity = new Vector3 (worldRelativeMove.x * movementSpeed, rBody.linearVelocity.y, worldRelativeMove.z * movementSpeed);

        }
        else
        {
            //Third Person Logic
            //Moving relative to camera's facing direction
            Vector3 camForward = thirdPersonCameraTransform.forward;
            Vector3 camRight = thirdPersonCameraTransform.right;
            //zero out Y to prevent flying
            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            //  Calculate world-space movement direction
            Vector3 worldMove = (camForward * moveInput.y + camRight * moveInput.x) * movementSpeed;
            rBody.linearVelocity = new Vector3(worldMove.x, rBody.linearVelocity.y, worldMove.z);

            ///  Rotate player to face movement direction smoothly
            if(moveDirection.magnitude >= 0.1f)
            {
                // Calculate rotation based on movement direction
                Quaternion targetRotation = Quaternion.LookRotation(worldMove);
                //Smoothly rotate
                rBody.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            }

        }

    }
    /// <summary>
    ///  Unity Method called automatically when collider touches another collider
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;
    }
    
    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }
    private void Jump()
    {
        if (grounded)
        {
            //Add force to rigidbody
            rBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }
}
