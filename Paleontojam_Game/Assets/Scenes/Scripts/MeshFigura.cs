using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshFigura : MonoBehaviour
{
    [SerializeField] private Texture2D imagen;
  

   public Mesh mesh;
    int verticeActual;
    private void Start()
    {
      
      
     
      
        mesh = GetComponent<MeshFilter>().mesh;

        Vector3[] vertices = mesh.vertices;

        print(imagen.width);
        for (int i = 0; i < imagen.width; i++)
        {
            for (int j = 0; j < imagen.height; j++)
            {
                int verticeActual = (i + imagen.width * j);
         
                vertices[verticeActual].y = imagen.GetPixel(i, j).r;
          

               

            }
          

        }

        mesh.vertices = vertices;
    }
    // Update is called once per frame
    void Update()
    {


      
        
    }
}
