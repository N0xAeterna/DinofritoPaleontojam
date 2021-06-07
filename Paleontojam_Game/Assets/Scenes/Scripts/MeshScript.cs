using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeshScript : MonoBehaviour
{
    MeshCollider meshc;
    [SerializeField]private float TimeToDisapear;
   
    public AnimationCurve CurveIntensity;
    [SerializeField]private float Curve;
    private float CountToDestroyObject;
   public Mesh mesh;
    private void Start()
    {

        meshc = GetComponent<MeshCollider>();
      
    }
    void Update()
    {
     
         mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
    
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
          
            RaycastHit hit;
            RaycastHit hitOb;

            meshc.sharedMesh = mesh;
            bool isTouching = Physics.Raycast(ray,out hit,ToolsScript.instancia.Distancia1,ToolsScript.instancia.layer);
            print(isTouching);
           
         
            
           
            bool TouchElemengt = Physics.Raycast(ray, out hitOb, ToolsScript.instancia.Distancia1, ToolsScript.instancia.layerObject);
            if (isTouching)
            {
                print("Estoy Tocando");
              
                for (var i = 0; i < vertices.Length; i++)
                {
                    float distancia = Vector3.Distance(hit.point,transform.TransformPoint( vertices[i]));
                  


                    if (Mathf.Abs(distancia) < 1.2f)
                    {

                        vertices[i] = new Vector3(vertices[i].x, vertices[i].y - (ToolsScript.instancia.Herramienta1*CurveIntensity.Evaluate(distancia)), vertices[i].z);
                    }





                }
            }
            if (TouchElemengt)
            {
                CountToDestroyObject += Time.deltaTime;
                switch (ToolsScript.instancia.Herramienta1)
                {
                    case 0.03f:
                        if (CountToDestroyObject > 0.1)
                        {
                            ButtonManager.instancia.DesactiveInStart1[0].SetActive(true);
                            CountToDestroyObject = 0;
                        }; break;
                    case 0.009f:
                        if (CountToDestroyObject > 0.8)
                        {
                            ButtonManager.instancia.DesactiveInStart1[0].SetActive(true);
                            CountToDestroyObject = 0;
                        }; break;
                    case 0.003f:
                        if (CountToDestroyObject > 1.8)
                        {
                            ButtonManager.instancia.DesactiveInStart1[0].SetActive(true);
                            CountToDestroyObject = 0;
                        }
                        ; break;
                }
              
            }
        }
    

        mesh.vertices = vertices;
    }
  
}
