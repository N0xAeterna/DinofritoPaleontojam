using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorFollow : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    LayerMask layermask;
    // Start is called before the first frame update
    void Start()
    {
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
        }

    }
}
