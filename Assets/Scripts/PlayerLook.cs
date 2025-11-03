using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{

    [SerializeField] private float lookSpeed = 100.0f;
    [SerializeField] private Transform playerBody;
    private Vector2 lookInput;
    private InputSystem_Actions playerInput;
    private float xRotation = 0f;
    private void Awake()
    {
        playerInput = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();
    }
    private void OnDisable()
    {
        playerInput.Player.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //  Read the mouse input
        lookInput = playerInput.Player.Look.ReadValue<Vector2>();
        float mouseX = lookInput.x * lookSpeed * Time.deltaTime;
        float mouseY = lookInput.y * lookSpeed * Time.deltaTime;
        xRotation -= mouseY;

        //CLAMP  
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //  Looking left and right, rotate the entire player body on Y
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
