using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Placement : MonoBehaviour
{
    public GameObject objPrefab;
    public float startPos, offset, objCount;
    public float yMin, yMax;

    public void PlaceOBJ()
    {
        for (int i = 0; i < objCount; i++)
        {
             GameObject obj = Instantiate(objPrefab, transform);
             obj.transform.localPosition = new Vector3(5.4f, Random.Range(yMin, yMax), startPos + (offset * i));
             obj.name = "Place Arrow";
        }
    }
}

[CustomEditor(typeof(Placement))]
public class PlacementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Placement nameChanger = (Placement)target;

        if (GUILayout.Button("Place"))
        {
            nameChanger.PlaceOBJ();
        }
    }
}