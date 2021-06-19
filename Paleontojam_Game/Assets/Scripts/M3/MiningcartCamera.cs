using UnityEngine;

/// <summary>
/// Player's main camera
/// </summary>
public class MiningcartCamera : MonoBehaviour
{
    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// Updates camera position to properly follow the player
    /// </summary>
    void FixedUpdate()
    {
        Vector3 newPosition = new Vector3();
        newPosition.x = player.transform.position.x + 4;
        newPosition.y = player.transform.position.y + 5;
        newPosition.z = player.transform.position.z - 15f;

        transform.position = newPosition;
    }
}
