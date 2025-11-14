using UnityEngine;

public class TreasurePickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindFirstObjectByType<GameManager>().FoundTreasure();
            Destroy(gameObject);
        }
    }

    
}
