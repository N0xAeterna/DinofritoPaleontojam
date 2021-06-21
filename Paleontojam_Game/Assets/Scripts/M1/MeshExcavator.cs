using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshExcavator : MonoBehaviour
{
    [SerializeField]
    Mesh mesh;
    [SerializeField]
    float maxDepth;
    float[] depth;
    public bool PushMeshAlongVerts(int triangleIndex, Mesh inputMesh, Vector3 hitNormal, float ToolStrength, Transform meshTransform, MeshCollider copyCollider, bool excavating)
    {
        
        mesh = inputMesh;
        if (depth == null)
        {
            depth = new float[mesh.vertices.Length];
        }
        hitNormal = meshTransform.rotation * hitNormal;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        Vector3 p0 = vertices[triangles[triangleIndex * 3 + 0]];
        Vector3 p1 = vertices[triangles[triangleIndex * 3 + 1]];
        Vector3 p2 = vertices[triangles[triangleIndex * 3 + 2]];

        float delta = Time.deltaTime;
        if (excavating)
        {
            bool canExcavate;
            canExcavate = true;
            if (depth[triangles[triangleIndex * 3 + 0]] < maxDepth)
                vertices[triangles[triangleIndex * 3 + 0]] += hitNormal * ToolStrength * delta;
            else
                canExcavate = false;
            if (depth[triangles[triangleIndex * 3 + 1]] < maxDepth)
                vertices[triangles[triangleIndex * 3 + 1]] += hitNormal * ToolStrength * delta;
            else
                canExcavate = false;
            if (depth[triangles[triangleIndex * 3 + 2]] < maxDepth)
                vertices[triangles[triangleIndex * 3 + 2]] += hitNormal * ToolStrength * delta;
            else
                canExcavate = false;
            if (canExcavate == false)
                return false;
            else
            {
                depth[triangles[triangleIndex * 3 + 0]] += ToolStrength * delta;
                depth[triangles[triangleIndex * 3 + 1]] += ToolStrength * delta;
                depth[triangles[triangleIndex * 3 + 2]] += ToolStrength * delta;
                mesh.vertices = vertices;
                copyCollider.sharedMesh = mesh;
            }
        }
        return true;
        

    }
}
