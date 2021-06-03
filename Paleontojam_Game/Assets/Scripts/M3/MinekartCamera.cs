using UnityEngine;

public class MinekartCamera : MonoBehaviour
{
    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newPosition = new Vector3();
        newPosition.x = player.transform.position.x + 4;
        newPosition.y = player.transform.position.y + 5;
        newPosition.z = player.transform.position.z - 7.86f;

        transform.position = newPosition;
    }
}
