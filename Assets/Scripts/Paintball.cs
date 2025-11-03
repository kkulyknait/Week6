using System.IO;
using UnityEngine;

public class Paintball : MonoBehaviour
{
    [SerializeField] private float damage = 10.0f;
    private void OnEnable()
    {
        // Disable the paintball after 3 seconds if we don't hit anything
        Invoke("Deactivate", 3.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //damage logic here
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.ApplyDamage(damage);  //  Apply Damage on Hit
        }
        Deactivate();
    }

    void Deactivate()
    {
        // check if it's in the pool
        if (gameObject.activeInHierarchy)
        {
            //reset physics
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.angularVelocity = Vector3.zero;
                rb.linearVelocity = Vector3.zero;
            }
            //Return to pool
            gameObject.SetActive(false);
        }
    }
    //private void OnTriggerEnter(Collider other)
    //    {
    //    ColorMod colorMod = other.gameObject.GetComponent<ColorMod>();
    //    // other is the collidoer of the object that enters the trigger
    //    // Get the game object and grab the ColorMod script
    //    if (colorMod != null)
    //    {
    //        Material myMaterial = GetComponent<Renderer>().material;
    //        colorMod.ChangeColor(myMaterial);
    //    }

    // }
}
