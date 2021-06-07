using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMesh : MonoBehaviour
{
    [SerializeField]private bool PerlingNoiseTerrain;
    MeshCollider meshC;
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    [SerializeField]private int Xsize = 20;
    [SerializeField]private int Zsize = 20;
    void Start()
    {
        mesh = new Mesh();
        meshC = GetComponent<MeshCollider>();
        GetComponent<MeshFilter>().mesh=mesh;
        CreateShape();
        Updatemesh();
        meshRecalculateNormals();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateShape()
    {
        vertices = new Vector3[(Xsize + 1) * (Zsize + 1)];
      
        for (int i = 0 , z = 0; z <= Zsize; z++)
        {
            for (int x = 0; x <= Xsize; x++)
            {
          
               
                    float y = Mathf.PerlinNoise(x * .5f, z * .3f) * 2f;
             vertices[i]= (PerlingNoiseTerrain)? vertices[i] = new Vector3(x, y, z):  vertices[i] = new Vector3(x, 1.5f, z);


                i++;
            }
        }
        triangles = new int[Xsize * Zsize * 6];
        int vert = 0;
        int tris = 0;
        for (int z = 0; z < Zsize; z++)
        {
            for (int x = 0; x < Xsize; x++)
            {

                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + Xsize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + Xsize + 1;
                triangles[tris + 5] = vert + Xsize + 2;
                vert++;
                tris += 6;

            }
            vert++;
        }
      
    
    }
    void Updatemesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        meshC.sharedMesh=mesh;
       
       
    }
    void meshRecalculateNormals()
    {

    }
 
}
