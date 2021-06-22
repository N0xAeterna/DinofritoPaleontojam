using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IndicatorFollow : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    LayerMask layermask;
    MeshExcavator excavator;
    [SerializeField]
    MeshCollider colliderCopy;
    [SerializeField]
    ParticleSystem rocks;
    [SerializeField]
    float excavatingSpeed;
    // Start is called before the first frame update
    void Start()
    {
        excavator = gameObject.GetComponent<MeshExcavator>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        CursorToWorldPosition();
    }


    void CursorToWorldPosition()
    {
        Ray rayToCameraPos = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        
        if (Physics.Raycast(rayToCameraPos, out hitInfo, 100, layermask))
        {
            transform.position = hitInfo.point;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            if (excavator != null && hitInfo.rigidbody != null)
            {
                excavatingSpeed += Input.mouseScrollDelta.y*0.1f;
                excavatingSpeed = Mathf.Clamp(excavatingSpeed, 0, 10);
                bool excavating = false;
                bool canExcavate = true;
                excavating = Input.GetMouseButton(0);
                
                canExcavate = excavator.PushMeshAlongVerts(hitInfo.triangleIndex, hitInfo.rigidbody.gameObject.GetComponent<MeshFilter>().mesh, hitInfo.normal, excavatingSpeed*Time.deltaTime, hitInfo.rigidbody.gameObject.transform, colliderCopy, excavating);
                if (excavating && canExcavate)
                {
                    rocks.emissionRate = 50 * (excavatingSpeed);
                    rocks.startSpeed = 30 * (excavatingSpeed / 10);
                }
                else
                {
                    rocks.emissionRate = 0;
                }
            }
        }

        

    }
}
