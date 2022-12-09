using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingRing : ObstacleManager
{
    [SerializeField] private bool isMoving;
  //  [SerializeField] protected List<Transform> obstaclePieces;
    Transform blockGfx;
    public List<Transform> wayPoints;
    public float speed;

    public bool slab;

    public bool reverse,Connection1, Connection2;
    public static bool  Connect1, Connect2, Connect3, Connect4;
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
            hit.attachedRigidbody.AddExplosionForce(power * collisionMagnitude, explosionPos, radius, upwards);




            if(hit.name== "Connection1")
            {
                Connection1 = true;
                if (Connection2 && Connection1)
                {
                    transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
                }
                 
                
                print("Connection1 hit ");
            }
            if (hit.name == "Connection2")
            {
                Connection2 = true;
                if (Connection1 && Connection2)
                {
                    transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
                }


                print("Connection2 hit ");
            }
            if (hit.name == "Connect1")
            {
                
                Connect1 = true;
                print("Connect1 hit ");
                if (Connect2 && Connect1 )
                {
                    if (transform.GetChild(0).Find("Cylinder (3)") != null)
                    {
                        transform.GetChild(0).Find("Cylinder (3)").GetComponent<Rigidbody>().isKinematic = false;
                        transform.GetChild(0).Find("Cylinder (3)").SetParent(null);
                    }

                    print("Connect1 Connect2 ");
                }
                else if (Connect4 && Connect1  )
                {
                    if (transform.GetChild(0).Find("Cylinder (3)") != null)
                    {
                        transform.GetChild(0).Find("Cylinder (3)").GetComponent<Rigidbody>().isKinematic = false;
                        transform.GetChild(0).Find("Cylinder (3)").SetParent(null);
                    }
                    if (transform.GetChild(0).Find("Cylinder (2)") != null)
                    {                                         
                        transform.GetChild(0).Find("Cylinder (2)").GetComponent<Rigidbody>().isKinematic = false;
                        transform.GetChild(0).Find("Cylinder (2)").SetParent(null);
                    }
                    if(transform.GetChild(0).Find("InnerRing") != null)
                    {
                        transform.GetChild(0).Find("InnerRing").GetComponent<Rigidbody>().isKinematic = false;
                        transform.GetChild(0).Find("InnerRing").SetParent(null);
                    }
                    
                }
            }
            if (hit.name == "Connect2")
            {
                
                Connect2 = true;
                print("Connect2 hit ");

                if (Connect1 && Connect2)
                {
                    if (transform.Find("Cylinder (3)") != null)
                    {
                        transform.Find("Cylinder (3)").GetComponent<Rigidbody>().isKinematic = false;
                        transform.Find("Cylinder (3)").SetParent(null);
                    }

                    print("Connect1 Connect2 ");
                }
                else if (Connect3 && Connect2)
                {
                    if (transform.Find("InnerRing") != null)
                    {
                        transform.Find("InnerRing").GetComponent<Rigidbody>().isKinematic = false;
                        transform.Find("InnerRing").SetParent(null);
                    }
                }
            }
            if (hit.name == "Connect3")
            {
                
                Connect3 = true;
                print("Connect3 hit ");
                if ( Connect4 && Connect3 )
                {
                    if (transform.Find("Cylinder (2)") != null)
                    {
                        transform.Find("Cylinder (2)").GetComponent<Rigidbody>().isKinematic = false;
                        transform.Find("Cylinder (2)").SetParent(null);
                    }

                    print("Connect3 Connect4 ");
                }
                else if (Connect2 && Connect3)
                {
                    if (transform.Find("InnerRing") != null)
                    {
                        transform.Find("InnerRing").GetComponent<Rigidbody>().isKinematic = false;
                        transform.Find("InnerRing").SetParent(null);
                    }
                }


            }
            if (hit.name == "Connect4")
            {
                
                Connect4 = true;
                print("Connect4 hit ");
                if (Connect3 && Connect4)
                {
                    if (transform.GetChild(0).Find("Cylinder (2)") != null)
                    {
                        transform.GetChild(0).Find("Cylinder (2)").GetComponent<Rigidbody>().isKinematic = false;
                        transform.GetChild(0).Find("Cylinder (2)").SetParent(null);
                    }

                    print("Connect3 Connect4 ");
                }
                else if (Connect1 && Connect4)
                {
                    if (transform.GetChild(0).Find("Cylinder (3)") != null)
                    {
                        transform.GetChild(0).Find("Cylinder (3)").GetComponent<Rigidbody>().isKinematic = false;
                        transform.GetChild(0).Find("Cylinder (3)").SetParent(null);
                    }
                    if (transform.GetChild(0).Find("Cylinder (2)") != null)
                    {
                        transform.GetChild(0).Find("Cylinder (2)").GetComponent<Rigidbody>().isKinematic = false;
                        transform.GetChild(0).Find("Cylinder (2)").SetParent(null);
                    }
                    if (transform.GetChild(0).Find("InnerRing") != null)
                    {
                        transform.GetChild(0).Find("InnerRing").GetComponent<Rigidbody>().isKinematic = false;
                        transform.GetChild(0).Find("InnerRing").SetParent(null);
                    }

                }
            }
           

            if (slab)
            {
                
                /*foreach (Transform item in obstaclePieces)
                {
                    if (item.localPosition.y > hit.gameObject.transform.localPosition.y)
                    {
                        item.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    }

                }*/
            }


        }
    }
}
