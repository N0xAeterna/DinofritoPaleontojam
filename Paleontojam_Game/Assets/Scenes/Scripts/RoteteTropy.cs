using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoteteTropy : MonoBehaviour
{ [SerializeField]private float VelocityX;
   [SerializeField] private float VelocityY;

    private float Yaw;
    private float Pitch;
    private bool WinTheGame;

    void Start()
    {
        WinTheGame = true;
    }

    // Update is called once per frame
    void Update()

    {

        if (Input.GetMouseButton(0)&& WinTheGame)
        {
            Yaw += VelocityX * Input.GetAxis("Mouse X");
            Pitch -= VelocityY * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(Pitch, Yaw, 0.0f);
        }
     
    }
}
