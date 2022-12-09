using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    public Transform mainCam, menuCam, canvasLvButtons;
    public GameObject menuScene, mainCanvas;
    public GameObject player;
    public Vector3 menuCamPos;

    public int readyLevel;

    public List<float> playerCheckPoint;

    #region Singleton
    public static MainMenu instance;

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

    public void startBtn()
    {
        SceneManager.LoadScene(1);
    }
    public void Start()
    {
        /*PlayerPrefs.SetInt("myLevel", 0);*/
        menuCamPos = menuCam.transform.position;
        if (PlayerPrefs.HasKey("currentLv"))
        {
            readyLevel = PlayerPrefs.GetInt("currentLv");
        }
    }

    private void Update()
    {
        // PlayerPrefs.SetInt("currentLevel", 0);
        // readyLevel = PlayerPrefs.GetInt(myLevel);
        /*for (int i = 0; i <= readyLevel; i++)
        {
            canvasLvButtons.GetChild(i).gameObject.SetActive(true);
        }*/
    }

    public async void LvButton(int Lv)
    {
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, playerCheckPoint[Lv]);
        mainCam.position = new Vector3(mainCam.position.x, mainCam.position.y, playerCheckPoint[Lv]);
        menuScene.SetActive(false);
        menuScene.transform.parent.GetChild(0).gameObject.SetActive(false);

        menuCam.transform.DOMove(mainCam.position, 4f).OnComplete(() =>
        {
            mainCam.gameObject.SetActive(true);
            mainCanvas.gameObject.SetActive(true);
            menuCam.gameObject.SetActive(false);
            player.GetComponent<CharacterController>().enabled = true;

        });
        menuCam.transform.DOLocalRotate(new Vector3(0f, 90f, 0f), 4f);
        await GameManager.instance.WaitForSomeTime(0.5f);
        AudioManager.instance.SmoothPlay("MainMenu", "IntroStage1");
        
    }

    public void MenuButton()
    {
        /*GameManager.instance.ResumeGame();
        AudioManager.instance.SmoothPlay(GameManager.instance.currentBG, "Main Menu");
        menuCam.gameObject.SetActive(true);
        mainCam.gameObject.SetActive(false);
        menuCam.position = new Vector3(mainCam.position.x, mainCam.position.y, mainCam.position.z);
        player.GetComponent<CharacterController>().enabled = false;

        menuCam.transform.DOMove(menuCamPos, 2f);
        menuCam.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 2f);
        await GameManager.instance.WaitForSomeTime(1f);
        menuScene.SetActive(true);
        menuScene.transform.parent.GetChild(0).gameObject.SetActive(true);*/
        GameManager.instance.ResumeGame();
        SceneManager.LoadScene(0);
        GameManager.RestartLV = false;
    }
    public void ResetButton()
    {
        GameManager.instance.ResumeGame();
        SceneManager.LoadScene(0);

        // playerPrefsManager.SetLvStart(1);
        GameManager.RestartLV = true;
        /* GameManager.instance.ResumeGame();
         SceneManager.LoadScene(0);
        // AudioManager.instance.Play("Rewind"); 
         player.GetComponent<CharacterController>().enabled = false;
         AudioManager.instance.SmoothPlay(GameManager.instance.currentBG, "MainMenu");*/
        /* player.transform.DOMove(new Vector3(player.transform.position.x, player.transform.position.y, playerCheckPoint[0]), 4f).OnComplete(() =>
         {
             player.GetComponent<CharacterController>().enabled = true;
         });
         mainCam.transform.DOMove(new Vector3(mainCam.position.x, mainCam.position.y, playerCheckPoint[0]), 4f);*/
    }
}
