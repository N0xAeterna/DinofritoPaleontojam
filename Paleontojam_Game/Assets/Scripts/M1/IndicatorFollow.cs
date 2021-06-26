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
    //Aca todo las variables que agregue para que pase lo que pasa en la escena 1 :u
    [SerializeField] private List<GameObject> huesos;
    [SerializeField] private Mesh mesh;
  
    private int getid;
  

    public GameObject objetivo;
    private int count;
    Ray rayToCameraPos;
    private bool oneTime;
    void Start()
    {
        count = 0;
        excavator = gameObject.GetComponent<MeshExcavator>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (count >= 4)
        {
            SaveAndLoad.instancia.GuardarNumErrores(SaveAndLoad.instancia.CargarNumErrores().NumOfLose + 1);
            ButtonManager.instancia.DesactiveInStart1[0].SetActive(true);
        }
        CursorToWorldPosition();
    }


    void CursorToWorldPosition()
    {
        rayToCameraPos = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        RaycastHit hitOb;

        bool TouchElemengt = Physics.Raycast(rayToCameraPos, out hitOb, ToolsScript.instancia.Distancia1, ToolsScript.instancia.layerObject);
        if (Physics.Raycast(rayToCameraPos, out hitInfo, 100, layermask))
        {
            transform.position = hitInfo.point;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            if (excavator != null && hitInfo.rigidbody != null)
            {
                excavatingSpeed += Input.mouseScrollDelta.y * 0.1f;
                excavatingSpeed = Mathf.Clamp(excavatingSpeed, 0, 10);
                bool excavating = false;
                bool canExcavate = true;
                excavating = Input.GetMouseButton(0);

                canExcavate = excavator.PushMeshAlongVerts(hitInfo.triangleIndex, hitInfo.rigidbody.gameObject.GetComponent<MeshFilter>().mesh, hitInfo.normal, excavatingSpeed * Time.deltaTime, hitInfo.rigidbody.gameObject.transform, colliderCopy, excavating);
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

        Vector3[] vertices = mesh.vertices;
        if (oneTime)
        {
            count += 1;
            print(count);
            oneTime = false;
        }
        if (TouchElemengt)
        {
            print("E tocado un hueso");
            if (!ToolsScript.instancia.CanGive)
            {
                for (int i = 0; i < vertices.Length; i++)
                {
                    float distancia = Vector3.Distance(huesos[getid].transform.position,(vertices[i]));//aca detecta distancia entre lso huesos

                    if (distancia < 1f)
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            oneTime = true;// aca si toca el hueso cuando 
                        }
                    
                    }
                }



            }
         

        }



    }
    public int IdHueso(int i)
    {
        return i;
    }
   
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position);
    } 
}
