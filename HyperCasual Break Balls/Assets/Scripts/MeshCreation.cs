using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCreation : MonoBehaviour
{
    protected MeshFilter meshFilter;
    protected Mesh mesh;

    private void Start()
    {
        mesh = new Mesh();
        mesh.name = "My Mesh";

        mesh.vertices = GenerateVerts();
        mesh.triangles = GenerateTris();

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }

    private int[] GenerateTris()
    {
        return new int[]
        {
            //bottom
            1, 0, 2,
            2, 0, 3,

            //top
            4, 5, 6,
            4, 6, 7,

            //left
            0, 3, 7,
            0, 4, 7,
        };
    }

    private Vector3[] GenerateVerts()
    {
        return new Vector3[]
        {
           //bottom
           new Vector3(-1, 0, 1),
           new Vector3(1, 0, 1),
           new Vector3(1, 0, -1),
           new Vector3(-1, 0, -1),

           //top
           new Vector3(-1, 2, 1),
           new Vector3(1, 2, 1),
           new Vector3(1, 2, -1),
           new Vector3(-1, 2, -1),

           //front
           new Vector3(1, 0, 1),
           new Vector3(1, 0, -1),
           new Vector3(1, 2, 1),
           new Vector3(1, 2, -1),

           //left
           new Vector3(-1, 0, 1),
           new Vector3(-1, 0, -1),
           new Vector3(-1, 2, 1),
           new Vector3(-1, 2, -1),

           //right
           new Vector3(1, 0, -1),
           new Vector3(-1, 0, -1),
           new Vector3(-1, 2, 1),
           new Vector3(-1, 2, -1),

           //back
           new Vector3(1, 0, -1),
           new Vector3(-1, 0, -1),
           new Vector3(1, 2, -1),
           new Vector3(-1, 2, -1),

           //front
           new Vector3(1, 0, 1),
           new Vector3(-1, 0, 1),
           new Vector3(1, 2, 1),
           new Vector3(-1, 2, 1),
        };
    }
}
