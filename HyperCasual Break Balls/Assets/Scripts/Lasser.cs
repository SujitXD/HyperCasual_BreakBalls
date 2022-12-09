using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Lasser : MonoBehaviour
{
    public Transform LasserEffect;
    Vector3 contactPoint;
    public float distance=1f;
    // Start is called before the first frame update
    void Start()
    {
        LasserEffect = transform.parent.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        LasserEffect.localScale =  new Vector3(distance, 1f, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball"))
        {
            contactPoint = other.ClosestPoint(transform.position);
            distance = Vector3.Distance(LasserEffect.position, contactPoint) * 0.05f;
        }
        else if (other.CompareTag("Ball"))
        {
            Destroy(other.gameObject);

        }
    }
    private void OnTriggerExit(Collider other)
    {
       
        if (!other.CompareTag("Ball"))
        {
            //LasserEffect.DOScaleX(1f, 1f);
            distance = 1f;
        }
    }

   
}
