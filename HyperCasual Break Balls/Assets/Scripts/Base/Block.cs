using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : ObstacleManager
{
    [SerializeField] private bool isMoving;
    [SerializeField] protected List<Transform> obstaclePieces;
    Transform blockGfx;
    public List<Transform> wayPoints;
    public float speed;

    public bool slab;

    public bool reverse;
    int index;

    private void Start()
    {
        blockGfx = transform.GetChild(0);

        if (reverse)
            index = wayPoints.Count - 1;
    }

    private async void Update()
    {
        if (isMoving)
        {
            if(Vector3.Distance(blockGfx.localPosition, wayPoints[index].localPosition) > 0.1f)
            {
                blockGfx.localPosition = Vector3.MoveTowards(blockGfx.localPosition, wayPoints[index].localPosition, speed * Time.deltaTime);
            }
            else
            {
                if (!reverse)
                {
                    if (index == wayPoints.Count - 1)
                    {
                        reverse = true;
                        return;
                    }

                    index++;
                }

                if(reverse)
                {
                    if (index == 0)
                    {
                        reverse = false;
                        return;
                    }

                    index--;
                }

                isMoving = false;
                await GameManager.instance.WaitForSomeTime(0.2f);
                isMoving = true;
            }
        }
    }

    public override void Shatter(Vector3 hitPoint, float _magnitude)
    {
        base.Shatter(hitPoint, _magnitude);
    }
    public override void Explode(Collider hit, Vector3 explosionPos, float collisionMagnitude)
    {
        base.Explode(hit, explosionPos, collisionMagnitude);

        if (hit.attachedRigidbody)
        {
            hit.attachedRigidbody.isKinematic = false;
            hit.attachedRigidbody.gameObject.tag = "Untagged";
            hit.attachedRigidbody.AddExplosionForce(power * collisionMagnitude, explosionPos, radius, upwards);

            if (slab)
            {
                
                foreach (Transform item in obstaclePieces)
                {
                    if (item.localPosition.y > hit.gameObject.transform.localPosition.y)
                    {
                        item.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                        item.gameObject.tag = "Untagged";
                    }

                }
            }
            if (hit.name.Contains("Link"))
            {
                hit.transform.parent.gameObject.SetActive(false);
            }


        }
    }
}
