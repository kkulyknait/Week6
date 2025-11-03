using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
    //  OnEnable runs every time SetActive(true) is called
    private void OnEnable()
    {
        //Failsafe:  Deactivate after 3 seconds if it hits nothing
        //We Invoke to call deactivate after a delay
        Invoke("Deactivate", 3.0f);

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // Destroy(gameObject, 3.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Deactivate();
        //  Destroy(gameObject);
    }

    //Deactivate is our helper to handle deactivation
    void Deactivate()
    {
        //check if it's active first
        if(gameObject.activeInHierarchy)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            //Return to the object pool
            gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
