using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform target;  // The player's camera target
    [SerializeField] private float distance = 5.0f;  // Distance from the target
    [SerializeField] private float lookSpeed = 100.0f;  // Speed of camera rotation
    [SerializeField] private float minZoom = 2.0f;  //  Minimum zoom distance
    [SerializeField] private float maxZoom = 15.0f;  //  Maximum zoom distance

    private InputSystem_Actions playerInput;
    private Vector2 lookInput;

    private float yaw = 0.0f;  //  Left/Right
    private float pitch = 20.0f;  //Up/Down

    private void Awake()
    {
        playerInput = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (target == null)
        {
            target = GameObject.Find("CameraTarget").transform;
        }
    }
    void LateUpdate()
    {
        if (target == null) return;

        //  Orbit Logic
        lookInput = playerInput.Player.Look.ReadValue<Vector2>();
        yaw += lookInput.x * lookSpeed * Time.deltaTime;
        pitch -= lookInput.y * lookSpeed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -30f, 60f);  // Clamp vertical orbit

        // Zoom Logic
        float scroll = Mouse.current.scroll.ReadValue().y;
        distance -= scroll * 0.01f * 5.0f;  //Adjust multiplier

        // Clamp the zoom
        distance = Mathf.Clamp(distance, minZoom, maxZoom);

        //  Position logic
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredPosition = target.position - (rotation * Vector3.forward * distance);
        transform.position = desiredPosition;

        transform.LookAt(target);
    }
    
}
