using UnityEngine;

public class PlayerLauncher : MonoBehaviour
{
    public GameObject grenade;
    public GameObject barrelEnd;

    float grenadeSpeed;
    float fireRate, timeStamp;

    GameObject[] bulletPool;  //  an array to hold our grenades
    int index;  //  keep track of which grenade to use next

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grenadeSpeed = 20.0f;
        fireRate = 0.5f;
        timeStamp = -1.0f;

        //  Create the pool
        index = 0;  //beginning of the pool
        bulletPool = new GameObject[10];  //create array for 10 grenades
        for (int i = 0; i < bulletPool.Length; i++)
        {
            //Instantiate grenade and store in array
            bulletPool[i] = Instantiate(grenade, Vector3.zero, Quaternion.identity);
            //deactivate it, hiding it from the scene
            bulletPool[i].SetActive(false);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire1") == 1 && Time.time > timeStamp + fireRate)
        {
            timeStamp = Time.time;

            //  New firing logic without instantiate
            //  Get the next grenade from the pool
            GameObject currentBullet = bulletPool[index];


            //  Move it to the barrel end position and rotation
            currentBullet.transform.position = barrelEnd.transform.position;
            currentBullet.transform.rotation = barrelEnd.transform.rotation;

            //  Activate Grenade
            currentBullet.SetActive(true);

            //  Fire it by setting velocity
            currentBullet.GetComponent<Rigidbody>().linearVelocity = 
            currentBullet.transform.forward * grenadeSpeed;

            // Move forward in the pool
            index++;

            //  If we reach the end of the pool, loop back to the beginning
            if (index >= bulletPool.Length)
            {
                index = 0; // cycle back to first grenade
            }

            //  

            ////Create our grenade prefab
            //GameObject instantiatedObject = Instantiate(grenade, barrelEnd.transform.position, barrelEnd.transform.rotation);
            ////Get the grenade RB
            //Rigidbody rb = instantiatedObject.GetComponent<Rigidbody>();
            ////Give the grenade some velocity
            //rb.velocity = instantiatedObject.transform.forward * grenadeSpeed;


        }
    }
}
