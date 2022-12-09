using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    public Transform splittingPlane;
    Mesh mesh;

    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;

        Vector3[] vertices = mesh.vertices;
        print(vertices.Length);

        for (int i = 0; i < vertices.Length; i++)
        {
            GameObject vertex = new GameObject("Vertex " + (i + 1).ToString());
            
            Matrix4x4 matrix = transform.localToWorldMatrix;
            vertex.transform.position = matrix.MultiplyPoint3x4(vertices[i]);
            vertex.transform.SetParent(transform);

            /*Vector3 up = splittingPlane.TransformDirection(Vector3.up);
            Vector3 toCube = vertex.transform.position - splittingPlane.position;

            float dot = Vector3.Dot(up, toCube);

            if (dot > 0)
                print("Vertex " + (i + 1).ToString() + " is above plane! with y " + dot);
            else
                print("Vertex " + (i + 1).ToString() + " is below plane! with y " + dot);*/
        }
    }
}
