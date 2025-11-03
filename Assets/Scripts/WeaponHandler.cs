using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHandler : MonoBehaviour
{
    private InputSystem_Actions playerInput;  // reference to our input class

    [SerializeField] private GameObject paintball;
    [SerializeField] private GameObject barrelEnd;

    private float paintballSpeed = 20.0f;
    private float fireRate = 0.5f;
    private float timeStamp = -1.0f;

    GameObject[] bulletPool;  //  an array to hold our paintballs
    int index;  //  keep track of which paintballs to use next

    private void Awake()
    {
        //  When the "Fire" action is performed, call the Fire method
        playerInput = new InputSystem_Actions();
        playerInput.Player.Attack.performed += ContextMenu => Fire();
    }
    private void OnEnable()
    {
        playerInput.Player.Enable();
    }
    private void OnDisable()
    {
        playerInput.Player.Disable();
    }
    private void Fire()
    {
        if (Time.time > timeStamp + fireRate)
        {
            timeStamp = Time.time;

            //  New firing logic without instantiate
            //  Get the next paintball from the pool
            GameObject currentBullet = bulletPool[index];

            //  Move it to the barrel end position and rotation
            currentBullet.transform.position = barrelEnd.transform.position;
            currentBullet.transform.rotation = barrelEnd.transform.rotation;

            //  Activate Paintball
            currentBullet.SetActive(true);

            //  Fire it by setting velocity
            currentBullet.GetComponent<Rigidbody>().linearVelocity =
            currentBullet.transform.forward * paintballSpeed;

            // Move forward in the pool
            index++;

            //  If we reach the end of the pool, loop back to the beginning
            if (index >= bulletPool.Length)
            {
                index = 0; // cycle back to first paintball
            }
         }
    }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       //  Create the pool
        index = 0;  //beginning of the pool
        bulletPool = new GameObject[10];  //create array for 10 paintballs
        for (int i = 0; i < bulletPool.Length; i++)
        {
            //Instantiate paintball and store in array
            bulletPool[i] = Instantiate(paintball, Vector3.zero, Quaternion.identity);
            //deactivate it, hiding it from the scene
            bulletPool[i].SetActive(false);
        }
    }

}
