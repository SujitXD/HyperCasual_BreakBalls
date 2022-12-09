using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody hook;
    public GameObject linkPrefab;
    public Weight weight;
    public Vector3 weightAnchor;

    public int links = 5;

    private void Start()
    {
        GenerateRope();
    }

    private void GenerateRope()
    {
        Rigidbody prevRb = hook;

        for (int i = 0; i < links; i++)
        {
            GameObject link = Instantiate(linkPrefab, transform);
            HingeJoint joint = link.GetComponent<HingeJoint>();
            joint.connectedBody = prevRb;
            prevRb = link.GetComponent<Rigidbody>();

            if (i == links - 1)
                weight.ConnectRopeEnd(link.GetComponent<Rigidbody>(), weightAnchor);
            else
                prevRb = link.GetComponent<Rigidbody>();
        }
    }
}
