using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform cameraParent;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            cameraParent.Rotate(new Vector3(0,50*Time.deltaTime,0));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            cameraParent.Rotate(new Vector3(0, -50 * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.position+= transform.forward * 10 * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.position += -transform.forward * 10 * Time.deltaTime;
        }
    }
}
