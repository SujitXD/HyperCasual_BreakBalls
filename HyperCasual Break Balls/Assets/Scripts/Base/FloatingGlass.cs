using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloatingGlass : ObstacleManager
{
    Transform characterTarget;
    public float triggerDistance = 5f;
    public float popRotSpeed = 20f;
    public float popOffset = 2f;
    public float popTime = 2f;
    bool isPopped;
    bool stop;

    public bool sidePopper, horizontalPopper, verticalPopper;

    Vector3 startPos;

    private void Start()
    {
        characterTarget = CharacterController.instance.transform;

        startPos = transform.position;
    }
    private void Update()
    {
        if (!isPopped && Vector3.Distance(transform.position, characterTarget.position) < triggerDistance)
            Pop();


        if (isPopped && !stop)
        {
            if (sidePopper)
            {
                transform.Rotate(0f, 0f, popRotSpeed * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, characterTarget.position, popTime * Time.deltaTime);
            }
            else if(horizontalPopper)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPos + new Vector3(popOffset, 0f, 0f), popTime * Time.deltaTime);
            }
            else if (verticalPopper)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPos + new Vector3(0f, popOffset, 0f), popTime * Time.deltaTime);
            }
        }
    }
    private void Pop()
    {
        isPopped = true;
    }

    public override void Explode(Collider hit, Vector3 explosionPos, float collisionMagnitude)
    {
        base.Explode(hit, explosionPos, collisionMagnitude);
        
        stop = true;

        if (hit.attachedRigidbody)
        {
            hit.attachedRigidbody.isKinematic = false;
            hit.attachedRigidbody.AddExplosionForce(power * collisionMagnitude, explosionPos, radius, upwards);
        }

        foreach (Transform child in transform.GetChild(0))
        {
            if (child.GetComponent<Rigidbody>().isKinematic)
            {
                child.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
}
