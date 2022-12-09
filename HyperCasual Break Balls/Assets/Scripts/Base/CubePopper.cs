using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubePopper : ObstacleManager
{
    Transform characterTarget;
    public float triggerDistance = 5f;
    public float popRotSpeed = 20f;
    public float popOffset = 2f;
    public float popTime = 2f;
    bool isPopped;
    bool stop;

    Sequence seq;

    public bool sidePopper, verticalPopper;
    Rigidbody rb;

    private void Start()
    {
        characterTarget = CharacterController.instance.transform;

        if (verticalPopper)
            rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!isPopped && Vector3.Distance(transform.position, characterTarget.position) < triggerDistance)
            Pop();
        
        if(sidePopper)
        {
            if (isPopped && !stop)
                transform.Rotate(0f, 0f, popRotSpeed * Time.deltaTime);
        }
    }

    private void Pop()
    {
        isPopped = true;

        Sequence sequence = DOTween.Sequence();
        if (sidePopper)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            sequence.Append(transform.DOJump(characterTarget.position + new Vector3(0f, 0f, popOffset), 2f, 1, popTime).SetEase(Ease.Linear));
        }
        else if (verticalPopper)
        {
            sequence.Append(transform.DOLocalMoveY(popOffset + transform.localPosition.y, popTime).SetEase(Ease.Linear));
            sequence.OnComplete(() => { rb.isKinematic = false; });
        }

        seq = sequence;
    }

    private void StopPop()
    {
        if(!stop)
        {
            stop = true;
            seq.Kill();
        }
    }

    public override void Explode(Collider hit, Vector3 explosionPos, float collisionMagnitude)
    {
        base.Explode(hit, explosionPos, collisionMagnitude);

        StopPop();

        if (hit.attachedRigidbody)
        {
            hit.attachedRigidbody.isKinematic = false;
            hit.attachedRigidbody.AddExplosionForce(power * collisionMagnitude, explosionPos, radius, upwards);
        }
    }
}
