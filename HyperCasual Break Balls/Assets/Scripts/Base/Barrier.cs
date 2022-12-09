using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : ObstacleManager
{
    public LayerMask whatIsSupport;
    public Vector3 rayDirection;
    public float detectionRange;

    Transform leftSideSupport, rightSideSupport;

    public List<Barrier> cornerSlabs;


    private void Update()
    {
        /*Debug.DrawRay(transform.position, -rayDirection * detectionRange, Color.red);

        Debug.DrawRay(transform.position, rayDirection * detectionRange, Color.red);*/

        if (!isShattered)
        {
            bool endSlabsShattered = false;

            for (int i = 0; i < cornerSlabs.Count; i++)
            {
                if (!cornerSlabs[i].isShattered)
                {
                    endSlabsShattered = false;
                    break;
                }
                else
                {
                    endSlabsShattered = true;
                }
            }

            bool supportSlabShattered = false;

            if(leftSideSupport == null && rightSideSupport != null)
            {
                Barrier nextSlab = rightSideSupport.GetComponent<Barrier>();
               
                if (nextSlab == null) return;

                for (int i = 0; i < 15; i++)
                {
                    if (nextSlab.rightSideSupport != null)
                    {
                        nextSlab = nextSlab.rightSideSupport.GetComponent<Barrier>();

                        if (nextSlab == null) break;
                    }
                    else
                    {
                        supportSlabShattered = true;
                        break;
                    }
                }
            }
            else if(rightSideSupport == null && leftSideSupport != null)
            {
                Barrier prevSlab = leftSideSupport.GetComponent<Barrier>();

                if (prevSlab == null) return;

                for (int i = 0; i < 15; i++)
                {
                    if (prevSlab.leftSideSupport != null)
                    {
                        prevSlab = prevSlab.leftSideSupport.GetComponent<Barrier>();

                        if (prevSlab == null) break;
                    }
                    else
                    {
                        supportSlabShattered = true;
                        break;
                    }
                }
            }

            if (!IsSupported() || endSlabsShattered || supportSlabShattered)
            {
                isShattered = true;
                transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Rigidbody>().isKinematic = false;
                //transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
                
                transform.GetComponent<Collider>().enabled = false;

                /*Transform slab = transform.GetChild(0);

                foreach (Transform child in slab)
                {
                    child.transform.gameObject.SetActive(true);
                }*/
            }
        }
    }

    public bool IsSupported()
    {
        RaycastHit hitLeft, hitRight;

        Debug.DrawRay(transform.position, -rayDirection * detectionRange, Color.red);
        if (Physics.Raycast(transform.position, -rayDirection, out hitLeft, detectionRange, whatIsSupport)) 
        {
            if(hitLeft.collider != transform)
            {
                leftSideSupport = hitLeft.collider.transform;
            }
        }
        else
        {
            leftSideSupport = null;
        }

        Debug.DrawRay(transform.position, rayDirection * detectionRange, Color.red);
        if (Physics.Raycast(transform.position, rayDirection, out hitRight, detectionRange, whatIsSupport))
        {
            if (hitRight.collider != transform)
            {
                rightSideSupport = hitRight.collider.transform;
            }
        }
        else
        {
            rightSideSupport = null;
        }

        if (leftSideSupport == null && rightSideSupport == null)
        {
            return false;
        }

        return true;
    }

    public override void Shatter(Vector3 hitPoint, float _magnitude)
    {
        base.Shatter(hitPoint, _magnitude);
    }

    public override void Explode(Collider hit, Vector3 explosionPos, float collisionMagnitude)
    {
        base.Explode(hit, explosionPos, collisionMagnitude);

        if (hit.transform == transform)
        {
            transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);

            foreach (Transform child in transform.GetChild(0))
            {
                child.transform.gameObject.SetActive(true);
                //child.GetComponent<Rigidbody>().isKinematic = false;
                child.GetComponent<Rigidbody>().AddExplosionForce(power * collisionMagnitude, explosionPos, radius, upwards);
            }

            transform.GetComponent<Collider>().enabled = false;
            isShattered = true;
            leftSideSupport = rightSideSupport = null;
        }
    }
}
