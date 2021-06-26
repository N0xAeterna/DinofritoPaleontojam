using UnityEngine;

/// <summary>
/// Player's main camera
/// </summary>
public class MiningcartCamera : MonoBehaviour
{
    GameObject player;
    public static bool seguir = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// Updates camera position to properly follow the player
    /// </summary>
    void FixedUpdate()
    {
        if(seguir)
        {
            Vector3 newPosition = new Vector3();
            newPosition.x = player.transform.position.x + 6;
            newPosition.y = player.transform.position.y + 17.93f;
            newPosition.z = player.transform.position.z - 53.96f;

            transform.position = newPosition;
        }
        
    }
}
