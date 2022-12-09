using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Crystal : ObstacleManager
{
    [SerializeField] protected List<Transform> obstaclePieces;
    [SerializeField] private Material brokedCrystalMat, brokedCrystalMatLightUp;
    [SerializeField] private int ballCounts = 3;

    public float diamondCrystalDetection = 4f;
    public float triangleCrystalDetection = 2f;
    Transform diamondCrystal, triangleCrystal;
    Transform characterController;
    public bool isStar,powerUP;
    bool isDiamond, isTriangle;

    private void Start()
    {
        if(isStar)
        {
            characterController = CharacterController.instance.transform;
            diamondCrystal = transform.GetChild(1);
            triangleCrystal = transform.GetChild(2);

            diamondCrystal.parent = null;
            triangleCrystal.parent = null;

        }
    }

    private void Update()
    {
        if (!isStar)
            return;

        if(!isDiamond && Vector3.Distance(transform.position, characterController.position) < diamondCrystalDetection)   
        {
            isDiamond = true;
            transform.GetChild(0).DOScale(Vector3.zero, 1f).SetEase(Ease.Linear).OnComplete(() => { 
                transform.GetChild(0).gameObject.SetActive(false);
            });

            Vector3 diamondScale = diamondCrystal.localScale;
            diamondCrystal.gameObject.SetActive(true);
            diamondCrystal.localScale = Vector3.zero;
            diamondCrystal.DOScale(diamondScale, 0.5f).SetEase(Ease.Linear);
        }
        else if (!isTriangle && Vector3.Distance(transform.position, characterController.position) < triangleCrystalDetection)
        {
            isTriangle = true;
            diamondCrystal.DOScale(Vector3.zero, 1f).SetEase(Ease.Linear).OnComplete(() => {
                diamondCrystal.gameObject.SetActive(false);
            });

            Vector3 triangleScale = triangleCrystal.localScale;
            triangleCrystal.gameObject.SetActive(true);
            triangleCrystal.localScale = Vector3.zero;
            triangleCrystal.DOScale(triangleScale, 0.5f).SetEase(Ease.Linear);
        }

        if (diamondCrystal.GetComponent<Crystal>().isShattered)
            isStar = false;
    }

    public override void Shatter(Vector3 hitPoint, float _magnitude)
    {
        base.Shatter(hitPoint, _magnitude);

        if (isShattered)
            return;

        if (powerUP)
        {
            foreach (Transform piece in obstaclePieces)
            {
                piece.GetComponent<MeshRenderer>().material = brokedCrystalMatLightUp;
                //piece.SetParent(transform);
            }
        }
        else
        {
            foreach (Transform piece in obstaclePieces)
            {
                piece.GetComponent<MeshRenderer>().material = brokedCrystalMat;
                //piece.SetParent(transform);
            }
        }
        

        GameObject hitScoreUI = ObjectPooler.instance.SpawnFromPool("CrystalHitScore", new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z), Quaternion.identity);
        hitScoreUI.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = ballCounts.ToString();
        hitScoreUI.transform.GetChild(0).GetComponent<Image>().DOFade(0f, 1f).SetEase(Ease.Linear);
        hitScoreUI.transform.DOMoveY(transform.position.y + 7f, 1f).SetEase(Ease.Linear).OnComplete(() => {
            hitScoreUI.gameObject.SetActive(false);

            Color col = hitScoreUI.transform.GetChild(0).GetComponent<Image>().color;
            col.a = 0.5f;
            hitScoreUI.transform.GetChild(0).GetComponent<Image>().color = col;
            hitScoreUI.transform.SetParent(ObjectPooler.instance.transform);
        });

        GameManager.instance.ManageBallCount(ballCounts);

        isShattered = true;
    }

    public override void Explode(Collider hit, Vector3 explosionPos, float collisionMagnitude)
    {
        base.Explode(hit, explosionPos, collisionMagnitude);

        if (isStar)
        {
            isStar = false;
        }

        if (transform.GetChild(0).GetComponent<DoRotate>() != null)
            transform.GetChild(0).GetComponent<DoRotate>().enabled = false;

        if (hit.attachedRigidbody)
        {
            hit.attachedRigidbody.isKinematic = false;
            hit.attachedRigidbody.gameObject.tag = "Untagged";
            if (powerUP)
            {
                hit.attachedRigidbody.AddExplosionForce(30 * collisionMagnitude, explosionPos, radius, upwards);
            }
            else
            {
                hit.attachedRigidbody.AddExplosionForce(power * collisionMagnitude, explosionPos, radius, upwards);
            }
            
        }
    }
}
