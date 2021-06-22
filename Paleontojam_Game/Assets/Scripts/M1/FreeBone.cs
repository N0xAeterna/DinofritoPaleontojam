using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeBone : MonoBehaviour
{
    [SerializeField]
    bool buried;
    [SerializeField]
    LayerMask mask;
    Rigidbody rb;


    private void Start()
    {
        buried = true;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 1, mask))
        {
            Debug.DrawRay(ray.origin, ray.direction);
            if (hitInfo.transform.gameObject.CompareTag("Terreno") && buried)
            {
                buried = false;
                rb.isKinematic = false;
                transform.position += Vector3.up * 1f;
                rb.AddForce(Vector3.up * 5);
            }
        }
    }
}
