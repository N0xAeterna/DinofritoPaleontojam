using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MeshScript : MonoBehaviour
{
    MeshCollider meshc;
    [SerializeField] private float TimeToDisapear;

    public AnimationCurve CurveIntensity;
    [SerializeField] private float Curve;
    [SerializeField] private GameObject Ganaste;
    private float CountToDestroyObject;
    public Mesh mesh;
    public GameObject objetivo;
    private bool TomarObjeto;
    [SerializeField] private List<GameObject> huesos;
    private float DistanciaEntreHuesos;
    private int getid;
    private int NumHuesosTuWin;
    private bool oneTime;
    private bool CanADD;
    private void Start()
    {
        CanADD = true;
       string path = Application.dataPath + "/editable/Errors.json";
        meshc = GetComponent<MeshCollider>();
        ButtonManager.instancia.DesactiveInStart1[2].SetActive(true);
        if (!File.Exists(path))
        {
            SaveAndLoad.instancia.GuardarNumErrores(0);
        }
    
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DialogoManager.Instancia.MostrarSiguienteOracion();
            if(DialogoManager.Instancia.Finalizado){
                Destroy(ButtonManager.instancia.DesactiveInStart1[2]);
            }
        }
        if (NumHuesosTuWin > huesos.Count - 1)
        {
          
                ButtonManager.instancia.DesactiveInStart1[1].SetActive(true);
          
          
        }
        mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        if (Input.GetMouseButton(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            RaycastHit hitOb;


            meshc.sharedMesh = mesh;
            bool isTouching = Physics.Raycast(ray, out hit, ToolsScript.instancia.Distancia1, ToolsScript.instancia.layer);





            bool TouchElemengt = Physics.Raycast(ray, out hitOb, ToolsScript.instancia.Distancia1, ToolsScript.instancia.layerObject);
            for (int i = 0; i < huesos.Count; i++)
            {
                DistanciaEntreHuesos = Vector3.Distance(hit.point, (huesos[i].transform.position));

                if (DistanciaEntreHuesos < 1f)
                {
                    getid = IdHueso(i);
                }
            }

            if (isTouching)
            {

                if (!ToolsScript.instancia.CanGive)
                {


                    for (var i = 0; i < vertices.Length; i++)
                    {
                        float distancia = Vector3.Distance(hit.point, transform.TransformPoint(vertices[i]));
                        if (Mathf.Abs(distancia) < 1.2f)
                        {
                            vertices[i] = new Vector3(vertices[i].x, vertices[i].y - (ToolsScript.instancia.Herramienta1 * CurveIntensity.Evaluate(distancia)), vertices[i].z);
                        }

                    }

                }
            }
            if (TouchElemengt)
            {
                print("E tocado un hueso");
                if (ToolsScript.instancia.CanGive)
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


        mesh.vertices = vertices;
       
        if (TomarObjeto)
        {

            float Distancia = (huesos[getid].transform.position - objetivo.transform.position).magnitude;


            if (Distancia > 3)
            {
                huesos[getid].transform.position = Vector3.Lerp(huesos[getid].transform.position, objetivo.transform.position, 5 * Time.deltaTime);

            }
            else
            {
                TomarObjeto = false;
                if (!oneTime)
                {
                    NumHuesosTuWin += 1;
                    StartCoroutine(getHueso());
                }

                print(NumHuesosTuWin);
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
        for (int i = 0; i < huesos.Count; i++)
        {
            Gizmos.DrawWireSphere(huesos[i].transform.position, 1);
        }
    }

}
