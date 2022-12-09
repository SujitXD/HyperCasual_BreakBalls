using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LasserType : ObstacleManager
{
    [SerializeField] protected List<Transform> obstaclePieces;
    [SerializeField] protected List<GameObject> lasserLine;
    [SerializeField] private Material brokedCrystalMat;

    public override void Shatter(Vector3 hitPoint, float _magnitude)
    {
        base.Shatter(hitPoint, _magnitude);

        if (isShattered )
            return;

        
        foreach(GameObject line in lasserLine)
        {
            line.gameObject.SetActive(false);
        }

       

        isShattered = true;
    }

    public override void Explode(Collider hit, Vector3 explosionPos, float collisionMagnitude)
    {
        base.Explode(hit, explosionPos, collisionMagnitude);

        if (hit.attachedRigidbody)
        {
            hit.attachedRigidbody.isKinematic = false;
            hit.attachedRigidbody.AddExplosionForce(power * collisionMagnitude, explosionPos, radius, upwards);
        }
    }
}
