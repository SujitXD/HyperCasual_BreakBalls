using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collateral : MonoBehaviour
{
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.collider.CompareTag("Ground"))
        {
            rb.isKinematic = false;
        }
    }
}
