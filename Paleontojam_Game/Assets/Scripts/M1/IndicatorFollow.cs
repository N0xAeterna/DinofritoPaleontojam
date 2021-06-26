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
    private float DistanciaEntreHuesos;
    private int getid;
    private int NumHuesosTuWin;
    private bool oneTime;
    private float CountToDestroyObject;

    public GameObject objetivo;
    private bool TomarObjeto;
    private bool CanADD;
    Vector3[] vertices;
    Ray rayToCameraPos;
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

        if (TouchElemengt)
        {
          //  print("E tocado un hueso");
            if (!ToolsScript.instancia.CanGive)
            {
                for (int i = 0; i < vertices.Length; i++)
                {
                    float distancia = Vector3.Distance(huesos[getid].transform.position, transform.TransformPoint(vertices[i]));

                    if (distancia < 3f)
                    {
                        TomarObjeto = true;
                        print("llego");
                    }
                }



            }
            else
            {

                ButtonManager.instancia.DesactiveInStart1[0].SetActive(true);
                SaveAndLoad.instancia.GuardarNumErrores(SaveAndLoad.instancia.CargarNumErrores().NumOfLose + 1);
                print(SaveAndLoad.instancia.CargarNumErrores().NumOfLose);

            }

        }



    }
    public int IdHueso(int i)
    {
        return i;
    }
    IEnumerator getHueso()
    {
        oneTime = true;
        yield return new WaitForSeconds(1);
        oneTime = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position);
    } 
}
