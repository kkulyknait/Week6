using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
public class GameManager : MonoBehaviour
{
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerLook playerLook;
    [SerializeField] private Transform dialogueCamPos;

    [SerializeField] private GameObject mapIcon;
    [SerializeField] private GameObject treasureIcon;
    [SerializeField] private TextMeshProUGUI questStatusText;

    private InputSystem_Actions playerInput;
    private NPC npc;
    private bool isInteracting = false;

    public enum QuestState { NotStarted, MapQuest, TreasureQuest, Complete }
    public QuestState currentQuestState;

    private void Awake()
    {
        playerInput = new InputSystem_Actions();
        playerInput.Player.Interact.performed += ctx => OnInteract();

    }
    private void OnEnable()
    {
        if (playerInput == null)
        {
            Debug.LogError("playerInput is NULL in OnEnable!");
            return;
        }
        Debug.Log("GameManager OnEnable() called, enabling input.");
        playerInput.Player.Enable();
    }
    private void OnDisable()
    {
        // --- ADD THESE LINES ---
        if (playerInput == null)
        {
            Debug.LogError("playerInput is NULL in OnDisable!");
            return;
        }
        Debug.Log("GameManager OnDisable() called, disabling input.");
        playerInput.Player.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        npc = FindFirstObjectByType<NPC>();
        cameraManager = GetComponent<CameraManager>();
        currentQuestState = QuestState.NotStarted;
        UpdateQuestUI();

    }

    void OnInteract()
    {
        Debug.Log("OnInteract() called! (E key was pressed)");
        // Check if player is in range of NPC
        if (npc.playerInRange)
        {
            isInteracting = !isInteracting;  //Toggle interactive state
            if (isInteracting)
            {
                // Enable dialogue camera
                cameraManager.EnableDialogueCamera(dialogueCamPos);
                // Disable player controls
                playerController.enabled = false;
                playerLook.enabled = false;
                Cursor.lockState = CursorLockMode.None;  //  Unlock cursor
                npc.interactText.SetActive(false);  // Hide interaction text
                
                if (currentQuestState == QuestState.NotStarted)
                {
                    currentQuestState = QuestState.MapQuest;
                 }
                else if (currentQuestState == QuestState.MapQuest)
                {
                    currentQuestState = QuestState.TreasureQuest;
                }
                else if (currentQuestState == QuestState.TreasureQuest)
                {
                    currentQuestState = QuestState.Complete;
                }
                if (currentQuestState == QuestState.Complete)
                {
                    //Quest Complete Message
                    questStatusText.text = "Quest Complete!";
                }
                UpdateQuestUI();
            }
            else
            {
                // Disable dialogue camera
                cameraManager.DisableDialogueCamera();
                // Enable player controls
                playerController.enabled = true;
                playerLook.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;  //  Lock cursor
                npc.interactText.SetActive(true);  // Show interaction text
            }

        }
    }
    public void UpdateQuestUI()
    {
      switch (currentQuestState)
        {
            case QuestState.NotStarted:
                questStatusText.text = "Quest: Talk to the NPC to start your adventure!";
                mapIcon.SetActive(false);
                treasureIcon.SetActive(false);
                break;
            case QuestState.MapQuest:
                questStatusText.text = "Quest: Find the hidden map!";
                mapIcon.SetActive(true);
                treasureIcon.SetActive(false);
                break;
            case QuestState.TreasureQuest:
                questStatusText.text = "Quest: Use the map to find the treasure!";
                mapIcon.SetActive(false);
                treasureIcon.SetActive(true);
                break;
            case QuestState.Complete:
                questStatusText.text = "Quest Complete! You found the treasure!";
                mapIcon.SetActive(false);
                treasureIcon.SetActive(false);
                break;
        }
    }

    private void Update()
    {
        // This will check the "Interact" action every single frame.
        // wasPressedThisFrame is the equivalent of GetKeyDown
        if (playerInput.Player.Interact.WasPressedThisFrame())
        {
            Debug.Log("GameManager Update() DETECTED Interact action!");

            // We can even manually call OnInteract() here for our test
            OnInteract();
        }
    }
}
