using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : ObstacleManager
{
    //[SerializeField] private float shatterRange = 0.5f;

    public override void Shatter(Vector3 hitPoint, float _magnitude)
    {
        base.Shatter(hitPoint, _magnitude);

        /*foreach (Transform piece in obstaclePieces)
        {
            if(Vector3.Distance(piece.position, hitPoint) < shatterRange)
            {
                if(piece.GetComponent<Rigidbody>().isKinematic)
                    piece.GetComponent<Rigidbody>().isKinematic = false;
            }
        }*/
    }
    public override void Explode(Collider hit, Vector3 explosionPos, float collisionMagnitude)
    {
        base.Explode(hit, explosionPos, collisionMagnitude);

        if (hit.attachedRigidbody)
        {
            AudioManager.instance.PlayOneShot("GlassPaneBreak");

            hit.GetComponent<MeshRenderer>().enabled = false;
            hit.GetComponent<BoxCollider>().enabled = false;

            // hit.attachedRigidbody.isKinematic = false;
            foreach (Transform child in hit.transform)
            {
                child.gameObject.SetActive(true);
                child.GetComponent<Rigidbody>().AddExplosionForce(power * collisionMagnitude, explosionPos, radius, upwards);
            }
        }
    }
}
