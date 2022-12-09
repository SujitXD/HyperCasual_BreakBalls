using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TallGlassShatter : ObstacleManager
{
    [SerializeField] protected List<Transform> obstaclePieces;
    [SerializeField] private Material brokedCrystalMat;
    public bool slab;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Animator>().enabled = true;
        }
    }
    public override void Shatter(Vector3 hitPoint, float _magnitude)
    {
        base.Shatter(hitPoint, _magnitude);

        if (isShattered )
            return;

        foreach (Transform piece in obstaclePieces)
        {
            piece.GetComponent<MeshRenderer>().material = brokedCrystalMat;
        }

        GameObject hitScoreUI = ObjectPooler.instance.SpawnFromPool("CrystalHitScore", new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z), Quaternion.identity);
        hitScoreUI.transform.GetChild(0).GetComponent<Image>().DOFade(0f, 1f).SetEase(Ease.Linear);
        hitScoreUI.transform.DOMoveY(transform.position.y + 7f, 1f).SetEase(Ease.Linear).OnComplete(() => {
            hitScoreUI.gameObject.SetActive(false);

            Color col = hitScoreUI.transform.GetChild(0).GetComponent<Image>().color;
            col.a = 0.5f;
            hitScoreUI.transform.GetChild(0).GetComponent<Image>().color = col;
            hitScoreUI.transform.SetParent(ObjectPooler.instance.transform);
        });

        GameManager.instance.ManageBallCount(3);

        isShattered = true;
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
                transform.GetComponent<Animator>().enabled = false;
                foreach (Transform item in obstaclePieces)
                {
                    if (item.localPosition.y > hit.gameObject.transform.localPosition.y)
                    {
                        item.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                        item.gameObject.tag = "Untagged";

                    }

                }
            }
            
        }
    }
}
