using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : ObstacleManager
{

    public int childIndex;
    public List<GameObject> childs;

    // Start is called before the first frame update
    void Start()
    {
        childIndex = transform.GetSiblingIndex();

        foreach (Transform item in transform.parent)
        {
            childs.Add(item.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.CompareTag("Ball"))
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
            
        }*/
    }
    public async override void Explode(Collider hit, Vector3 explosionPos, float collisionMagnitude)
    {
        base.Explode(hit, explosionPos, collisionMagnitude);

        if (hit.transform == transform)
        {
            transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
            for (int i = childIndex+1; i < childs.Count; i++)
            {
                childs[i].GetComponent<Rigidbody>().isKinematic = false;
                childs[i].gameObject.transform.SetParent(null); 
               /* transform.parent.GetComponent<DoRotate>().childs[i].GetComponent<Rigidbody>().isKinematic = false;
                transform.parent.GetComponent<DoRotate>().childs[i].gameObject.transform.SetParent(null);*/
                
            }
            foreach (Transform child in transform.GetChild(0))
            {
                child.transform.gameObject.SetActive(true);
                //child.GetComponent<Rigidbody>().isKinematic = false;
                child.GetComponent<Rigidbody>().AddExplosionForce(power * collisionMagnitude, explosionPos, radius, upwards);
                child.transform.gameObject.transform.SetParent(null);
            }

            transform.GetComponent<Collider>().enabled = false;
            isShattered = true;

            /*await GameManager.instance.WaitForSomeTime(2f);
            for (int i = childIndex; i < transform.parent.childCount; i++)
            {
                //transform.parent.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
                transform.parent.GetChild(i).SetParent(null);
            }*/
        }
    }
}
