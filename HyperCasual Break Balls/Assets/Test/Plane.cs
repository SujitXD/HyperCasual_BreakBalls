using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public Transform cube;

    private void Update()
    {
        for (int i = 0; i < cube.childCount; i++)
        {
            Vector3 pos = transform.TransformDirection(Vector3.up);
            Vector3 toCube = cube.GetChild(i).position - transform.position;

            if (Vector3.Dot(pos, toCube) > 0)
            {
                print("Vertex " + (i + 1).ToString() +" Facing!");
            }
            else
            {
                print("Vertex " + (i + 1).ToString() + " Not Facing!");
            }
        }
        
    }
}
