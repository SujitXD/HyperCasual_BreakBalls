using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    #region Singleton
    public static CameraFollow instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    public bool zRot,rotClock,rotAntiClock;
    public float rotSpeed,gravitySpeed;
    public Vector3 Gravity;
    public Transform playerTarget;
    Vector3 offset;

    [Range(-1000f, 1000f)]
    public float RotGravityRatioConstant;
    /*public float Rot;
    public float playerSpeed;
    public float DoorDistance;*/

    private void Start()
    {
        Gravity = Physics.gravity;

       
        offset = transform.position - playerTarget.position;
    }

    private void Update()
    {
        //playerSpeed = playerTarget.GetComponent<CharacterController>().forwardPlayerSpeed;
        
        if (rotAntiClock)
        {
            //transform.Rotate(Vector3.back * rotSpeed  * Time.deltaTime);

            if (transform.rotation.z == 0f)
            {

            }
            //Rot = RotGravityRatioConstant / (DoorDistance / playerSpeed);
        }
        /*else
        {
            transform.Rotate(Vector3.forward * (rotSpeed * RotGravityRatioConstant) * Time.deltaTime);
        }*/
        // transform.position = playerTarget.position + offset;
    }
    private void FixedUpdate()
    {
        //Physics.gravity = Gravity;
        Gravity = Physics.gravity;
        transform.position = playerTarget.position + offset;

        if (rotAntiClock)
        {
            
            if (Physics.gravity.y < 30 && Physics.gravity.x < 0)
            {
                Physics.gravity += new Vector3(0f, gravitySpeed , 0f);
            }
            if (Physics.gravity.x > -30 && Physics.gravity.y < 0)
            {
                Physics.gravity += new Vector3( -gravitySpeed , 0f, 0f);
            }

            if (Physics.gravity.y > -30 && Physics.gravity.x > 0)
            {
                Physics.gravity += new Vector3(0f, -gravitySpeed  , 0f);
            }
            if (Physics.gravity.x < 30 && Physics.gravity.y > 0)
            {
                Physics.gravity += new Vector3(gravitySpeed , 0f, 0f);
            }

            


        }
        else if(rotClock)
        {
            if (Physics.gravity.y < 30 && Physics.gravity.x > 0)
            {
                Physics.gravity += new Vector3(0f, gravitySpeed, 0f);
            }
            if (Physics.gravity.x > -30 && Physics.gravity.y > 0)
            {
                Physics.gravity += new Vector3(-gravitySpeed, 0f, 0f);
            }

            if (Physics.gravity.y > -30 && Physics.gravity.x < 0)
            {
                Physics.gravity += new Vector3(0f, -gravitySpeed, 0f);
            }
            if (Physics.gravity.x < 30 && Physics.gravity.y < 0)
            {
                Physics.gravity += new Vector3(gravitySpeed, 0f, 0f);
            }
        }
    }
    public void GravityRot()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.CompareTag("Door") && !zRot)
        {
            print("door2");
            zRot = true;
        }
        else if (other.CompareTag("Door") && zRot)
        {
            print("door3");
            zRot = false;
        }*/
        if(other.name.Contains("AutoDoorRotateAntiClock") )
        {
            print("AutoDoorRotateAntiClock");
            // DoorDistance = other.transform.parent.GetChild(other.transform.GetSiblingIndex() + 1).position.z - other.transform.position.z  ;

            rotAntiClock = true;
            transform.DORotate(new Vector3(0f, 0f, -180f), 15f).SetEase(Ease.Linear).OnComplete(() =>
            {             

                transform.DORotate(new Vector3(0f, 0f, -360f), 15f).SetEase(Ease.Linear).OnComplete(() => 
                {

                    rotAntiClock = false;
                    Physics.gravity = new Vector3(0f, -30f, 0f);
                });
            });           

        }
        if (other.name.Contains("AutoDoorRotateClock"))
        {
            print("AutoDoorRotateClock");
            // DoorDistance = other.transform.parent.GetChild(other.transform.GetSiblingIndex() + 1).position.z - other.transform.position.z  ;

            rotClock = true;
            transform.DORotate(new Vector3(0f, 0f, 180f), 15f).SetEase(Ease.Linear).OnComplete(() =>
            {

                transform.DORotate(new Vector3(0f, 0f, 360f), 15f).SetEase(Ease.Linear).OnComplete(() =>
                {

                    rotClock = false;
                    Physics.gravity = new Vector3(0f, -30f, 0f);
                });
            });

        }
        if (other.name.Contains("AutoDoorRotateAntiClock2"))
        {
            print("AutoDoorRotateAntiClock2");
            // DoorDistance = other.transform.parent.GetChild(other.transform.GetSiblingIndex() + 1).position.z - other.transform.position.z  ;

            rotAntiClock = true;
            transform.DORotate(new Vector3(0f, 0f, -180f), 15f).SetEase(Ease.Linear).OnComplete(() =>
            {

                transform.DORotate(new Vector3(0f, 0f, -360f), 15f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    transform.DORotate(new Vector3(0f, 0f, -180f), 15f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        transform.DORotate(new Vector3(0f, 0f, -360f), 15f).SetEase(Ease.Linear).OnComplete(() =>
                        {

                            rotAntiClock = false;
                            Physics.gravity = new Vector3(0f, -30f, 0f);
                        });
                        
                    });
                });
            });

        }
        if (other.name.Contains("AutoDoorRotateClock2"))
        {
            print("AutoDoorRotateClock2");
            // DoorDistance = other.transform.parent.GetChild(other.transform.GetSiblingIndex() + 1).position.z - other.transform.position.z  ;

            rotClock = true;
            transform.DORotate(new Vector3(0f, 0f, 180f), 15f).SetEase(Ease.Linear).OnComplete(() =>
            {
                transform.DORotate(new Vector3(0f, 0f, 360f), 15f).SetEase(Ease.Linear).OnComplete(() =>
                {                    
                    transform.DORotate(new Vector3(0f, 0f, 180f), 15f).SetEase(Ease.Linear).OnComplete(() =>
                    {

                        transform.DORotate(new Vector3(0f, 0f, 360f), 15f).SetEase(Ease.Linear).OnComplete(() =>
                        {

                            rotClock = false;
                            Physics.gravity = new Vector3(0f, -30f, 0f);
                        });
                    });
                });
            });

        }
        if (other.name.Contains("AutoDoorRotateClockRandomFast"))
        {
            print("AutoDoorRotateClockRandomFast");
            // DoorDistance = other.transform.parent.GetChild(other.transform.GetSiblingIndex() + 1).position.z - other.transform.position.z  ;
            Physics.gravity = new Vector3(0f, 0f, 0f);
            //rotClock = true;
            transform.DORotate(new Vector3(0f, 0f, 110f), 10f).SetEase(Ease.Linear).OnComplete(() =>
            {
                //rotClock = false;
               // rotAntiClock = true;
                transform.DORotate(new Vector3(0f, 0f, -110f), 10f).SetEase(Ease.Linear).OnComplete(() =>
                {

                    //rotAntiClock = false;
                    //rotClock = true;
                    transform.DORotate(new Vector3(0f, 0f, 110f), 10f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                       // rotClock = false;
                       // rotAntiClock = true;
                        transform.DORotate(new Vector3(0f, 0f, -110f), 10f).SetEase(Ease.Linear).OnComplete(() =>
                        {

                           // rotAntiClock = false;
                           // rotClock = true;
                            transform.DORotate(new Vector3(0f, 0f, 0f), 10f).SetEase(Ease.Linear).OnComplete(() =>
                            {
                               // rotClock = false;
                                Physics.gravity = new Vector3(0f, -30f, 0f);
                            });
                        });
                    });
                    
                });
            });

        }
        if (other.name.Contains("AutoDoorRotateAntiClockRandomSlow"))
        {
            print("AutoDoorRotateAntiClockRandomSlow");
            // DoorDistance = other.transform.parent.GetChild(other.transform.GetSiblingIndex() + 1).position.z - other.transform.position.z  ;

            rotClock = true;
            transform.DORotate(new Vector3(0f, 0f, -110f), 15f).SetEase(Ease.Linear).OnComplete(() =>
            {
                rotClock = false;
                rotAntiClock = true;
                transform.DORotate(new Vector3(0f, 0f, 110f), 15f).SetEase(Ease.Linear).OnComplete(() =>
                {

                    rotAntiClock = false;
                    rotClock = true;
                    transform.DORotate(new Vector3(0f, 0f, -110f), 15f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        rotClock = false;
                        rotAntiClock = true;
                        transform.DORotate(new Vector3(0f, 0f, 110f), 15f).SetEase(Ease.Linear).OnComplete(() =>
                        {

                            rotAntiClock = false;
                            rotClock = true;
                            transform.DORotate(new Vector3(0f, 0f, 0f), 15f).SetEase(Ease.Linear).OnComplete(() =>
                            {
                                rotClock = false;
                                Physics.gravity = new Vector3(0f, -30f, 0f);
                            });
                        });
                    });

                });
            });

        }
    }
    private void OnCollisionEnter(Collision col)
    {
        /*if (col.transform.CompareTag("Door") && !zRot)
        {
            print("door");
            zRot = true;
        }
        if (col.transform.CompareTag("Door") && zRot)
        {
            zRot = false;
        }*/
    }
    /* private void LateUpdate()
     {
         transform.position = playerTarget.position + offset;
     }*/
}
