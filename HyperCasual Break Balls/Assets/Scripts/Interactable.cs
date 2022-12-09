using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class Interactable : MonoBehaviour
{
    [SerializeField] private Color fogColor;

    [Header("Player Motion")]
    CharacterController characterController;
    [SerializeField] private float characterForwardSpeed;
    [SerializeField] private float characterBallSpeed;

    [Header("Handle Audio")]
    AudioManager audioManager;
    [SerializeField] private bool changeBgAudio, changeFog, zeroGrvity, normalGrvity, checkPointUI;
    [SerializeField] private string audioPlaying;
    [SerializeField] private string audioToPlay;

    public List<GameObject> objectToEnable, objectToDisable, checkPointUIToEnableSlow;
    public string LvToEnable, LvToDisable;
    public int Lv;
    /*[Header("Handle Vision")]
    CameraFollow cameraFollow;
    public bool rotateVision;
    public Vector3 vision;*/

    private void Start()
    {
        characterController = CharacterController.instance;
        audioManager = AudioManager.instance;
        //cameraFollow = CameraFollow.instance;
    }

    public async void Interact()
    {
        if (checkPointUI)
        {
            foreach (GameObject obj in checkPointUIToEnableSlow)
            {
                await GameManager.instance.WaitForSomeTime(1f);
                obj.SetActive(true);
                await GameManager.instance.WaitForSomeTime(2f);
                obj.SetActive(false);
            }
        }

        if (PlayerPrefs.GetInt("currentLv") < Lv && Lv != 0)
        {
            PlayerPrefs.SetInt("currentLv", Lv);
        }

        if (zeroGrvity)
        {
            Physics.gravity = new Vector3(0f, 0f, 0f);
        }
        if (normalGrvity)
        {
            Physics.gravity = new Vector3(0f, -30f, 0f);
        }

        if (changeFog)
        {
            RenderSettings.fogColor = fogColor;

        }
        //characterController.ManageCharacterMotion(characterForwardSpeed, characterBallSpeed);

        if (changeBgAudio)
        {
            audioManager.SmoothPlay(audioPlaying, audioToPlay);
            GameManager.instance.currentBG = audioToPlay;
        }

        /*if(rotateVision)
        {
            characterController.transform.DORotate(vision, 1f).SetEase(Ease.Linear);
            cameraFollow.transform.DORotate(vision, 1f).SetEase(Ease.Linear);
        }*/

        if (objectToEnable.Count != 0)
        {
            foreach (GameObject obj in objectToEnable)
            {
                obj.SetActive(true);
            }
        }
        if (objectToDisable.Count != 0)
        {
            foreach (GameObject obj in objectToDisable)
            {
                obj.SetActive(false);
            }
        }

        if (LvToEnable != "")
        {
            //ObjectPooler.instance.SpawnFromPool(LvToEnable,Vector3.zero, Quaternion.identity, transform, false, true);
            GameObject cp = Instantiate(Resources.Load("Checkpoints/" + LvToEnable)) as GameObject;
            GameManager.instance.checkPoints.Add(cp);
        }
        if (LvToDisable != "")
        {

            //ObjectPooler.instance.AddToPool(GameObject.Find(LvToDisable));
            //Destroy(GameObject.Find(LvToDisable + "(Clone)"));
            Destroy(GameManager.instance.checkPoints[0]);
            GameManager.instance.checkPoints.RemoveAt(0);
        }


        gameObject.SetActive(false);
    }
}
