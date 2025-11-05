using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    [SerializeField] public GameObject interactText;
    public bool playerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        //Chect the tag to see if it's the player
        if (other.CompareTag("Player"))
        {
            interactText.SetActive(true);
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.SetActive(false);
            playerInRange = false;
        }
    }
}
