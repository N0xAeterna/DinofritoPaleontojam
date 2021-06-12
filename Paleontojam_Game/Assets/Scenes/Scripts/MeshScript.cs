using System.Collections;
using System.Collections.Generic;
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
    public DialogoTrigger dialogoTrigger;
  
    private void Start()
    {

        meshc = GetComponent<MeshCollider>();
        ButtonManager.instancia.DesactiveInStart1[2].SetActive(true);
        dialogoTrigger.IniciarDialogo();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DialogoManager.instancia.MostrarSiguienteOracion();
            if(DialogoManager.instancia.Finalizado){
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
                if (ToolsScript.instancia.CanGive)
                {
                    TomarObjeto = true;


                }
                else
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
                            if (CountToDestroyObject > 1)
                            {
                                ButtonManager.instancia.DesactiveInStart1[0].SetActive(true);
                                CountToDestroyObject = 0;

                            }; break;
                        case 0.003f:
                            if (CountToDestroyObject > 2)
                            {
                                ButtonManager.instancia.DesactiveInStart1[0].SetActive(true);
                                CountToDestroyObject = 0;

                            }
                            ; break;
                    }
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

}
