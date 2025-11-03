using UnityEngine;

public class ShoulderMovement : MonoBehaviour
{
    public GameObject target;
    public bool leftSide;

    //  Store our min/max rotation angles
    //  Store in the -180 to +180 range for easier clamping
    private float minAngle;
    private float maxAngle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (leftSide)
        {
            minAngle = -90.0f;
            maxAngle = 10.0f;
        }
        else
        {
            minAngle = -10.0f;
            maxAngle = 90.0f;
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform.position);
        //  Get Euler Angles
        Vector3 angles = transform.localRotation.eulerAngles;

        //  Normalize the y angle to -180 to +180 range

        float currentY = angles.y;
        if (currentY > 180)
        {
            currentY -= 360;
        }

        float clampedY = Mathf.Clamp(currentY, minAngle, maxAngle);

        transform.localRotation = Quaternion.Euler(angles.x, clampedY, angles.z);


    }
}
