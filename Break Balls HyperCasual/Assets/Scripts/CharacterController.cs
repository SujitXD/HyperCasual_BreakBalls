using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class CharacterController : MonoBehaviour
{
    #region Singleton
    public static CharacterController instance;

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

    AudioManager audioManager;

    public float forwardPlayerSpeed = 10f;
    public Camera cam;
    public bool hits;

    [Range(1, 5)]
    public int ballSpawnCount;

    //public GameObject ballPrefab;

    public float forwardForce, upwardForce;
    
    Vector3 direction;
    bool isClicked, isHolding;
    Rigidbody rb;

    public float machineBallsFireRate = 0.2f;
    public bool machineBalls, fireBalls;

    bool isThrown;
    public int currentLv;

    private void Start()
    {
        audioManager = AudioManager.instance;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        PlayerInput();

        /*if(Input.GetKeyDown(KeyCode.Space))
        {
            PlayerDamage();
        }*/
    }

    private void FixedUpdate()
    {
        SpawnBall();
        rb.velocity = Vector3.forward * forwardPlayerSpeed;
    }

    private void PlayerInput()
    {
        if (GameManager.instance.GetCurrentBallCount() > 0)
        {
            bool inputDetection = false;

            #if UNITY_EDITOR
                 inputDetection = EventSystem.current.IsPointerOverGameObject();
            #elif UNITY_ANDROID
                 inputDetection = EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
            #elif UNITY_IPHONE
                 inputDetection = EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
            #endif

            if (Input.GetMouseButtonDown(0) && !inputDetection)
            {
                if (!machineBalls)
                    isClicked = true;

                isHolding = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isHolding = false;
            }

            direction = cam.ScreenPointToRay(Input.mousePosition).direction;
        }
    }

    private void PlayerDamage()
    {
        audioManager.Play("Crash");

        GameManager.instance.ScreenEffect(Color.red, 0.5f);
        StartCoroutine(CameraShake.instance.Shake(0.3f, 0.4f, 1f));

        GameManager.instance.ballCountFillImg.fillAmount = 0f;
        ballSpawnCount = 1;

        for (int i = 0; i < GameManager.instance.ballCountImgs.Count; i++)
        {
            if (i == ballSpawnCount - 1)
            {
                GameManager.instance.ballCountImgs[i].SetActive(true);
            }
            else
            {
                GameManager.instance.ballCountImgs[i].SetActive(false);
            }
        }

        int removeBalls = 10;
        //await GameManager.instance.WaitForSomeTime(1f);
        GameManager.instance.ManageBallCount(-removeBalls);

        for (int i = 0; i < removeBalls; i++)
        {
            Rigidbody ball = ObjectPooler.instance.SpawnFromPool("PsuedoBall", new Vector3(Random.Range(-0.5f, 0.5f), -1f, 0f), Quaternion.identity, transform, true).GetComponent<Rigidbody>();

            ball.transform.parent = null;
            ball.AddForce(Vector3.forward * Random.Range(50f, 80f), ForceMode.Impulse);
            ball.AddForce(Vector3.up * Random.Range(20f, 30f), ForceMode.Impulse);
        }
    }

    private void SpawnBall()
    {
        if (isClicked || (isHolding && machineBalls))
        {
            audioManager.Play("BallSwoosh");

            if(!machineBalls)
                isClicked = false;
            
            for (int i = 0; i < ballSpawnCount; i++)
            {
                if (machineBalls && isThrown)
                    break;

                //Rigidbody ball = Instantiate(ballPrefab, transform).GetComponent<Rigidbody>();
                Rigidbody ball = ObjectPooler.instance.SpawnFromPool("Ball", new Vector3(0f, -1f, 0f), Quaternion.identity, transform, true).GetComponent<Rigidbody>();
                //ball.transform.localPosition = Vector3.zero;

                if(ballSpawnCount == 2)
                {
                    if (i == 0)
                    {
                        Vector3 offset = new Vector3(-0.2f, 0f, 0f);
                        ball.transform.localPosition = offset;
                        direction += offset / 10f;
                    }
                    else if (i == 1)
                    {
                        Vector3 offset = new Vector3(0.2f, 0f, 0f);
                        ball.transform.localPosition = offset;
                        direction += offset / 10f;
                    }
                }
                else if (ballSpawnCount == 3)
                {
                    if (i == 0)
                    {
                        Vector3 offset = new Vector3(0f, 0.2f, 0f);
                        ball.transform.localPosition = offset;
                        direction += offset / 10f;
                    }
                    else if (i == 1)
                    {
                        Vector3 offset = new Vector3(-0.2f, -0.2f, 0f);
                        ball.transform.localPosition = offset;
                        direction += offset / 10f;
                    }
                    else if (i == 2)
                    {
                        Vector3 offset = new Vector3(0.2f, -0.2f, 0f);
                        ball.transform.localPosition = offset;
                        direction += offset / 10f;
                    }
                }
                else if (ballSpawnCount == 4)
                {
                    if (i == 0)
                    {
                        Vector3 offset = new Vector3(-0.2f, 0.2f, 0f);
                        ball.transform.localPosition = offset;
                        direction += offset / 10f;
                    }
                    else if (i == 1)
                    {
                        Vector3 offset = new Vector3(0.2f, 0.2f, 0f);
                        ball.transform.localPosition = offset;
                        direction += offset / 10f;
                    }
                    else if (i == 2)
                    {
                        Vector3 offset = new Vector3(-0.2f, -0.2f, 0f);
                        ball.transform.localPosition = offset;
                        direction += offset / 10f;
                    }
                    else if (i == 3)
                    {
                        Vector3 offset = new Vector3(0.2f, -0.2f, 0f);
                        ball.transform.localPosition = offset;
                        direction += offset / 10f;
                    }
                }
                else if (ballSpawnCount == 5)
                {
                    if (i == 0)
                    {
                        Vector3 offset = new Vector3(-0.3f, 0.2f, 0f);
                        ball.transform.localPosition = offset;
                        //direction += offset / 10f;
                    }
                    else if (i == 1)
                    {
                        Vector3 offset = new Vector3(0.3f, 0.2f, 0f);
                        ball.transform.localPosition = offset;
                        //direction += offset / 10f;
                    }
                    else if (i == 2)
                    {
                        Vector3 offset = new Vector3(0f, 0f, 0f);
                        ball.transform.localPosition = offset;
                        //direction += offset / 10f;
                    }    
                    else if (i == 3)
                    {
                        Vector3 offset = new Vector3(-0.3f, -0.2f, 0f);
                        ball.transform.localPosition = offset;
                        //direction += offset / 10f;
                    }
                    else if (i == 4)
                    {
                        Vector3 offset = new Vector3(0.3f, -0.2f, 0f);
                        ball.transform.localPosition = offset;
                        //direction += offset / 10f;
                    }
                }

                ball.transform.parent = null;
                ball.AddForce(direction * forwardForce, ForceMode.Impulse);
                //ball.AddForce(ball.transform.up * upwardForce, ForceMode.Impulse);

                if(machineBalls)
                {

                    isThrown = true;
                    Invoke(nameof(ResetThrow), machineBallsFireRate);
                    break;
                }
            }

            if (!machineBalls)
                GameManager.instance.ManageBallCount(-1);
        }
    }

    void ResetThrow()
    {
        isThrown = false;
    }

    public void MachineBalls(bool val)
    {
        machineBalls = val;
    }
    public void FireBalls(bool val)
    {
        fireBalls = val;
    }


    private async void OnTriggerEnter(Collider col)
    {
        // PlayerDamage();
        if (col.CompareTag("Obstacle") && !hits)
        {
            if (col.name.Contains("Laser"))
                audioManager.Play("LaserHit");

            PlayerDamage();
            hits = true;
            print("cOL name :: " + col.contactOffset);

            await GameManager.instance.WaitForSomeTime(1f);
            hits = false;
        }
        if (col.CompareTag("Door") && !hits)
        {
            PlayerDamage();
            hits = true;
            print("cOL name :: " + col.contactOffset);

            await GameManager.instance.WaitForSomeTime(1f);
            hits = false;
        }
        if (col.CompareTag("Interactable"))
        {
            col.GetComponent<Interactable>().Interact();
            currentLv= col.GetComponent<Interactable>().Lv;
        }
    }
}
