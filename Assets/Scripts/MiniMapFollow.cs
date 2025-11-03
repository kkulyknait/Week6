using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float height = 50.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(player == null)
        {
            player = GameObject.Find("Player").transform;
            height = transform.position.y;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(player == null)
        {
            return;
        }
        transform.position = new Vector3(player.position.x, height, player.position.z);
    }
}
