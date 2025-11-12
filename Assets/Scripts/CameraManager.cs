using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject firstPersonHead;
    [SerializeField] GameObject thirdPersonCamera;
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject dialogueCamera;

    private GameObject activePlayerCamera;  //Remember which camera is active
    private bool isFirstPerson = true;
    private InputSystem_Actions playerInput;
    private bool isInteracting = false;

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
        dialogueCamera.SetActive(false);
        playerController.SetMovementMode(true);
        activePlayerCamera = firstPersonHead;
    }
    void ToggleCamera()
    {

        if (isInteracting) return;

        isFirstPerson = !isFirstPerson;
        //Toggle camera objects
        firstPersonHead.SetActive(isFirstPerson);
        thirdPersonCamera.SetActive(!isFirstPerson);

        //Store the new active camera
        activePlayerCamera = isFirstPerson ? firstPersonHead : thirdPersonCamera;

        //Tell PlayerController which mode to use
        playerController.SetMovementMode(isFirstPerson);
    }

    public void EnableDialogueCamera(Transform dialogueTarget)
    {
        isInteracting = true;
        dialogueCamera.transform.position = dialogueTarget.position;
        dialogueCamera.transform.rotation = dialogueTarget.rotation;
        activePlayerCamera.SetActive(false);  //Disable current player camera
        dialogueCamera.SetActive(true);        //Enable dialogue camera
    }

    public void DisableDialogueCamera()
    {
        isInteracting = false;
        dialogueCamera.SetActive(false);       //Disable dialogue camera
        activePlayerCamera.SetActive(true);    //Enable current player camera
    }
    
}
