using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Ball : MonoBehaviour
{
    AudioManager audioManager;
    //public GameObject meshCutter;
    //Rigidbody ballRb;
    ObjectPooler objectPooler;
    CharacterController characterController;
    CameraShake cameraShake;

    bool isUsed;

    private void Awake()
    {
        //ballRb = GetComponent<Rigidbody>();
        audioManager = AudioManager.instance;
        objectPooler = ObjectPooler.instance;
        characterController = CharacterController.instance;
        cameraShake = CameraShake.instance;
    }

    private void OnEnable()
    {
        isUsed = false;
    }

    private async void OnCollisionEnter(Collision col)
    {
        print("s");
        //ballRb.velocity = Vector3.zero;
        /*if (col.collider.gameObject.CompareTag("Multiplier"))
        {
            print("+");
            col.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
            col.collider.gameObject.transform.GetChild(3).gameObject.SetActive(false);
           GameManager.instance.ManageBallCount(col.collider.gameObject.GetComponent<Multiplier>().num);
        }

        if (col.collider.gameObject.CompareTag("Multiplier-"))
        {
            print("-");
            col.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
            col.collider.gameObject.transform.GetChild(3).gameObject.SetActive(false);
            GameManager.instance.ManageBallCount(-col.collider.gameObject.GetComponent<Multiplier>().num);
        }*/

        if (col.collider.gameObject.CompareTag("Obstacle"))
        {
            if (isUsed)
                return;

            print(col.collider.name);

            ContactPoint contact = col.contacts[0];
            col.collider.transform.parent.parent.GetComponent<ObstacleManager>().Shatter(contact.point, col.relativeVelocity.magnitude);
            isUsed = true;
        }
        if (col.collider.gameObject.CompareTag("Gate"))
        {

            if (col.collider.name.Contains("Inner"))
            {
                if (isUsed)
                    return;

                col.collider.transform.parent.GetComponent<Animator>().enabled = false;
                print(col.collider.name);

                ContactPoint contact = col.contacts[0];
                col.collider.transform.parent.GetComponent<Crystal>().powerUP = true;
                col.collider.transform.parent.GetComponent<ObstacleManager>().Shatter(contact.point, col.relativeVelocity.magnitude);
                isUsed = true;

                col.collider.gameObject.SetActive(false);
            }
            else
            {
                if (isUsed)
                    return;
                col.collider.transform.parent.parent.GetComponent<Animator>().enabled = false;
                print(col.collider.name);

                ContactPoint contact = col.contacts[0];
                col.collider.transform.parent.parent.GetComponent<ObstacleManager>().Shatter(contact.point, col.relativeVelocity.magnitude);
                isUsed = true;
                col.collider.transform.parent.parent.transform.GetChild(3).gameObject.SetActive(false);
            }
            if (characterController.currentLv == 0)
            {
                ObjectPooler.instance.SpawnFromPool("ParticleEffect", transform.position, Quaternion.identity, null, false);
                objectPooler.AddToPool(gameObject);
            }


        }

        if (col.collider.gameObject.CompareTag("Button"))
        {
            col.collider.enabled = false;
            col.collider.transform.parent.GetChild(0).transform.DOLocalMoveX(-20f, 2f);
            col.collider.transform.parent.GetChild(1).transform.DOLocalMoveX(20f, 2f);
            audioManager.Play("Door");
            //col.collider.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }

        if (characterController.fireBalls)
        {
            print("fire");
            audioManager.Play("Explosion");
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            StartCoroutine(cameraShake.Shake(0.15f, 0.4f, 0.4f));
            GameObject blastEffect = transform.GetChild(0).gameObject;

            blastEffect.transform.parent = null;
            blastEffect.SetActive(true);
            await GameManager.instance.WaitForSomeTime(1f);
            blastEffect.SetActive(false);
            blastEffect.transform.SetParent(transform);

            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<Collider>().enabled = true;

            //objectPooler.AddToPool(gameObject);
        }
        if (col.collider.gameObject.CompareTag("BGWall") && characterController.currentLv > 0)
        {
            Vector3 rot = Vector3.zero;

            if (CheckHitDirection(Vector3.up))
            {
                rot = new Vector3(-90f, 0f, 0f);
                print("ball hit top");
            }
            else if (CheckHitDirection(Vector3.down))
            {
                rot = new Vector3(90f, 0f, 0f);
                print("ball hit bottom");
            }
            else if (CheckHitDirection(Vector3.left))
            {
                rot = new Vector3(0f, -90f, 0f);
                print("ball hit left");
            }
            else if (CheckHitDirection(Vector3.right))
            {
                rot = new Vector3(0f, 90f, 0f);
                print("ball hit right");
            }

            ObjectPooler.instance.SpawnFromPool("WallEffect", transform.position, Quaternion.Euler(rot), null, false);
            //audioManager.Play("Wall");
        }



        if (characterController.currentLv > 0)
        {
            ObjectPooler.instance.SpawnFromPool("ParticleEffect", transform.position, Quaternion.identity, null, false);

            objectPooler.AddToPool(gameObject);
        }
        



        /*if(col.collider.gameObject.CompareTag("Breakable"))
        {
            ContactPoint[] contact = col.contacts;

            for (int i = 0; i < 3; i++)
            {
                //GameObject cutter = Instantiate(meshCutter, contact[Random.Range(0, contact.Length)].point, Quaternion.Euler(0f, 0f, Random.Range(0f, 90f)));
                GameObject cutter = ObjectPooler.instance.SpawnFromPool("MeshCutter", contact[Random.Range(0, contact.Length)].point, Quaternion.Euler(0f, 0f, Random.Range(0f, 90f)));
            }
        }*/
    }

    private void OnTriggerEnter(Collider col)
    {
        /*if (col.CompareTag("Rope"))
        {
            Rope rope = col.transform.parent.GetComponent<Rope>();
            col.gameObject.SetActive(false);

            rope.weight.GetComponent<Weight>().enabled = false;

            Weight weight = rope.weight;
            weight.ropes.Remove(rope);
            //weight.ropes[weight.ropes.IndexOf(rope)] = null;

            if (weight.ropes.Count == 0)
            {
                foreach (Transform child in weight.transform.GetChild(0))
                {
                    child.GetComponent<Rigidbody>().isKinematic = false;
                }
            }

            *//*foreach (Transform child in rope.weight.transform.GetChild(0))
            {
                child.GetComponent<Rigidbody>().isKinematic = false;
            }*//*
        }*/

        if (col.CompareTag("Powerup"))
        {
            audioManager.Play("ItemPickup");

            PowerUp.Powerups powerUps = col.GetComponent<PowerUp>().powerUps;
            switch (powerUps)
            {
                case PowerUp.Powerups.MachineBalls:
                    GameObject machineBallUI = ObjectPooler.instance.SpawnFromPool("MachineBallUI", new Vector3(0f, -350f, 0f), Quaternion.identity, GameManager.instance.powerupUI, true);
                    machineBallUI.transform.localScale = Vector3.one;
                    GameManager.instance.powerups[0].powerUpBt.Add(machineBallUI.GetComponent<Button>());

                    break;
                case PowerUp.Powerups.FireBalls:
                    GameObject fireBallUI = ObjectPooler.instance.SpawnFromPool("FireBallUI", new Vector3(0f, -350f, 0f), Quaternion.identity, GameManager.instance.powerupUI, true);
                    fireBallUI.transform.localScale = Vector3.one;
                    GameManager.instance.powerups[1].powerUpBt.Add(fireBallUI.GetComponent<Button>());

                    break;
                case PowerUp.Powerups.SlowMotion:
                    GameObject slowMoUI = ObjectPooler.instance.SpawnFromPool("SlowMoUI", new Vector3(0f, -350f, 0f), Quaternion.identity, GameManager.instance.powerupUI, true);
                    slowMoUI.transform.localScale = Vector3.one;
                    GameManager.instance.powerups[2].powerUpBt.Add(slowMoUI.GetComponent<Button>());

                    break;
                default:
                    break;
            }

            col.gameObject.SetActive(false);
            GameManager.instance.AlignPowerupUI();
        }
    }

    bool CheckHitDirection(Vector3 dir)
    {
        Debug.DrawRay(transform.position, dir * 1.2f, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(dir), out hit, 1.2f))
        {
            if (hit.collider != null && hit.collider != transform)
            {
                return true;
            }
        }

        return false;
    }
}
