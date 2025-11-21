using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CannonControl : MonoBehaviour
{
    [SerializeField] private Animator cannonAnimator;  //  Drag the barrel here
    [SerializeField] private GameObject interactText;  //  Drag Text UI Here
    [SerializeField] private Transform barrelEnd;   // Drag Barrel Tip here
    [SerializeField] private GameObject muzzleFlashPrefab;  // Drag Muzzle Flash Prefab  

    private bool isPlayerInRange = false;
    private InputSystem_Actions playerInput;

    private void Awake()
    {
        playerInput = new InputSystem_Actions();
      
    }
    private void OnEnable()
    {
        playerInput.Player.Enable();
        Debug.Log("Cannon: OnEnable ran. Input Enabled.");
    }
    private void OnDisable()
    {
        playerInput.Player.Disable();
    }
    private void Update()
    {
        
        if (playerInput.Player.Interact.WasPressedThisFrame())
        {
            Debug.Log("Cannon: 'E' pressed in Update!");
            TryFire();
        }
    }


    //  Trigger Logic
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            interactText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactText.SetActive(false);
        }
    }

    // Firing logic
    private void TryFire()
    {
        Debug.Log("TryFire Called!");
        if (isPlayerInRange)
        {
            //trigger animation transition
            cannonAnimator.SetTrigger("Fire");
            // Spawn Particles
            if (muzzleFlashPrefab != null && barrelEnd != null)
            {
                Instantiate(muzzleFlashPrefab, barrelEnd.position, barrelEnd.rotation);
            }
    }
}


}
