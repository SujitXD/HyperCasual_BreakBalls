using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] protected float magnitude, radius, power, upwards;
    public bool isShattered;

    public virtual void Shatter(Vector3 hitPoint, float collisionMagnitude)
    {
        Vector3 explosionPos = hitPoint;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            if (!hit.CompareTag("BGWall"))
            {
                Explode(hit, explosionPos, collisionMagnitude);
            }

        }

        AudioManager.instance.PlayOneShot("GlassPaneBreak");
    }

    public virtual void Explode(Collider _hit, Vector3 hitPoint, float collisionMagnitude)
    {

    }
}
