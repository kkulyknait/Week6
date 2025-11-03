using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManger : MonoBehaviour
{
    [SerializeField] GameObject firstPersonHead;
    [SerializeField] GameObject thirdPersonCamera;
    [SerializeField] PlayerController playerController;

    private bool isFirstPerson = true;
    private InputSystem_Actions playerInput;

    private void Awake()
    {
        playerInput = new InputSystem_Actions();
        playerInput.Player.SwitchCamera.performed += ctx => ToggleCamera();

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
        firstPersonHead.SetActive(true);
        thirdPersonCamera.SetActive(false);
        playerController.SetMovementMode(true);

    }
    void ToggleCamera()
    {
        isFirstPerson = !isFirstPerson;
        //Toggle camera objects
        firstPersonHead.SetActive(isFirstPerson);
        thirdPersonCamera.SetActive(!isFirstPerson);

        //Tell PlayerController which mode to use
        playerController.SetMovementMode(isFirstPerson);
    }
    
}
